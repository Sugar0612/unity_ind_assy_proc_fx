using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestionData;

public class TheoreticalExamPanel : BasePanel
{
    // 内存池
    private PoolList<TheoreticalExamQuestion> m_Pool = new PoolList<TheoreticalExamQuestion>();

    // 问题列表
    private List<TheoreticalExamQuestion> m_quList = new List<TheoreticalExamQuestion>();

    // 模板
    public GameObject m_Template;

    // 模板的父控件
    public GameObject m_Parent;



    public override void Awake()
    {
        base.Awake();

        m_Pool.AddListener(Instance);
    }

    /// <summary>
    /// 初始化问题面板
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
    /// 对象创建内存的方法
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
/// 考试题类型(多选单选)
/// </summary>
public enum QuestionType
{
    Single = 0, Multiple, TrueOrFalse, Fill
}

/// <summary>
/// Options A-Z (选择数目)
/// </summary>
public enum OptionOrder
{
    A = 0, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NULL
}

// 试题数据
public class QuestionData
{
    //编号
    public int number;

    public int ID;

    //问题类型
    public QuestionType type;

    //问题文本
    public string text;

    //答案
    public string answer;

    //解析
    public string analyze;

    //分数
    public float score;

    //选项
    public List<OptionData> options = new List<OptionData>();

    /// <summary>
    /// 获取选择题选项
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
/// 选项数据
/// </summary>
public class OptionData
{
    //选项编号
    public OptionOrder order;

    //选项文本显示
    public string text;
}
