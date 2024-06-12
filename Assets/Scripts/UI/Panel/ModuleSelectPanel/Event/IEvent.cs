using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModuleEventSpawn
{

    public static ModuleEvent Spawn<T>(string name, string code, MonoBehaviour mono) where T : ModuleEvent
    {
        try
        {
            Type t = Type.GetType(name, true);
            object @object = Activator.CreateInstance(t);
            ModuleEvent @event = @object as ModuleEvent;
            if (@event != null)
            {
                @event.m_Name = name;
                @event.m_Code = code;
                @event.m_mono = mono;
                return @event;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return null;
    }
}

public class ModuleEvent
{
    // ģʽ������
    public string m_Name;

    // ģʽ����
    public string m_Code;

    public MonoBehaviour m_mono;

    /// <summary>
    /// ���ÿ��ģʽ��ͬ���¼�
    /// </summary>
    /// <param name="module_name"></param>
    /// <param name="args"></param>
    public virtual void OnEvent(params object[] args) { }
}
