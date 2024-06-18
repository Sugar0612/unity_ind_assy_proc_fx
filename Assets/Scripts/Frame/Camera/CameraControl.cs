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

    /// <summary>
    /// ��ͨģʽ��ʾplayer��������������������
    /// </summary>
    static public void SetNormal()
    {
        CloseAll();
        player?.SetActive(true); 
    }

    /// <summary>
    /// ���Ŷ��� ʹ�ö������.
    /// </summary>
    static public void SetAnimation()
    {
        CloseAll();
        animation?.SetActive(true);
    }

    /// <summary>
    /// ��ʾ�����
    /// </summary>
    static public void SetMain()
    {
        CloseAll();
        main?.SetActive(true);
    }

    // ��֪������д��չ�Ժܲ�ṹд�ĺ���֣��ǵģ��Ҿ����Ժ󲻻��������������µ�Camera obj��...
    //... ���Ծ���ôд�ѡ�����
}
