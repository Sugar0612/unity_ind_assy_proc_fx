using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera ��ʼ��������
public class CameraLauncher : MonoBehaviour
{
    private void Awake()
    {
        CameraControl.main = GameObject.Find("Main Camera");

        if (CameraControl.player != null)
        {
            CameraControl.player.transform.Find("Capsule").gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        // �ȹر����о�ͷ
        CameraControl.CloseAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ModelAnimControl._Instance.Play(0, 100);
        }
    }

    void Start()
    {
        // ��������Ҿ�ͷ
        CameraControl.SetNormal();
        ModelAnimControl._Instance.Play(0, 0); // �Ⲣ����Ϊ�� ����ʲô������ֻ��Ϊ�����ó���ģ��
    }
}
