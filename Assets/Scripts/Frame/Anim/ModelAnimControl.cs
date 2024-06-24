using LitJson;
using sugar;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.XR.Oculus.Input;
using UnityEngine;

public enum AnimState
{
    None,
    Playing,
    Puase,
    Stop
}

public struct ConstrPtStep
{
    public string step; //��ǰ����
    public string constrPt; // ʩ��Ҫ��
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

    private AnimState m_AnimState = AnimState.None; // ��¼�����Ĳ���״̬

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(_Instance);

        ModelName = GlobalData.ModelTarget.modelName;
        // ��ȡ xxx.json �е� ��ǰ����_ʩ��Ҫ��
        NetworkManager._Instance.DownLoadTextFromServer(Application.streamingAssetsPath + "/ModelExplain/" + ModelName + ".json", (str) =>
        {
            //Debug.Log(str);
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
        CameraControl.SetPlayer();
        StartCoroutine(Slice(0f, 0f));
    }

    void Update()
    {
        if (GlobalData.DestroyModel)
        {
            DataRecycling();
            GlobalData.DestroyModel = false;
        }
    }


    public IEnumerator PlayAnim(float f_start, float f_end)
    {
        // �л����������
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        Debug.Log(canvas.name);
        canvas.SetActive(false); // ���Ŷ�����ʱ�� �ر�UI��
        CameraControl.SetAnimation();
        GameMode.Instance.ArrowActive(false); // ���ؼ�ͷ

        StartCoroutine(Slice(f_start, f_end));
        yield return new WaitUntil( () => 
        {
            return m_AnimState != AnimState.Playing;
        });

        CameraControl.SetPlayer(); // �л��� Player�����
        canvas.SetActive(true);
        GameMode.Instance.NextStep();
    }

    public void Play()
    {
        m_Animtor.SetBool("play", true);
        m_AnimState = AnimState.Playing;
    }

    public void Stop()
    {
        m_Animtor.SetBool("play", false);
        m_AnimState = AnimState.Stop;
    }

    public void GoOn()
    {
        m_Animtor.speed = 1.0f;
        m_AnimState = AnimState.Playing;
    }

    public void Puase()
    {
        m_Animtor.speed = 0.0f;
        m_AnimState = AnimState.Puase;
    }

    // ���Ŷ���ĳһ��֡�Ķ���
    public IEnumerator Slice(float f_start, float f_end)
    {
        //Debug.Log("In Slice!");
        float start = f_start * (1 / 24.0f);
        float end = f_end * (1 / 24.0f);
        float animTime = (end - start); // f_start �� f_end ����֡ʱ����

        Play();
        yield return new WaitForSeconds(0.1f);

        m_Animtor.PlayInFixedTime("Play", 0, start); // �� startʱ�俪ʼ���Ŷ���
        GoOn();
        yield return new WaitForSeconds(animTime);

        //Debug.Log("Close ANim");
        // ���������ͣ����
        Puase();
        yield return null;
    }

    /// <summary>
    /// ���ݻ���
    /// </summary>
    /// <param name="msg"></param>
    public void DataRecycling()
    {
        Debug.Log("DataRecycling");
        CameraControl.SetMain();
        CameraControl.animation = null;
        CameraControl.player = null;
        Destroy(gameObject);
    }
}
