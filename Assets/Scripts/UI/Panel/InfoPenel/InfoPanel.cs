using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : BasePanel
{
    public Button m_View; // �����ť��ʾʩ����ʾ���
    public Button m_Audio; // ������ť
    public Button m_Step; // ����չʾ��ť
    public TextMeshProUGUI m_StepText; // �������Text
    public TextMeshProUGUI m_Count; // ��Ҫ���빤������
    public TextMeshProUGUI m_IntroduceText; // ʩ��Ҫ�����Text

    public GameObject m_Introduce; // ʩ��Ҫ�����
    // public GameObject m_StepPanel; // ����ѡ�����
    public GameObject m_Hint; // ��ʾ���
    public GameObject m_StepHint; // ������ʾ���
    // public GameObject m_Minmap; // С��ͼ

    public static InfoPanel _instance;

    // public bool m_showMap = true; // �Ƿ�չʾС��ͼ

    public override void Awake()
    {
        base.Awake();
        _instance = this;
        Active(false);
    }

    private void Start()
    {
        //Debug.Log("m_IntroduceText: " + m_IntroduceText.text);
        m_View.onClick.AddListener(() =>
        {
            ActiveConstructionPanel();
        });

        m_Step.onClick.AddListener(() =>
        {
            // ActiveStepPanel();
        });

        Init();
    }

    private void FixedUpdate()
    {
        // m_Count.text = GameMode.Instance.NumberOfToolsRemaining();
        UpdateInfo();
    }

    private void Init()
    {
        m_Introduce?.gameObject.SetActive(false);
    }

    private void UpdateInfo()
    {
        if (GlobalData.StepIdx >= 0 && GlobalData.StepIdx < GlobalData.stepStructs.Count)
        {
            m_StepText.text = ModelAnimControl._Instance.m_ConPtStep?[GlobalData.StepIdx].step;
            m_IntroduceText.text = ModelAnimControl._Instance.m_ConPtStep?[GlobalData.StepIdx].constrPt;
        }
    }

    private void ActiveConstructionPanel()
    {
        bool b = m_Introduce.gameObject.activeSelf;
        m_Introduce.gameObject.SetActive(!b);
    }

    // ʵѵģʽ����һЩ����
    public void TrainingModeUIClose()
    {
        m_Audio.gameObject.SetActive(false);
        m_StepHint.gameObject.SetActive(false);
        m_Hint.gameObject.SetActive(false);
    }
}
