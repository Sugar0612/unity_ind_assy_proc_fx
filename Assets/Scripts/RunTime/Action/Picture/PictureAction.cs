using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class PictureAction : BaseAction
{
    private bool m_Init = false;

    private PicturePanel m_Panel;

    public PictureAction()
    {
        m_Panel = UITools.FindAssetPanel<PicturePanel>();

        m_Token = new CancellationTokenSource();
    }

    public override async UniTask AsyncShow(string name)
    {
        if (!m_Init)
        {
            List<Sprite> sprites = await LoadPictureAsync(name);
            m_Panel.Init(sprites);
            m_Init = true;
        }

        await UniTask.WaitUntil(() => m_Init == true);
        m_Panel.Active(true);
    }

    private async UniTask<List<Sprite>> LoadPictureAsync(string name)
    {
        var paths = await NetworkManager._Instance.DownLoadPicturesAsync(name);

        List<Sprite> sprites = new List<Sprite>();
        //foreach (var path in paths)
        //{
        //    Debug.Log("picture action: " + path);
        //}

        if (paths.Count == 0)
            UITools.ShowMessage("��ǰģ��û��ͼƬ��Դ");

        AsyncResult result = await AssetConsole.Instance.LoadTexObject(paths.ToArray());
        await UniTask.WaitUntil(() => result.isLoad == true);
        foreach (var spo in result.m_Assets)
        {
            sprites.Add(Tools.SpriteConvert((Texture2D)spo.Value));
        }
        return sprites;
    }

    public override void Exit()
    {
        m_Token.Cancel();
        m_Token.Dispose();
        m_Token = new CancellationTokenSource();
        m_Panel.Active(false);
    }
}