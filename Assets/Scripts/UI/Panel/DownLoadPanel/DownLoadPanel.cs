using Cysharp.Threading.Tasks;
using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownLoadPanel : BasePanel
{
    public Slider m_ProgressSlider; // �M�ȗl
    public TextMeshProUGUI m_ProgressPercent; // �@ʾ�M�ȗl�ٷֱ�
    public TextMeshProUGUI m_Hint; // ��ʾ�ı�
    public Button m_Finish; // ��ɰ�ť

    [HideInInspector]
    public bool m_Finished; // �˴θ���ȫ�����

    [HideInInspector]
    public List<string> m_NeedDL = new List<string>(); // ��Ҫ�������ظ��µ��ļ�

    [HideInInspector]
    public List<FilePackage> m_NeedWt = new List<FilePackage>(); // ��Ҫд�뵽���ص��ļ�

    public static DownLoadPanel _instance;

    private float m_Percent; // �ܽ���
    private float m_bufPercent; // �����ļ��Ľ���

    public override void Awake()
    {
        base.Awake();

        _instance = this;
        m_Finished = false;
        m_Finish.onClick.AddListener(() => 
        {
            Debug.Log("Go Finish~");
            Active(false);

            m_bufPercent = 0.0f;
            m_Percent = 0.0f;
            m_Finished = true;
        });

        Active(false);
        m_Finish.enabled = false;
    }

    /// <summary>
    /// ���ý������ٷֱ�
    /// </summary>
    /// <param name="percent"></param>
    public void SetDLPercent(float percent)
    {
        Debug.Log($"=========== {m_Percent} || {m_bufPercent} || {percent}");
        m_Percent = m_bufPercent + (percent / (float)m_NeedDL.Count) * 0.9f;
        if (percent >= 100.0f)
        {
            m_bufPercent = m_Percent;
        }

        m_ProgressSlider.value = m_Percent / 100.0f;
        m_ProgressPercent.text = m_Percent.ToString("f1") + "%";

        if (m_ProgressSlider.value >= 0.9f)
        {
            m_Hint.text = $"�ȴ��ļ�д�뵽����...";
        }
        else
        {
            m_Hint.text = $"����������Դ...";
            m_Finish.enabled = false;
            m_Finished = false;
        }
    }

    /// <summary>
    /// ����д���ļ�ʱ�Ľ�����
    /// </summary>
    /// <param name="percent"></param>
    public void SetWritePercent(float percent)
    {
        m_Percent = m_bufPercent + percent / 10.0f / (float)m_NeedWt.Count;
        // Debug.Log("=========== SetWritePercent: " + m_Percent);

        if (percent >= 100.0f)
        {
            m_bufPercent = m_Percent;
        }

        m_ProgressSlider.value = m_Percent / 100.0f;
        m_ProgressPercent.text = m_Percent.ToString("f1") + "%";

        if (m_ProgressSlider.value >= 1.0f)
        {
            Debug.Log("������ɣ�");
            m_Hint.text = $"������ɣ�";
            m_Finish.enabled = true;
        }
    }

    /// <summary>
    /// ���
    /// </summary>
    public void Clear()
    {
        m_NeedWt.Clear();
        m_NeedDL.Clear();
        m_Finished = false;
        m_Percent = 0.0f;
        m_bufPercent = 0.0f;

        GlobalData.Checked = false;
        GlobalData.Downloaded = false;
        GlobalData.IsLatestRes = false;
    }
}