using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����洢�����и��ֹ��ܵ����
public static class CameraControl
{
    static public GameObject main; // �����
    static public GameObject animation; // �������
    static public GameObject player; // �������

    static public void CloseAll()
    {
        main?.SetActive(false);
        animation?.SetActive(false);
        player?.SetActive(false);
    }

    static public void SetNormal()
    {
        main?.SetActive(false);
        animation?.SetActive(false);
        player?.SetActive(true);
    }
}
