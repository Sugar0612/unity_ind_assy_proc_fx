using Cysharp.Threading.Tasks;
using sugar;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private List<GameObject> m_arrowTrans = new List<GameObject>(); // ��ͷ��ͬ�����λ��
    private bool m_Prepare;

    private GameMethod m_Method; // �ò������Ϸ����
    private GameState m_State;

    [HideInInspector]
    public List<string> m_Tools = new List<string>(); // Ŀǰ������Ҫ����Ĺ���
    private int m_ToolIdx = 0; // Ŀǰ���蹤�ߵ�����

    private string currToolName = ""; // ��Ҳ����ĵ�������ߵ�����

    [HideInInspector]
    public float m_Score; // ����ģʽһ������ķ���

    private bool m_Init = false; // ��ʼ���ɹ�

    public async void Start()
    {
        await UniTask.WaitUntil(() => GlobalData.stepStructs.Count != 0);


        m_arrowTrans = GameObject.FindGameObjectsWithTag("trans").ToList();
        // Debug.Log(m_arrowTrans.Count);
        for (int i = 0; i < m_arrowTrans.Count && i < GlobalData.stepStructs.Count; i++)
        {
            // Debug.Log("Looper: " + m_arrowTrans[i].position);
            var step_info = GlobalData.stepStructs[i];
            step_info.arrowTrans = m_arrowTrans[i].transform;
            GlobalData.stepStructs[i] = step_info;
        }
        m_Init = true;
    }

    private void FixedUpdate()
    {
        StateMachine();
    }

    private void OnEnable()
    {
        UnityEventCenter.AddListener(EnumDefine.EventKey.NotificationCallback, UpdateRealBody);
    }

    private void OnDisable()
    {
        UnityEventCenter.RemoveEventLister(EnumDefine.EventKey.NotificationCallback);
    }

    private void StateMachine()
    {
        if (m_State == GameState.Prepare && m_Tools.Count > 0)
        {
            // ���ģʽ�£����ݲ�ͬstring��ʵ�ֲ�ͬ���ߵ���˸
            if (m_Method == GameMethod.Clicked)
            {
                PrepareClickStep();
            }
            else
            {
                PrepareDragStep();
            }
        }
        else if (m_State == GameState.Playing && currToolName.Length > 0)
        {
            PerformThisStep();
        }
    }

    /// <summary>
    /// ÿ�β��������Ŀ���ǲ��Ŷ�����
    /// �����е����ϷŲ��Ŷ������е��ǵ�����Ŷ�����������Ҫ���ݲ�ͬ�������д��
    /// </summary>
    public void Prepare()
    {
        if (!m_Prepare)
        {
            string method = GlobalData.stepStructs?[GlobalData.StepIdx].method;
            //m_Tools.Clear();
            m_Tools = GlobalData.stepStructs[GlobalData.StepIdx].tools;
            //Debug.Log("Prepare: " + m_Tools.Count);
            if (method == "���")
            {
                m_Method = GameMethod.Clicked;
                //CameraControl.target.AddComponent<HighlightingEffect>();
                transform.AddComponent<ClickMethod>();
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

    /// <summary>
    /// ������һ�� ��ק/�����������Ϣ
    /// </summary>
    /// <param name="name"></param>
    public async void PerformThisStep()
    {
        if (m_ToolIdx < m_Tools.Count && currToolName == m_Tools[m_ToolIdx])
        {
            m_ToolIdx++;
            currToolName = "";
            if (m_ToolIdx >= m_Tools.Count)
            {
                GlobalData.totalScore += m_Score;
                float start = float.Parse(GlobalData.stepStructs[GlobalData.StepIdx].animLimite[0]);
                float end = float.Parse(GlobalData.stepStructs[GlobalData.StepIdx].animLimite[1]);
                await ModelAnimControl._Instance.PlayAnim(start, end); // ����������̲���Ķ���
            }
        }
    }

    public async UniTask UpdateArrowTrans()
    {
        if (GlobalData.stepStructs != null && GlobalData.stepStructs.Count > 0)
        {
            await UniTask.WaitUntil(() => m_Init == true);
            // Debug.Log("PrepareDragStep: " + GlobalData.stepStructs[GlobalData.StepIdx].arrowTrans.position);
            var new_trans = GlobalData.stepStructs[GlobalData.StepIdx].arrowTrans;

            // Debug.Log("UpdateArrowTrans(): " + new_trans.localPosition);
            m_Arrow.transform.localPosition = new_trans.localPosition;
            m_Arrow.transform.localRotation = new_trans.localRotation;
        }
    }

    private async void PrepareDragStep()
    {
        await UpdateArrowTrans();
        m_State = GameState.Playing; // ׼���׶ν�����������Ϸ�׶�
    }

    private void PrepareClickStep()
    {
        string tool = m_Tools[m_ToolIdx];
        GameObject go = GameObject.Find(tool);
        if (go != null)
        {
            //HighlightableObject ho = go.AddComponent<HighlightableObject>();
            //ho.FlashingOn(Color.green, Color.red, 2f);
        }
        m_State = GameState.Playing; // ׼���׶ν�����������Ϸ�׶�
    }

    public void ArrowActive(bool b)
    {
        m_Arrow.SetActive(b);
    }

    public void SetToolName(string name)
    {
        currToolName = name;
    }

    public void NextStep()
    {
        UnityEventCenter.DistributeEvent(EnumDefine.EventKey.NotificationCallback, null); // ����һ��ʵѵ���˳ɼ�body�ڴ�����
        if (GlobalData.StepIdx + 1 < GlobalData.stepStructs.Count)
        {
            GlobalData.StepIdx++;
            SetStep(GlobalData.StepIdx);
        }
    }

    // �û�����ѡ��ͬ�Ĳ��������Ϸ
    public async void SetStep(int i)
    {
        // Debug.Log("Step: " + i + " || " + GlobalData.stepStructs.Count);
        if (i >= 0 && i < GlobalData.stepStructs.Count)
        {
            GlobalData.StepIdx = i;
            m_Prepare = false;
            m_ToolIdx = 0;
            currToolName = "";

            float frame = float.Parse(GlobalData.stepStructs[i].animLimite[0]); // ����Ϊ�� ��ʾ��һ��������ģ�͵�״̬[ÿһ��ģ�Ͷ���ı�]
            await ModelAnimControl._Instance.Slice(frame, frame);
            Prepare();
        }
    }

    // Ŀǰ����Ҫ�������߲��ܼ����
    public string NumberOfToolsRemaining()
    {
        return (m_Tools.Count - m_ToolIdx).ToString();
    }

    private void UpdateRealBody(IMessage msg)
    {
        if (GlobalData.mode != Mode.Examination) { return; }
        List<AnswerDetailVoListItem> realList = new List<AnswerDetailVoListItem>();

        AnswerDetailVoListItem avi = new AnswerDetailVoListItem();
        avi.resourceId = GlobalData.codeVSidDic[GlobalData.currModuleCode];
        avi.userScore = GlobalData.totalScore.ToString();
        realList.Add(avi);

        GlobalData.m_RealExamBody = realList;
    }
}
