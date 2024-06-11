using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSelectPanel : BasePanel
{
    public GameObject m_Item; // ģʽ�x���Item���o

    public Button m_Exit; // �˳����o

    public GameObject m_TaskListPanel; // �����΄��б���
    public Button m_CloseTask; // ���˴����P�]���o
    public Transform m_ParentTrans; // �����б��еİ��o������Parent
    public Button m_Task; // �����б��еİ��o

    private ConfigModuleList m_ModuleList;

    public override void Awake()
    {
        base.Awake();

        m_Exit.onClick.AddListener(UITools.Quit);
        m_CloseTask.onClick.AddListener(CloseTaskPanel);
    }

    private void Start()
    {
        m_ModuleList = ConfigConsole.Instance.FindConfig<ConfigModuleList>();
    }

    public void CloseTaskPanel()
    {
        for (int i = 0; i < m_ParentTrans.childCount; i++)
        {
            m_ParentTrans.GetChild(i).gameObject.SetActive(false);
        }
        m_TaskListPanel.SetActive(false);
    }
}
