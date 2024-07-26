using Cysharp.Threading.Tasks;
using sugar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ѵ����ť�������¼�
public class TrainEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        base.OnEvent(args);
        //Debug.Log("Train Event!");
        SwitchSceneAccName(base.m_Name);
        await UniTask.Yield();
    }
}
