using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuGrid : MonoBehaviour
{
    private SubMenuItem m_Item;
    private SubMenuDragItem m_DragItem;
    private List<string> m_Tools = new List<string>(); // �����Ѿ����ɹ��Ĺ�������

    public Action<string, int> OnSubBtnClicked = (a, b) => { };

    public void Init(List<StepStruct> subMenus, bool drag = false)
    {
        if (!drag)
        {

        }
        else
        {
            if (m_DragItem == null)
            {
                m_DragItem = transform.GetChild(1).GetComponent<SubMenuDragItem>();
            }

            // ÿ�����ߙ���Item�Č���������SubMenus������Ҫ�õ��Ĺ�����Ϣ
            foreach(var subMenu in subMenus)
            {
                //Debug.Log("subMenuGrid: " + subMenu.subMenuName);
                foreach (var tool in subMenu.tools)
                {
                    if (m_Tools.Contains(tool))
                    {
                        continue; // ����������Ѿ����ɹ���������� ��ô����������
                    }
                    SubMenuDragItem drag_item = Instantiate(m_DragItem, this.transform);
                    drag_item.gameObject.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Textures/Tools/" + tool);
                    drag_item.gameObject.SetActive(true);
                    drag_item.Init(tool);
                    m_Tools.Add(tool);
                }
            }
        }
    }
}
