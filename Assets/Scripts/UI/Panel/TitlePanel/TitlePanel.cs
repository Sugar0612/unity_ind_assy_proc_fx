using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : BasePanel
{
    public Button m_Exit;

    public TextMeshProUGUI m_Title;

    public override void Awake()
    {
        base.Awake();
        m_Title.text = $"ģ������-{GlobalData.currModuleName}";
        m_Exit.onClick.AddListener(OnExitBtnClicked);
    }

    private void OnExitBtnClicked()
    {
        if (GlobalData.mode == Mode.Examination)
        {
            // TODO..����ģʽ���˳��ύ�ɿ�
            UITools.Loading("Menu", false);
            GlobalData.currentExamIsFinish = true;
        }
        else
        {
            UITools.Loading("Menu", false);
            //ɾ���Ѿ�ʵ������ģ������
            UnityEventCenter.DistributeEvent(EnumDefine.EventKey.DataRecycling, null);
        }
    }
}
