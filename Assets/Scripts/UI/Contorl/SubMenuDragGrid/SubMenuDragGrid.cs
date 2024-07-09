using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuDragGrid : MonoBehaviour
{
    public SubMenuGrid m_Tool;
    public SubMenuGrid m_Material;

    public Button m_ToolBtn;
    public Button m_MaterialBtn;

    private EquStatus m_equStatus = EquStatus.TOOL;

    void Start()
    {
        SwitchLeafPanel();
        m_ToolBtn.onClick.AddListener(OnClickTool);
        m_MaterialBtn.onClick.AddListener(OnClickMaterial);
    }

    /// <summary>
    /// �л���ͬ�ķ�ҳ
    /// </summary>
    private void SwitchLeafPanel()
    {
        // Debug.Log(m_equStatus);
        if (m_equStatus == EquStatus.TOOL)
        {
            m_Tool.gameObject.SetActive(true);
            m_Material.gameObject.SetActive(false);
        }
        else
        {
            m_Tool.gameObject.SetActive(false);
            m_Material.gameObject.SetActive(true);
        }
    }

    private void OnClickTool()
    {
        m_equStatus = EquStatus.TOOL;
        SwitchLeafPanel();
    }

    private void OnClickMaterial()
    {
        m_equStatus = EquStatus.MATERIAL;
        SwitchLeafPanel();
    }

    // ��ʾĿǰ��ʾ���� ���ߴ��ڻ��ǲ��ϴ���
    public enum EquStatus
    {
        TOOL,
        MATERIAL,
        NONE
    }
}
