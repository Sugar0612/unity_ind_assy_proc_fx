using Cysharp.Threading.Tasks;
using LitJson;
using sugar;
using UnityEngine;

public class DownLoadEvent : BaseEvent
{
    public float old_Percent = 0.0f;
    public async override void OnPrepare(params object[] args)
    {
        // await UniTask.SwitchToMainThread();

        while(true)
        {
            await UniTask.WaitUntil(() => old_Percent != NetworkClientTCP.percent);

            old_Percent = NetworkClientTCP.percent;
            DownLoadPanel._instance.SetDLPercent(NetworkClientTCP.percent);

            if (NetworkClientTCP.percent == 100.0f)
            {
                old_Percent = 0.0f;
                break;
            }
        }
        Debug.Log("break!");
    }

    public override async void OnEvent(params object[] args)
    {
        await UniTask.RunOnThreadPool(() =>
        {
            var mp = args[0] as MessPackage;
            FilePackage fp = JsonMapper.ToObject<FilePackage>(mp.ret);
            //string savePath = Application.streamingAssetsPath + "\\Data\\" + fp.relativePath;

            DownLoadPanel._instance.m_NeedWt.Add(fp); //���������ļ����ݼ��ص��ڴ���ȥ
            GlobalData.Downloaded = true;
            //Tools.Bytes2File(fp.fileData, savePath);
        });
    }
}

/// <summary>
/// �ļ���
/// </summary>
public class FilePackage
{
    public string fileName;
    public string relativePath;
    public byte[] fileData;
}
