using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TheoreticalExamQuestion : MonoBehaviour
{
    // ��������
    public QuestionData m_Data;

    // �����ı�
    public TextMeshProUGUI m_TextQuestion;

    // ������
    public GameObject m_ItemQuestion;

    //����ѡ��ģ��
    public GameObject m_TemplateOption;

    //ģ�常����
    public GameObject m_TemplateParent;

    // ���������С
    private int m_FontSizeQuestion;

    // �������������С[��ѡ or ��ѡ]
    private int m_FontSizeQuestionType;

    // ѡ���б�
    public List<TheoreticalExamOption> m_chooseList = new List<TheoreticalExamOption>();

    // ѡ������ڴ��
    private PoolList<TheoreticalExamOption> m_Pool = new PoolList<TheoreticalExamOption>();

    // ���ÿ����������Ĵ�
    private string m_Answer;

    private void Awake()
    {
        m_Pool.AddListener(Instance);

        m_FontSizeQuestion = (int)m_TextQuestion.fontSize;
        m_FontSizeQuestionType = m_FontSizeQuestion - 6;
    }

    // ������еĶ����ڴ����뷽��
    private TheoreticalExamOption Instance()
    {
        GameObject go = GameObject.Instantiate(m_TemplateOption, m_TemplateParent.transform);
        go.transform.localScale = Vector3.one;

        TheoreticalExamOption op = go.GetComponent<TheoreticalExamOption>();
        return op;
    }

    public void Init(QuestionData data)
    {
        m_Data = data;

        // �����ʼ��
        InitQuestion(data.number, data.type, data.text);

        for (int i = 0; i < data.options.Count; ++i)
        {
            OptionData item = data.options[i];
            TheoreticalExamOption op_item = m_Pool.Create("Item_Option" + item.order.ToString());
            op_item.transform.SetAsLastSibling();
            op_item.onToggle = OnItemToggle;
            op_item.Init(data.type, item); // ��ÿ��ѡ����г�ʼ��
            m_chooseList.Add(op_item); // ������������ÿ��ѡ��
        }
    }

    /// <summary>
    /// ���ѡ��toggle
    /// ������Ҵ�
    /// </summary>
    /// <param name="data"></param>
    private void OnItemToggle(OptionData data)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in m_chooseList)
        {
            sb.Append(item.m_Answer);
        }
        m_Answer = sb.ToString();
    }

    /// <summary>
    /// ��ʼ��ÿһ����Ŀ��Item�ؼ�����
    /// </summary>
    /// <param name="number"></param>
    /// <param name="type"></param>
    /// <param name="text"></param>
    private void InitQuestion(int number, QuestionType type, string text)
    {
        Debug.Log("Question: " + text);
        text = Tools.checkLength(text, 27);
        m_TextQuestion.text = string.Format($"{number}.{text}\n{GetQuestionType(type, m_FontSizeQuestionType)}");
        m_ItemQuestion.transform.SetAsLastSibling();
    }

    /// <summary>
    /// ��ȡ �������� ԭʼ�ı�
    /// eg: (�����Ƕ�ѡ��)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private string GetQuestionType(QuestionType type, int size)
    {
        return string.Format($"<color=#FF7900FF><size={size}>{QuestionTypeToString(type)}</size></color>");
    }

    private string QuestionTypeToString(QuestionType type)
    {
        if (type == QuestionType.Single)
            return "������ѡ���⣩";
        else if (type == QuestionType.Multiple)
            return "������ѡ���⣩";
        else if (type == QuestionType.TrueOrFalse)
            return "���ж��⣩";
        else if (type == QuestionType.Fill)
            return "������⣩";
        else
            return "";
    }
}
