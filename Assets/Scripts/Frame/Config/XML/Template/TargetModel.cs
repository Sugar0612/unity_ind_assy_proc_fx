using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public struct Proj
{
    public string ProjName; //��Ŀ����
    public List<Target> targets; // ����Ŀ�б�
}

public struct Target
{
    public string modelName; // ���ڶ�Ӧ Addreass�е�Default���֣��Ӷ��첽������Դ
    public int modelCode; // ģʽ����
    public string menuName; // ������ʾ��UI�ϵ����֣��û�����ѡ��ͬ���������鲻ͬ�ĳ���
}


public class TargetModel : ConfigTemplate
{
    public List<Proj> m_Projs = new List<Proj>();

    public override void ReadXML(XDocument doc)
    {
        base.ReadXML(doc);
        var Projs = doc.Root.Elements("Item");
        foreach (var item in Projs)
        {
            Proj proj = new Proj();
            proj.ProjName = item.Attribute("ProjName").Value;

            List<Target> TargetList = new List<Target>();
            foreach (var target in item.Elements("Target"))
            {
                Target t = new Target();
                t.modelName = target.Attribute("ModelName").Value;
                t.modelCode = Convert.ToInt32(target.Attribute("ModelCode").Value);
                t.menuName = target.Attribute("MenuName").Value;
                TargetList.Add(t);
            }
            proj.targets = TargetList;
            m_Projs.Add(proj);
        }

        GlobalData.Projs.Clear();
        GlobalData.Projs = m_Projs;
        // �������ôд����ô m_Targets ������û��ʲô������...
        // ...�������뱣��m_Targets �����Ժ��õĵ�
    }
}
