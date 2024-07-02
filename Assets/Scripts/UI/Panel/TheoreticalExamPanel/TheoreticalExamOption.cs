using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheoreticalExamOption : MonoBehaviour
{
    // ѡ������
    public OptionData m_Data;

    // ��ť����
    public Toggle m_Toggle;

    // ��ť������
    // ���ڵ�ѡ����ѡ��ֻ����һ����ison��������ҪToggleGroup���п���
    public ToggleGroup m_Group;

    //��ʾ��Ŀ�ı�
    public TextMeshProUGUI m_Text;

    // �û�ѡ��Ĵ�
    public string m_Answer { get { return m_Toggle.isOn ? m_Data.order.ToString() : ""; } }

    // ��������
    public QuestionType m_Type;



    public Action onToggle = () => { };

    public void Init(QuestionType type, OptionData data)
    {
        m_Type = type;
        m_Data = data;

        // ���� UI״̬/����
        UpdateToggle(type);
        UpdateText(data.order, data.text);

        m_Toggle.onValueChanged.AddListener(OnToggleClick);
    }

    /// <summary>
    /// ����ѡ��ť��״̬
    /// </summary>
    public void UpdateToggle(QuestionType type)
    {
        // ����Ƕ�ѡ����ô����Ҫ���뵽Group��ȥ
        if (type == QuestionType.Multiple)
        {
            m_Toggle.group = null;
        }
        else
        {
            m_Toggle.group = m_Group;
        }
    }

    /// <summary>
    /// ������Ŀ�ı�������
    /// </summary>
    public void UpdateText(OptionOrder order, string text)
    {
        text = Tools.checkLength(text, 26);
        m_Text.text = $"{order}.{text}";
    }

    /// <summary>
    /// ����Toggle�Ƿ�ɽ���
    /// </summary>
    /// <param name="b"></param>
    public void AllToggleActive(bool b)
    {
        m_Toggle.interactable = b;
    }

    /// <summary>
    /// Toggle�ؼ��������
    /// </summary>
    /// <param name="b"></param>
    public void OnToggleClick(bool b)
    {
        onToggle();
    }

    /// <summary>
    /// �˳�����
    /// </summary>
    public void Clear()
    {
        onToggle = () => { };
        m_Group = null;
        m_Toggle.isOn = false;
    }
}
