using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ѵ����ť�������¼�
public class TrainEvent : ModuleEvent
{
    public override void OnEvent(params object[] args)
    {
        base.OnEvent(args);
        Debug.Log("Train Event!");
    }
}
