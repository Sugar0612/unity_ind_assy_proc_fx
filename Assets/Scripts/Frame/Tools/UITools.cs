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
        MessagePanel panel = FindAssetPanel<MessagePanel>();
        panel.Show(mess, duration);
    }

    /// <summary>
    /// ���dscene������֮ǰ߀Ҫ�@ʾһ�����d�е�UI��
    /// </summary>
    /// <param name="scene">��Ҫ�@ʾ�Ĉ���</param>
    /// <param name="real"> ���real��True�������dģ�͈��������ڮ������dUI���� </param>
    /// <param name="model_name"></param>
    public static void Loading(string scene, bool real = true, string model_name = "")
    {
        LoadingPanel load_panel = FindAssetPanel<LoadingPanel>();
        load_panel.LoadScene(scene, model_name, real);
    }

    public static void OpenDialog(string title, string info, Action callback)
    {
        DialogPanel.OpenDialog(title, info, callback);
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


    /// <summary>
    /// UI�YԴ�ļ��d
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T FindAssetPanel<T>() where T : BasePanel
    {
        T t = UIConsole.Instance.FindPanel<T>();
        if (t == null)
        {
            string path = "Prefabs/UI/Panel/" + typeof(T).ToString();
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(path));
            if (go != null)
            {
                t = go.GetComponent<T>();
                go.name = typeof(T).Name;
                if (t is IGlobalPanel)
                {
                    // ȫ��UI���d
                    go.transform.SetParent(GlobalCanvas.Instance.transform);
                }
                else
                {
                    // ����UI���d
                    go.transform.SetParent(GameObject.Find("Canvas/").transform);
                }
                go.transform.localScale = Vector3.one;
                RectTransform rect = go.transform as RectTransform;
                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
            }
        }
        return t;
    }
}

