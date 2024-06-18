using LitJson;
using sugar;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public struct ConstrPtStep
{
    public string step; //��ǰ����
    public string constrPt; // ʩ��Ҫ��
}

public enum AnimState
{
    None,
    Stop,
    Play
}

public class ModelAnimControl : MonoBehaviour
{
    public static ModelAnimControl _Instance;

    public GameObject m_animCamera; // �������������
    public GameObject m_player; // �������

    private Animator m_Animtor; // Animtor���

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

        m_Animtor = GetComponent<Animator>();
    }

    private void Start()
    {
        CameraControl.player = m_player;
        CameraControl.animation = m_animCamera;
    }

    public void Play(float f_start, float f_end)
    {
        // �������

        StartCoroutine(PlayAnim(f_start, f_end));
    }

    private IEnumerator PlayAnim(float f_start, float f_end)
    {
        m_Animtor.SetBool("play", true);
        yield return new WaitForSeconds(0.1f);

        float start = f_start * (1 / 24.0f);
        float end = f_end * (1 / 24.0f);
        float animTime = (end - start); // f_start �� f_end ����֡ʱ����

        m_Animtor.PlayInFixedTime("Play", 0, start); // �� startʱ�俪ʼ���Ŷ���
        m_Animtor.speed = 1.0f;
        yield return new WaitForSeconds(animTime);

        // ������Ϲرն���
        m_Animtor.SetBool("play", false);
        yield return null;
    }
}
