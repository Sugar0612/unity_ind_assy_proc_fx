using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DownLoadPanel : BasePanel
{
    public Slider m_ProgressSlider; // �M�ȗl
    public TextMeshProUGUI m_ProgressPercent; // �@ʾ�M�ȗl�ٷֱ�
    public TextMeshProUGUI m_Hint; // ��ʾ�ı�
    public Button m_Finish; // ��ɰ�ť

    public static DownLoadPanel _instance;

    public override void Awake()
    {
        base.Awake();

        _instance = this;

        m_ProgressSlider.onValueChanged.AddListener((value) => 
        {
            Debug.Log("m_ProgressSlider.onValueChanged");

        });

        m_Finish.onClick.AddListener(() => 
        {
            Debug.Log("Go Finish~");
            Active(false);
        });

        Active(false);
        m_Finish.enabled = false;
    }

    /// <summary>
    /// ���ý������ٷֱ�
    /// </summary>
    /// <param name="percent"></param>
    public void SetPercent(float percent)
    {   
        m_ProgressSlider.value = percent / 100.0f;
        m_ProgressPercent.text = $"{percent}%";

        if (m_ProgressSlider.value >= 1.0f)
        {
            m_Hint.text = $"������ɣ�";
            m_Finish.enabled = true;
        }
        else
        {
            m_Hint.text = $"���ڸ�����Դ...";
            m_Finish.enabled = false;
        }
    }
}
