using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sugar
{
    public enum Mode
    {
        None,
        Practice,
        Examinaion
    }

    public static class GlobalData
    {
        public static string IP = "127.0.0.1";
        public static string Port = "8096";
        public static string Url = $"http://{IP}:{Port}";

        public static void SetUrl(string _ip, string _port)
        {
            Url = $"http://{_ip}:{_port}";
        }

        public static string token;

        public static int SuccessCode = 200;//����ɹ�
        public static int ErrorCode = 500;//ҵ���쳣 
        public static int WarningCode = 601;//ϵͳ������Ϣ

        public static Mode mode = Mode.None;

        // �Ѿ���ɱ��ο��ˣ����µ�¼��ſ��ٴο���
        public static bool currentExamIsFinish = false;

        // ModuleList�����еĿؼ�
        public static GameObject TaskListPanel = null; // �����б����
        public static Button Task; // �����б��е�һ����Task���o
        public static Transform TaskParent = null; // �@ʾ���������б��o��Parent Transform
    }
}
