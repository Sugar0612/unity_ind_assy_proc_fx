using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo
{
    public string userName;
    public string password;
    public bool login;
    public string hint;
}

namespace sugar
{
    public class LoginPanel : MonoBehaviour
    {
        /// <summary>
        /// IF => InputField
        /// </summary>
        public TMP_InputField m_UserIF;
        public TMP_InputField m_PwdIF;
        public Button m_Login;
        public Button m_Close;

        private async void Awake()
        {
            await Utilly.DownLoadTextFromServer(Application.streamingAssetsPath + "/IP.txt", (content) =>
            {
                GlobalData.IP = content;
                GlobalData.SetUrl(content, "8096");
            });

            if (m_Login != null)
            {
                m_Login.onClick.AddListener(LoginRequest);
            }

            if (m_Close != null)
            {
                m_Close.onClick.AddListener(UITools.Quit);
            }
        }

        void Update()
        {

        }

        /// <summary>
        /// ��¼����
        /// </summary>
        private void LoginRequest()
        {
            if (string.IsNullOrEmpty(m_UserIF.text))
            {
                UITools.ShowMessage("�û�������Ϊ��");
                return;
            }
            if (string.IsNullOrEmpty(m_PwdIF.text))
            {
                UITools.ShowMessage("���벻��Ϊ��");
                return;
            }
            
            // �ͻ��������¼
            Client.Instance.Login(URL.URL_LOGIN, m_UserIF.text, m_PwdIF.text);
        }
    }
}
