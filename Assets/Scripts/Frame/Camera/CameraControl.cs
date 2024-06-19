using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����洢�����и��ֹ��ܵ����
public static class CameraControl
{
    static public GameObject main; // �����
    static public GameObject animation; // �������
    static public GameObject player; // �������

    static public GameObject target; // Ŀǰ��ʹ�õ����

    static public void CloseAll()
    {
        main?.SetActive(false);
        animation?.SetActive(false);
        player?.SetActive(false);
        target = null;
    }

    /// <summary>
    /// ��ͨģʽ��ʾplayer��������������������
    /// </summary>
    static public void SetNormal()
    {
        CloseAll();
        player?.SetActive(true); 
        target = player;
    }

    /// <summary>
    /// ���Ŷ��� ʹ�ö������.
    /// </summary>
    static public void SetAnimation()
    {
        CloseAll();
        animation?.SetActive(true);
        target = animation;
    }

    /// <summary>
    /// ��ʾ�����
    /// </summary>
    static public void SetMain()
    {
        CloseAll();
        main?.SetActive(true);
        target = main;
    }

    // ��֪������д��չ�Ժܲ�ṹд�ĺ���֣��ǵģ��Ҿ����Ժ󲻻��������������µ�Camera obj��...
    //... ���Ծ���ôд�ѡ�����
}
