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
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    ModelAnimControl._Instance.Play(0, 100);
        //}
    }

    void Start()
    {
        // ����������ͷ
        CameraControl.SetMain();
    }
}
