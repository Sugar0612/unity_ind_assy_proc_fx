using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public struct Target
{
    public string modelName;
    public int modelCode;
}


public class TargetModel : ConfigTemplate
{
    public Target m_Target;

    public override void ReadXML(XDocument doc)
    {
        base.ReadXML(doc);
        var target = doc.Root.Element("Target");
        m_Target.modelName = target.Attribute("ModelName").Value;
        m_Target.modelCode = Convert.ToInt32(target.Attribute("ModelCode").Value);
        GlobalData.ModelTarget = m_Target;
        // �������ôд����ô m_Target ������û��ʲô������...
        // ...�������뱣��m_Target �����Ժ��õĵ�
    }
}
