using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : BasePanel
{
    // �����
    public PoolList<DialogPanelItem> m_Pool = new PoolList<DialogPanelItem>();

    // Item �б�
    public List<DialogPanelItem> m_List = new List<DialogPanelItem>();

    // Item ����ϵİ�ť
    public GameObject m_DialogItem;

    // Item �ĸ�����(ItemӦ��Clone���Ǹ����������)
    public GameObject m_ItemParent;

    // ����
    public TextMeshProUGUI m_Title;

    // ��Ϣչʾ
    public TextMeshProUGUI m_Info;

    // ����
    public Image m_BG;

    public override void Awake()
    {
        base.Awake();

        m_Pool.AddListener(OnInstanceItem);
    }

    /// <summary>
    /// ʵ����һ�� DialogPanelItem ���͵Ķ���
    /// </summary>
    /// <returns></returns>
    public DialogPanelItem OnInstanceItem()
    {
        GameObject go = GameObject.Instantiate(m_DialogItem);
        go.transform.parent = m_ItemParent.transform;
        go.transform.localScale = Vector3.one;
        
        DialogPanelItem item = go.GetComponent<DialogPanelItem>();
        return item;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="title"></param>
    /// <param name="info"></param>
    /// <param name="list"></param>
    public void UpdateData(string title, string info, params ButtonData[] list)
    {
        m_Title.text = title;
        m_Info.text = info;
        Clear();

        foreach (ButtonData d in list)
        {
            DialogPanelItem item = m_Pool.Create("Btn-" + d.text);
            item.transform.SetAsLastSibling();
            item.UpdateData(d.text);
            if (d.isClose)
            {
                item.AddListener(() =>
                {
                    this.Active(false);
                    d.method();
                });
            }
            else
            {
                item.AddListener(d.method);
            }

            m_List.Add(item);
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public void Clear()
    {
        foreach (var item in m_List)
        {
            item.Clear();
            m_Pool.Destroy(item);
        }
        m_List.Clear();
    }

    /// <summary>
    /// �򿪶Ի���
    /// </summary>
    /// <param name="title"></param>
    /// <param name="info"></param>
    /// <param name="callback"></param>
    public static void OpenDialog(string title, string info, Action callback)
    {
        DialogPanel panel = UITools.FindAssetPanel<DialogPanel>();
        if (panel != null)
        {
            panel.UpdateData(title, info, new ButtonData("ȷ��", callback), "ȡ��");
            panel.Active(true);
        }
        else
        {
            Debug.LogError("�Ի�����岻����");
        }
    } 
}

public class ButtonData
{
    // �ı���ʾ
    public string text;

    // ����
    public Action method;

    // �����ر����
    public bool isClose;

    public ButtonData(string txt, bool close = true)
    {
        this.text = txt;
        this.isClose = close;
        this.method = () => { };
    }

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="m"></param>
    public ButtonData(string txt, Action m, bool close = true)
    {
        this.text = txt;
        this.method = m;
        this.isClose = close;
    }

    /// <summary>
    /// �ַ�����ʽת��
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static implicit operator ButtonData(string txt)
    {
        return new ButtonData(txt);
    }
}
