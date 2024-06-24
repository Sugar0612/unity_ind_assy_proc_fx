using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public struct Target
{
    public string modelName; // ���ڶ�Ӧ Addreass�е�Default���֣��Ӷ��첽������Դ
    public int modelCode; // ģʽ����
    public string menuName; // ������ʾ��UI�ϵ����֣��û�����ѡ��ͬ���������鲻ͬ�ĳ���
}


public class TargetModel : ConfigTemplate
{
    public List<Target> m_Targets = new List<Target>();

    public override void ReadXML(XDocument doc)
    {
        base.ReadXML(doc);
        var targets = doc.Root.Elements("Target");
        foreach (var target in targets )
        {
            Target t = new Target();
            t.modelName = target.Attribute("ModelName").Value;
            t.modelCode = Convert.ToInt32(target.Attribute("ModelCode").Value);
            t.menuName = target.Attribute("MenuName").Value;
            m_Targets.Add(t);
        }

        GlobalData.Targets.Clear();
        GlobalData.Targets = m_Targets;
        // �������ôд����ô m_Targets ������û��ʲô������...
        // ...�������뱣��m_Targets �����Ժ��õĵ�
    }
}
