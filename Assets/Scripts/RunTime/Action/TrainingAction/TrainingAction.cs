using Cysharp.Threading.Tasks;
using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TrainingAction : BaseAction
{
    public bool isFinsh = false;

    public TrainingAction()
    {
        m_Token = new CancellationTokenSource();
    }

    public override async UniTask AsyncShow(string name)
    {
        //Debug.Log(name);
        await Tools.LoadSceneModel();
        InfoPanel._instance.TrainingModeUIClose();
        MenuPanel._instance.Active(false);
        GlobalData.mode = Mode.Examination;

        GlobalData.codeVSidDic.Clear();
        foreach (var child in GlobalData.examData.data.softwareInfoVo.child)
        {
            // Debug.Log("��ǰ��CODE = " + child.code + "       ��ǰ��ԴIDΪ = " + child.softwareId + "       ��ǰ����Դ����Ϊ = " + child.softwareName);
            GlobalData.codeVSidDic.Add(child.code, child.softwareId);
        }

        try
        {
            await UniTask.WaitUntil(() => isFinsh == true);
            //Debug.Log("await finish");
        }
        catch { }
    }

    public override void Exit(Action callback)
    {
        base.Exit(callback);
        Debug.Log("ʵ��ģʽ�˳�");
        isFinsh = true;
        CameraControl.SetMain();
    }
}
