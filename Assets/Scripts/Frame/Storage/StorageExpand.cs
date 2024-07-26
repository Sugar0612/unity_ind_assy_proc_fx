using LitJson;
using System.IO;
using UnityEngine;

public static class StorageExpand
{
    private static bool m_Init = false;

    private static StorageObject m_storage;
    public static StorageObject Storage
    {
        get
        {
            if (m_storage == null)
            {
                m_storage = Resources.Load("Storage/Clump") as StorageObject;
            }

            if (File.Exists(Application.streamingAssetsPath + "Storage.json") && !m_Init)
            {
                string s_json = File.ReadAllText(Application.persistentDataPath + "/Data.json");
                m_storage = JsonMapper.ToObject<StorageObject>(s_json);
                m_Init = true;
            }
            return m_storage;
        }
    }

    /// <summary>
    /// ���Ҷ�Ӧ������ ResourcesInfo
    /// </summary>
    /// <param name="code"></param>
    /// <param name="moduleName"></param>
    /// <returns></returns>
    public static ResourcesInfo FindRsInfo(string code, string moduleName, string relative)
    {
        ResourcesInfo info = Storage.rsCheck.Find((x) => x.id == code && x.moduleName == moduleName);
        return info == null ? new ResourcesInfo() { id = code, moduleName = moduleName, relaPath = relative } : info;
    }

    /// <summary>
    /// ���ͻ��˵���Դ�汾�Ƿ�ʱ���µ�
    /// </summary>
    /// <param name="cli_info"></param>
    public static ResourcesInfo GetThisInfoPkg(ResourcesInfo cli_info)
    {
        return Storage.rsCheck.Find((x) => { return (x.id == cli_info.id) && (x.moduleName == cli_info.moduleName); });
    }

    /// <summary>
    /// ��������ļ��İ汾��Ϣ
    /// </summary>
    /// <param name="relative"></param>
    public static void UpdateThisFileInfo(ResourcesInfo info)
    {
        int idx = Storage.rsCheck.FindIndex((x) => { return (x.id == info.id) && (x.moduleName == info.moduleName); });
        if (idx != -1)
        {
            Storage.rsCheck.RemoveAt(idx);
        }

        ResourcesInfo ri = new ResourcesInfo();
        ri.id = info.id;
        ri.moduleName = info.moduleName;
        ri.version_code = info.version_code;
        Storage.rsCheck.Add(ri);
        SaveToDisk();
    }

    /// <summary>
    /// �� ScriptableObject �����ݱ��浽Ӳ����
    /// </summary>
    public static void SaveToDisk()
    {
        // Debug.Log(Application.persistentDataPath);
        string s_json = JsonMapper.ToJson(Storage);
        File.WriteAllText(Application.persistentDataPath + "/Storage.json", s_json);
    }
}
