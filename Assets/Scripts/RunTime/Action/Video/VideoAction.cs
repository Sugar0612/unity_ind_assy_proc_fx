using Cysharp.Threading.Tasks;
using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VideoAction : BaseAction
{
    private VideoPanel m_Panel;

    // 用来存储已经初始化过的子模块名字，下一次进入不在初始化
    private Dictionary<string, List<string>> m_initList = new Dictionary<string, List<string>>();

    private bool m_init = false; // 是否准备好了

    public VideoAction()
    {
        m_Token = new CancellationTokenSource();
        m_panelToken = new CancellationTokenSource();
    }

    public override async UniTask AsyncShow(string name)
    {
        if (!m_initList.ContainsKey(name))
        {
            var paths = await NetworkManager._Instance.DownLoadAasetAsync(name);

            if (paths.Count == 0)
            {
                UITools.ShowMessage("当前模块没有Video资源");
            }

            await NetworkTCPExpand.RsCkAndDLReq(paths, name);

            m_Panel = UIConsole.Instance.FindAssetPanel<VideoPanel>();
            m_Panel.Init(paths);
            m_initList.Add(name, paths);
            m_init = true;
        }
        else
        {
            m_Panel = UIConsole.Instance.FindAssetPanel<VideoPanel>();
            m_Panel.Init(m_initList[name]);
            m_init = true;
        }
        await UniTask.WaitUntil(() => m_init == true, PlayerLoopTiming.Update, m_Token.Token);

        //m_Panel.transform.SetAsFirstSibling();
        m_Panel.Active(true);
        try
        { 
            await UniTask.WaitUntil(() => m_Panel?.m_Content.activeSelf == false);
        }
        catch 
        {

        }
    }


    public override void Exit(Action callback)
    {
        base.Exit(callback);

        m_Token.Cancel();
        m_Token.Dispose();
        m_Token = new CancellationTokenSource();

        m_Panel.Exit();
        m_Panel.Active(false);

        m_init = false;
    }
}
