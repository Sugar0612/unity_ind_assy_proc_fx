using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���d����UI�ű�
/// </summary>
public class LoadingPanel : BasePanel, IGlobalPanel
{
    public Slider m_ProgressSlider; // �M�ȗl
    public TextMeshProUGUI m_ProgressPercent; // �@ʾ�M�ȗl�ٷֱ�

    public override void Awake()
    {
        base.Awake();

        m_ProgressSlider.onValueChanged.AddListener( (value) => 
        {
            float progress = (float)value;
            m_ProgressPercent.text = progress.ToString("0.00%");
        });
    }

    // �������d
    public void LoadScene(string scene, string model_name, bool real)
    {
        if (real)
        {
            StartCoroutine(RealLoad(scene));
        }
        else
        {
            StartCoroutine(UnRealLoad(scene, model_name));
        }
    }

    private IEnumerator RealLoad(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false; // �������@ʾ��ǰ̨���F�����_���d
        while (!async.isDone)
        {
            m_ProgressSlider.value = async.progress;
            Debug.Log(m_ProgressSlider.value);
            if (async.progress >= 0.9f) //�������d�� progress ��0.9f�����ѽ����d����
            {
                m_ProgressSlider.value = 1.0f;
                yield return null;
                async.allowSceneActivation = true;
                OnLoaded();
                yield break;
            }
        }
        async.allowSceneActivation = true; // ���_���d�ꮅ�ᣬ���@ʾ��ǰ�_ȥ
        OnLoaded();
    }

    /// <summary>
    /// �ٽ���������
    /// PanelLoad��UI Percent�򠑼��d̫���ֱ�����^...
    /// ...����׌���������@ʾ�����@��ȥ̎���M�ȗl��percent
    /// </summary>
    /// <param name="name"></param>
    /// <param name="model_name"></param>
    /// <returns></returns>
    private IEnumerator UnRealLoad(string name, string model_name)
    {
        if (model_name == "ѵ��")
        {
            // ģ�ͳ����첽����
            GameObject obj;
            AsyncOperationHandle<GameObject> model_async = Addressables.LoadAssetAsync<GameObject>(GlobalData.ModelTarget.modelName);
            while (!model_async.IsDone)
            {
                //Debug.Log("proess: " + model_async.PercentComplete.ToString("f6"));
                yield return null;
            }
            obj = Instantiate(model_async.Result);
            obj.name = GlobalData.ModelTarget.modelName;
        }

        // Unity��������
        AsyncOperation scene_async = SceneManager.LoadSceneAsync(name);
        scene_async.allowSceneActivation = false; // �������@ʾ��ǰ̨���F�����_���d
        float real_percent;
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            real_percent = scene_async.progress;
            if (real_percent >= 0.9f) //�挍�ļ��d�ٷֱ�
            {
                real_percent = 1.0f;
            }

            if (real_percent > percent) // �@ʾLoading���ڣ����^ƽ���Ļ����M�ȗl��
            {
                if (percent <= 0.9f) 
                {
                    percent += Time.deltaTime;
                }
                else
                {
                    percent += Time.deltaTime * 0.2f;
                }
                percent = Mathf.Clamp01(percent);
            }
            m_ProgressSlider.value = percent;
            yield return new WaitForEndOfFrame();
        }

        scene_async.allowSceneActivation = true; // ���_���d�ꮅ�ᣬ���@ʾ��ǰ�_ȥ
        OnLoaded();
    }

    /// <summary>
    /// ���d����ᣬ�N�����d����
    /// </summary>
    private void OnLoaded()
    {
        //DestroyImmediate(gameObject);
        Destroy(gameObject);
    }
}
