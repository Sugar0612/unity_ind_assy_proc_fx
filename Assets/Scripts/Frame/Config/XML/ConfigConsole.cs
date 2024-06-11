using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigConsole : Singleton<ConfigConsole>
{
    public static string m_RootPath;

    //�����ļ��б�
    public Dictionary<string, ConfigTemplate> m_ConfigList = new Dictionary<string, ConfigTemplate>();

    // �Ƿ��ѽ����d����
    private bool m_Loaded = false;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        if (!m_Loaded)
        {
            m_RootPath = Application.streamingAssetsPath;
        }
    }

    public void LoadConfig(string root_path)
    {
        if (m_Loaded)
            return;

        // Web���xȡ

    }

    /// <summary>
    /// ͨ�^��ͫ@ȡ�����ļ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T FindConfig<T>() where T : ConfigTemplate
    {
        ConfigTemplate template = FindConfig(typeof(T).ToString());
        return template == null ? null : template as T;
    }

    public ConfigTemplate FindConfig(string name)
    {
        if (m_ConfigList.ContainsKey(name))
        {
            return m_ConfigList[name];
        }
        else
        {
            return null;
        }
    }
}
