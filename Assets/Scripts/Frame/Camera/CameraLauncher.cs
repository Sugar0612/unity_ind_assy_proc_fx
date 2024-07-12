using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera ��ʼ��������
public class CameraLauncher : MonoBehaviour
{
    private void Awake()
    {
        CameraControl.main = GameObject.Find("Main Camera");

        // �ȹر����о�ͷ
        CameraControl.CloseAll();
    }

    private void Update()
    {
        float num = RenderSettings.skybox.GetFloat("_Rotation");
        RenderSettings.skybox.SetFloat("_Rotation", num + 0.002f);
    }

    void Start()
    {
        // ����������ͷ
        CameraControl.SetMain();
    }
}
