using LitJson;
using sugar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConstrPtStep
{
    public string step; //��ǰ����
    public string constrPt; // ʩ��Ҫ��
}

public class ModelAnimControl : MonoBehaviour
{
    public static ModelAnimControl _Instance;
    public string m_animCameraName; // �������������

    private float totalScore = 0;

    public List<ConstrPtStep> m_ConPtStep = new List<ConstrPtStep>();

    private string ModelName; //ģ�͵� adressables Group Default Name

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this);

         ModelName = GlobalData.ModelTarget.modelName;
        // ��ȡ xxx.json �е� ��ǰ����_ʩ��Ҫ��
        NetworkManager._Instance.DownLoadTextFromServer(Application.streamingAssetsPath + "/ModelExplain/" + ModelName + ".json", (str) => 
        {
            Debug.Log(str);
            JsonData js_data = JsonMapper.ToObject<JsonData>(str);
            foreach (var item in js_data.Keys)
            {
                //Debug.Log(item);
                string[] fields = js_data[item].ToString().Split('_');

                ConstrPtStep step = new ConstrPtStep();
                step.step = fields[0];
                step.constrPt = fields[1];

                m_ConPtStep.Add(step);
            }
        });
    }

    private void Start()
    {

    }
}
