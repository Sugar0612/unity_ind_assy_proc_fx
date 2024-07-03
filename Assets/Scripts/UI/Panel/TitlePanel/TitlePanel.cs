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
        CameraControl.SetMain();
        if (GlobalData.isLoadModel)
        {
            //Debug.Log("Exit: " + GlobalData.currModuleName);
            CameraControl.SetMain();
            GlobalData.DestroyModel = true;
            GlobalData.StepIdx = 0;
        }

        if (GlobalData.mode == Mode.Examination)
        {
            // TODO..����ģʽ���˳��ύ�ɿ�
            UITools.Loading("Menu");
            GlobalData.currentExamIsFinish = true;
        }
        else
        {
            UITools.Loading("Menu");
        }
    }
}
