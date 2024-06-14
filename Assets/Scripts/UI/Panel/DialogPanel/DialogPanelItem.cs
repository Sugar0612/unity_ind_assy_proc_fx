using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanelItem : MonoBehaviour
{
    // ��ť
    public Button m_Button;

    // �ı���ʾ
    public List<TextMeshProUGUI> m_Text = new List<TextMeshProUGUI>();

    // ��ť����¼�
    private Action OnButtonClicked = () => { };

    private void Start()
    {
        m_Button.onClick.AddListener(() => {  OnButtonClicked(); });
    }

    public void UpdateData(string text)
    {
        m_Text.ForEach(a => a.text = text);
    }

    /// <summary>
    /// ����¼�
    /// </summary>
    public void AddListener(Action callback)
    {
        OnButtonClicked = callback;
    }


    /// <summary>
    /// ����
    /// </summary>
    public void Clear()
    {
        m_Text.ForEach(a => a.text = "");
    }
}
