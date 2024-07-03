using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ConfigTemplate
{
    // ���
    public int id;

    // ����
    public string name;

    // xml �ĵ�
    public XDocument doc;

    // xml ·��
    public string path;

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="path"></param>
    public async void ReadXML(string path)
    {
        await NetworkManager._Instance.DownLoadTextFromServer(path, (text) =>
        {
            XDocument doc = XDocument.Parse(text);
            this.path = path;
            this.doc = doc;
            ReadXML(doc);
        });
    }

    /// <summary>
    /// ������ͬ��xml�ļ������洢���ڴ���
    /// </summary>
    /// <param name="doc"></param>
    public virtual void ReadXML(XDocument doc)
    {

    }
}
