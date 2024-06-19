using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Button m_View; // �����ť��ʾʩ����ʾ���
    public Button m_Audio; // ������ť
    public TextMeshProUGUI m_StepText; // �������Text
    public TextMeshProUGUI m_Count; // ��Ҫ���빤������
    public GameObject m_Introduce; // ʩ��Ҫ�����

    private TextMeshProUGUI m_IntroduceText; // ʩ��Ҫ�����Text

    private void Awake()
    {
        m_IntroduceText = GameObject.Find("InstroduceTx").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //Debug.Log("m_IntroduceText: " + m_IntroduceText.text);
        m_View.onClick.AddListener(() =>
        {
            ActiveConstructionPanel();
        });

        Init();
    }

    private void FixedUpdate()
    {
        m_Count.text = GameMode.Instance.NumberOfToolsRemaining();
        UpdateInfo();
    }

    private void Init()
    {
        
    }

    private void UpdateInfo()
    {
        m_StepText.text = ModelAnimControl._Instance.m_ConPtStep?[GlobalData.StepIdx].step;
        m_IntroduceText.text = ModelAnimControl._Instance.m_ConPtStep?[GlobalData.StepIdx].constrPt;
    }

    private void ActiveConstructionPanel()
    {
        bool b = m_Introduce.gameObject.activeSelf;
        m_Introduce.gameObject.SetActive(!b);
    }
}
