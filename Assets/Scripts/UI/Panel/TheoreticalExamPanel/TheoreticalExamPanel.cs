using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestionData;

public class TheoreticalExamPanel : BasePanel
{
    // �ڴ��
    private PoolList<TheoreticalExamQuestion> m_Pool = new PoolList<TheoreticalExamQuestion>();

    // �����б�
    private List<TheoreticalExamQuestion> m_quList = new List<TheoreticalExamQuestion>();

    // ģ��
    public GameObject m_Template;

    // ģ��ĸ��ؼ�
    public GameObject m_Parent;



    public override void Awake()
    {
        base.Awake();

        m_Pool.AddListener(Instance);
    }

    /// <summary>
    /// ��ʼ���������
    /// </summary>
    /// <param name="data"></param>
    public void Init(List<QuestionData> data)
    {
        for (int i = 0; i < data.Count; ++i)
        {
            TheoreticalExamQuestion qu_item = m_Pool.Create("QuestionItem_" + i);
            qu_item.Init(data[i]);
            m_quList.Add(qu_item);
        }
    }

    /// <summary>
    /// ���󴴽��ڴ�ķ���
    /// </summary>
    /// <returns></returns>
    public TheoreticalExamQuestion Instance()
    {
        GameObject go = GameObject.Instantiate(m_Template, m_Parent.transform);
        go.transform.localScale = Vector3.one;

        TheoreticalExamQuestion teq = go.GetComponent<TheoreticalExamQuestion>();
        return teq;
    }
}

/// <summary>
/// ����������(��ѡ��ѡ)
/// </summary>
public enum QuestionType
{
    Single = 0, Multiple, TrueOrFalse, Fill
}

/// <summary>
/// Options A-Z (ѡ����Ŀ)
/// </summary>
public enum OptionOrder
{
    A = 0, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NULL
}

// ��������
public class QuestionData
{
    //���
    public int number;

    public int ID;

    //��������
    public QuestionType type;

    //�����ı�
    public string text;

    //��
    public string answer;

    //����
    public string analyze;

    //����
    public float score;

    //ѡ��
    public List<OptionData> options = new List<OptionData>();

    /// <summary>
    /// ��ȡѡ����ѡ��
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static List<OptionData> GetOptions(string str)
    {
        List<OptionData> result = new List<OptionData>();
        string[] list = str.Split('_');
        if (list.Length <= 0) return result;

        for (int i = 0; i < list.Length; i++)
        {
            if (string.IsNullOrEmpty(list[i])) 
                continue;
            OptionData op = new OptionData();
            op.order = (OptionOrder)i;
            op.text = list[i];
            result.Add(op);
        }
        return result;
    }
}

/// <summary>
/// ѡ������
/// </summary>
public class OptionData
{
    //ѡ����
    public OptionOrder order;

    //ѡ���ı���ʾ
    public string text;
}
