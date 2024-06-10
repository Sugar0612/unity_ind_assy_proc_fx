using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float m_CurrTime = 0;

    public float m_Time = 0;

    public Action m_Callback = null;

    public bool m_Running = false;

    public bool m_Destroy = false;

    // �Ƿ�Ϊȫ�ֶ���������ǵĻ�����TimerConsole�ĳ���ж�ص�ʱ���Destroy����
    public bool m_Global = false;
    private void FixedUpdate()
    {
        if (m_Running)
        {
            if (m_CurrTime >= m_Time)
            {
                m_Running = false;
                if (m_Callback != null)
                {
                    m_Callback();
                }

                if (m_Destroy)
                {
                    Destroy();
                }
            }
            else
            {
                m_CurrTime += Time.fixedDeltaTime;
            }
        }
    }

    /// <summary>
    /// ����ʱ
    /// </summary>
    /// <param name="time"></param>
    /// <param name="callback"></param>
    /// <param name="destroy">ִ�н����Ƿ�����</param>
    public void Run(float time, Action callback, bool destroy = false)
    {
        m_Time = time;
        m_CurrTime = 0;
        m_Callback = callback;
        m_Destroy = destroy;
        m_Running = true;
    }

    public void Stop()
    {
        m_Running = false;
    }

    public void Destroy()
    {
        m_Running = false;
        m_Callback = null;
        TimerConsole.Instance.Destroy(this);
    }
}
