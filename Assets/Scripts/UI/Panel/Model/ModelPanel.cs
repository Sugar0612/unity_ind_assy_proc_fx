using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ModelPanel : BasePanel
{

    public GameObject m_EquiItem; // �����ť

    public Transform m_EquiItemParent; // �����

    public TextMeshProUGUI m_DescriptionText; // ͼ��

    public Button m_RevertBtn; // ���ð�ť

    public Action<string> onClickPart = (str) => { };

    public Action onClickRevert = () => { };

    public void Init(List<string> name)
    {
        SpawnItem(name);
        m_RevertBtn.onClick.AddListener(() => { onClickRevert?.Invoke(); });
    }

    public void SpawnItem(List<string> items)
    {
        foreach (var item in items)
        {
            //Debug.Log("====================SpawnItem: " + item);
            GameObject go = GameObject.Instantiate(m_EquiItem, m_EquiItemParent);
            go.gameObject.SetActive(true);

            Sprite spr = Resources.Load<Sprite>($"Textures/Tools/{item}");
            if (null == spr)
            {
                spr = Resources.Load<Sprite>("Textures/NotFoundTips/NotFoundImage");
            }

            go.GetComponent<Image>().sprite = spr;
            go.GetComponentInChildren<TextMeshProUGUI>().text = item;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                SetDescription(item);
                onClickPart?.Invoke(item);
            });
        }
    }

    public void SetDescription(string name)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Word/Tools/{name}");
        if (null == textAsset)
        {
            m_DescriptionText.text = "�����ļ���ʧ������ϵ��̨����Ա";
        }
        else
        {
            m_DescriptionText.text = textAsset.text;
        }
    }
}
