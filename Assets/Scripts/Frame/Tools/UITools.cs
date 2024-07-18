using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UITools
{
    /// <summary>
    /// ��ʾ���ڼ��d
    /// </summary>
    /// <param name="mess"></param>
    /// <param name="duration"></param>
    public static void ShowMessage(string mess, float duration = 3.0f)
    {
        MessagePanel panel = UIConsole.Instance.FindAssetPanel<MessagePanel>();
        panel.Show(mess, duration);
    }

    /// <summary>
    /// ���dscene������֮ǰ߀Ҫ�@ʾһ�����d�е�UI��
    /// </summary>
    /// <param name="scene">��Ҫ�@ʾ�Ĉ���</param>
    /// <param name="real"> ���real��True�������dģ�͈��������ڮ������dUI���� </param>
    /// <param name="model_name"></param>
    public static void Loading(string scene, string model_name = "")
    {
        LoadingPanel load_panel = UIConsole.Instance.FindAssetPanel<LoadingPanel>();
        load_panel.LoadScene(scene, model_name);
    }

    public static void OpenDialog(string title, string info, Action callback, bool single = false)
    {
        DialogPanel.OpenDialog(title, info, callback, single);
    }

    /// <summary>
    /// �P�]�Α�
    /// </summary>
    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

