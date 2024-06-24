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
        Examination
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
        public static int examId; // ����id

        public static int SuccessCode = 200;//����ɹ�
        public static int ErrorCode = 500;//ҵ���쳣 
        public static int WarningCode = 601;//ϵͳ������Ϣ

        public static Mode mode = Mode.None;
        public static string currModuleCode = "";
        public static string currModuleName = "";
        public static List<Target> Targets = new List<Target>(); // ѵ��/ʵѵ���˵�ģ�ͳ��� Addressables Groups Default Name ���б�.
        public static Target ModelTarget; // ѵ��/ʵѵ���˵�ģ�ͳ��� Addressables Groups Default Name.

        // �Ѿ���ɱ��ο��ˣ����µ�¼��ſ��ٴο���
        public static bool currentExamIsFinish = false;

        // ModuleList�����еĿؼ�
        public static GameObject TaskListPanel = null; // �����б����
        public static Button Task; // �����б��е�һ����Task���o
        public static Transform TaskParent = null; // �@ʾ���������б��o��Parent Transform

        /// <summary>
        /// ��ѡ���꿼���б���������Լ�ģ�����Ϣ�����ڴ�������...
        /// ...��ϰģʽ�� ͨ��ģ���Ӧ��SoftwareID��ȡ���⣬ ��һ������Ϊģ����� �ڶ�������Ϊ SoftwareID
        /// </summary>
        public static ExamJsonData examData;

        // �Y��惦���ǲ�ͬ�Ŀģʽ�����ֺ�̖�a
        //public static List<string[]> moduleContent = new List<string[]> { new string[] {"", "10022"} }; 

        public static List<string> FinishExamModule = new List<string>(); //ӛ��ѽ���ɵĿ�������

        public static int StepIdx = 0; // ��������
        public static List<StepStruct> stepStructs = new List<StepStruct>(); // ����������Ϣ
        public static bool canClone = false; // ��stepStructesˢ�� ��ʾ����������UI��[SelectStepPanel]���Զ�stepStructes����Clone
    }
}
