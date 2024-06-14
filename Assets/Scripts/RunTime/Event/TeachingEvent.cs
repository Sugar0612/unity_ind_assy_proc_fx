using sugar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ѧ��ť���¼�
public class TeachingEvent : ModuleEvent
{
    public override void OnEvent(params object[] args)
    {
        base.OnEvent(args);
        //Debug.Log("Teaching Event!");

        foreach (var t in GlobalData.moduleContent)
        {
            SwitchSceneAccName(m_Name, t[1]);
        }
    }
}
