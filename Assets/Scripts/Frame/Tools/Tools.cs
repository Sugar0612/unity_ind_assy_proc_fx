using Cysharp.Threading.Tasks;
using LitJson;
using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class Tools
{
    // ��תӢ�ֵ�
    public static Dictionary<string, string> EscDic = new Dictionary<string, string>{ { @"��ѧ", "TeachingEvent"}, {@"ѵ��", "TrainEvent"},
        {@"����", "AssessEvent"}, 
     {@"�̰�", "PDFAction"}, {@"ͼֽ", "PDFAction"},{@"����", "PDFAction"},{@"�淶", "PDFAction"}, {@"ͼƬ", "PictureAction"}, 
     {@"����", "VideoAction"},{@"��Ƶ", "VideoAction"}, {@"����", "ModelAction"}, {@"����", "TheoreticalExamAction"}, {@"ʵ��", "TrainingAction"} };

    // ��ͬ��ģʽ��Ӧ��ͬ���ļ�·��
    // (û��д���������ģʽ��Ҫô�������ȡ��Ҫô�Ǵ�Addressabels�л�ȡ)
    private static Dictionary<string, string> FileDic = new Dictionary<string, string> 
    {
        {@"�̰�", FPath.JiaoAnSuffix}, {@"ͼֽ", FPath.TuZhiSuffix}, {@"����", FPath.FangAnSuffix}, {@"�淶", FPath.GuiFanSuffix},
        {@"ͼƬ", FPath.PictureSuffix}, {@"����", FPath.AnimSuffix}, {@"��Ƶ", FPath.VideoSuffix}
    };

    public static bool CheckMessageSuccess(int code)
    {
        return code == GlobalData.SuccessCode;
    }

    /// <summary>
    /// ��̬������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    static public T CreateObject<T>(string name) where T : class
    {
        object obj = CreateObject(name);
        return obj == null ? null : obj as T;
    }

    /// <summary>
    /// ��̬������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static object CreateObject(string name)
    {
        object obj = null;
        try
        {
            Type type = Type.GetType(name, true);
            obj = Activator.CreateInstance(type); //����ָ�����͵�ʵ����
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
        return obj;
    }

    public static string LocalPathEncode(string txt)
    {
        return txt;
    }


    /// <summary>
    /// Cn To En Or En To Cn
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string Escaping(string name)
    {
        string TypeName = "";
        if (EscDic.ContainsKey(name))
        {
            TypeName = EscDic[name];
            if (!EscDic.ContainsKey(TypeName))
            {
                EscDic.Add(TypeName, name);
            }
        }
        return TypeName;
    }

    /// <summary>
    /// ��ȡ��ͬ��ģʽ��Ӧ�Ĳ�ͬ�ļ�·��
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetModulePath(string name)
    {
        string path = "";
        if (FileDic.ContainsKey(name))
        {
            path = FileDic[name];
        }
        return path;
    }

    /// <summary>
    /// Object -> Texture2D -> Sprite
    /// </summary>
    /// <returns>The convert.</returns>
    /// <param name="tex">Tex.</param>
    public static Sprite SpriteConvert(Texture2D tex)
    {
        Sprite sprite;
        sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero, 100f);
        return sprite;
    }

    /// <summary>
    /// ����ַ������ȣ���ҪʱҪ����
    /// </summary>
    /// <param name="text"></param>
    /// <param name="max_len"></param>
    /// <returns></returns>
    public static string checkLength(string text, int max_len)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < text.Length; ++i)
        {
            if (i != 0 && (i % max_len) == 0)
            {
                sb.Append('\n');
            }
            sb.Append(text[i]);
        }
        return sb.ToString();
    }


    /// <summary>
    /// ģ�ͳ����첽����
    /// </summary>
    /// <returns></returns>
    public static async UniTask LoadSceneModel()
    {
        GlobalData.DestroyModel = false;
        await LoadModel();
    }

    /// <summary>
    /// ģ�ͼ���
    /// </summary>
    /// <returns></returns>
    private static async UniTask LoadModel()
    {
        // ģ�ͳ����첽����       
        AsyncOperationHandle<GameObject> model_async = Addressables.LoadAssetAsync<GameObject>(GlobalData.ModelTarget.modelName);
        await UniTask.WaitUntil(() => model_async.IsDone == true);

        UnityEngine.Object.Instantiate(model_async.Result);
        await AnalysisStepFile();
    }

    /// <summary>
    /// ����StepFile��ȡ����
    /// TODO.. ������дһ���µ��ļ�ר�ŷ�����Ҫ�Ĺ��ߺͲ���
    /// </summary>
    private static async UniTask AnalysisStepFile()
    {
        await NetworkManager._Instance.DownLoadTextFromServer((Application.streamingAssetsPath + "/ModelExplain/" + GlobalData.ModelTarget.modelName + "Step.txt"), (dataStr) =>
        {
            //Debug.Log(dataStr);
            List<StepStruct> list = new List<StepStruct>();
            JsonData js_data = JsonMapper.ToObject(dataStr);
            JsonData step = js_data["child"];
            for (int i = 0; i < step.Count; i++)
            {
                StepStruct step_st = new StepStruct();
                string[] field = step[i].ToString().Split("_");
                if (field.Length == 4)
                {
                    step_st.method = field[0];
                    step_st.tools = new List<string>(field[1].Split("|"));
                    step_st.stepName = field[2];
                    step_st.animLimite = new List<string>(field[3].Split("~"));
                }
                else
                {
                    step_st.tools = new List<string>(field[0].Split("|"));
                    step_st.stepName = field[1];
                    step_st.animLimite = new List<string>(field[2].Split("~"));
                }
                list.Add(step_st);
            }

            // ������Ϣ����
            GlobalData.stepStructs.Clear();
            GlobalData.stepStructs = list;
            //Debug.Log("GlobalData.stepStructs Count: " + GlobalData.stepStructs.Count);
            GlobalData.canClone = true;
            GameMode.Instance.Prepare(); // Step¼����ɺ���Ϸ׼��

            if (GlobalData.mode == Mode.Examination)
            {
                GameMode.Instance.m_Score = GlobalData.trainingExamscore / GlobalData.stepStructs.Count;
                Debug.Log("ʵѵ�� " + GameMode.Instance.m_Score);
            }

            InfoPanel._instance.gameObject.SetActive(true);
        });
        SpawnTask();
    }

    /// <summary>
    /// ��ק�ؼ��Ĵ���
    /// </summary>
    private static void SpawnTask()
    {
        DragTask task;
        task = new DragTask();
        if (!task.IsInit)
        {
            task.Init(GlobalData.stepStructs, GameObject.Find("Canvas").transform); //MenuPanel/Content/BG
        }
        task.Show();      
    }
}
