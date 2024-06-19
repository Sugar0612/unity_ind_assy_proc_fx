using sugar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMethod
{
    None,
    Drag,
    Clicked
}

public enum GameState // Ŀǰ��һ����״̬
{
    Prepare,
    Playing,
    Stop
}

public class GameMode : Singleton<GameMode>
{
    public GameObject m_Arrow; // ��ͷ[����ʶ�𹤾��Ƿ���ȷ��]

    private bool m_Prepare;

    private GameMethod m_Method; // �ò������Ϸ����
    private GameState m_State;

    public Queue<string> m_Tools = new Queue<string>(); // Ŀǰ������Ҫ����Ĺ���


    private void FixedUpdate()
    {
        StateMachine();
    }

    private void StateMachine()
    {
        //if (m_State == GameState.Prepare && m_Tools.Count > 0)
        //{
        //    if (m_Method == GameMethod.Clicked)
        //    {
        //        PrepareClickStep(); // ���ģʽ�£����ݲ�ͬstring��ʵ�ֲ�ͬ���ߵ���˸
        //    }
        //}
    }

    public void Start()
    {
        Prepare();
    }

    /// <summary>
    /// ÿ�β��������Ŀ���ǲ��Ŷ�����
    /// �����е����ϷŲ��Ŷ������е��ǵ�����Ŷ�����������Ҫ���ݲ�ͬ�������д��
    /// </summary>
    public void Prepare()
    {
        if (!m_Prepare)
        {
            string method = GlobalData.stepStructs[GlobalData.StepIdx].method;
            foreach (var tool in GlobalData.stepStructs[GlobalData.StepIdx].tools)
            {
                m_Tools.Enqueue(tool); // �µĲ�������µĹ��߿�
            }

            if (method == "���")
            {
                m_Method = GameMethod.Clicked;
                CameraControl.target.AddComponent<HighlightingEffect>();
                ArrowActive(false);
            }
            else
            {
                m_Method = GameMethod.Drag;

                // ��ק����ǰ��׼������
                ArrowActive(true);
            }
            m_Prepare = true;
            m_State = GameState.Prepare;
        }
    }

    public static void PerformThisStep()
    {
        
    }

    public void ArrowActive(bool b)
    { 
        m_Arrow.SetActive(b); 
    }

    private void PrepareDragStep()
    {
        
    }

    private void PrepareClickStep()
    {
        string tool = m_Tools.Dequeue();
        GameObject go = GameObject.Find(tool);
        if (go != null)
        {
            HighlightableObject ho = go.AddComponent<HighlightableObject>();
            ho.FlashingOn(Color.green, Color.red, 2f);
        }
        m_State = GameState.Playing; // ׼���׶ν�����������Ϸ�׶�
    }

    public void NextStep()
    {
        if (GlobalData.StepIdx < GlobalData.stepStructs.Count)
        {
            GlobalData.StepIdx++;
            Prepare();
        }
    }

    // �û�����ѡ��ͬ�Ĳ��������Ϸ
    public void SetStep(int i)
    {
        if (i > 0 && i < GlobalData.stepStructs.Count)
        {
            GlobalData.StepIdx = i;
            Prepare();
        }
    }
}
