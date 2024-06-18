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

    void Start()
    {
        // ��������Ҿ�ͷ
        CameraControl.SetNormal();
    }

}
