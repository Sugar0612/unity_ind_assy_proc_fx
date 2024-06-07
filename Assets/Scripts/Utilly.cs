using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace sugar
{
    public static class Utilly
    {
        /// <summary>
        /// ���� path_url �ҵ��ı�����
        /// </summary>
        /// <param name="path_url"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator DownLoadTextFromServer(string path_url, Action<string> callback)
        {
            UnityWebRequest req = UnityWebRequest.Get(path_url);
            yield return req.SendWebRequest();

            string content = req.downloadHandler.text;

            if (callback != null)
            {
                callback(content);
            }
        }
    }
}
