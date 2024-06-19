using LitJson;
using sugar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public struct StepStruct
{
    public string method; // ����[��ק or ���]
    public List<string> tools; // ����
    public string stepName; // ��������
    public List<string> animLimite; // �ò���Ķ���֡����Χ
}


// �ˆ����
public class MenuPanel : BasePanel
{
    public GameObject m_MenuItem;
    public Transform menuItemParent;

    private List<Menu> m_MenuList = new List<Menu>(); // �Y������Ҫ���ù��ߵ���Ϣ
    private string currTaskName; // Ŀǰ��Task����
    private BaseTask currTask; // Ŀǰ������ʵ��

    private Dictionary<string, BaseTask> m_TaskDic = new Dictionary<string, BaseTask>(); // �����ֵ�

    private void Start()
    {
        ConfigMenuList menulist = ConfigConsole.Instance.FindConfig<ConfigMenuList>();
        m_MenuList = menulist.m_MenuList;

        Init();
    }

    private void Init()
    {
        if (m_MenuItem == null)
        {
            Debug.Log("�ˆΌ��������ڣ�");
            return;
        }

        foreach (var menu in m_MenuList)
        {
            string[] menuName = menu.menuName.Split('_');
            //Debug.Log("GlobalData.currModuleName: " + GlobalData.currModuleName + "| menuName[1]: " + menuName[1]);
            if (GlobalData.currModuleName == menuName[1])
            {
                if (menuName[0] != "��װ����")
                {
                    // TODO..
                }
                else
                {
                    Debug.Log("menuName: " + menuName[0]);

                    List<StepStruct> list = new List<StepStruct>();
                    foreach (var item in menu.subMenuList)
                    {
                        //Debug.Log("��ǰcodeΪ = " + GlobalData.currModuleCode + " | �б�IDΪ = " + item.enumID.ToString());
                        if (int.Parse(GlobalData.currModuleCode) == item.enumID)
                        {
                            Debug.Log("curr Name: " + item.subMenuName);
                            NetworkManager._Instance.DownLoadTextFromServer((Application.streamingAssetsPath + "/ModelExplain/" + item.subMenuName + "Step.txt"), (dataStr) => 
                            {
                                //Debug.Log(dataStr);
                                JsonData js_data = JsonMapper.ToObject(dataStr);
                                JsonData step = js_data["child"];
                                for (int i = 0; i < step.Count; i++)
                                {
                                    StepStruct step_st = new StepStruct();
                                    string[] field = step[i].ToString().Split("_");
                                    if (field.Length == 4)
                                    {
                                        step_st.method = field[0];
                                        step_st.tools = new List<string>(field[1].Split("|"));
                                        step_st.stepName = field[2];
                                        step_st.animLimite = new List<string>(field[3].Split("~"));
                                    }
                                    else
                                    {
                                        step_st.tools = new List<string>(field[0].Split("|"));
                                        step_st.stepName = field[1];
                                        step_st.animLimite = new List<string>(field[2].Split("~"));
                                    }
                                    list.Add(step_st);
                                    //subMenu.subMenuName = step[i].ToString();
                                    //subMenu.enumID = item.enumID;
                                    //list.Add(subMenu);
                                }
                                SpawnTask(menuName[0], list); // ��ͬ��ģʽ�ַ���ͬ���¼�
                            });
                        }
                    }
                }
            }
        }
    }

    private void SpawnTask(string menuName, List<StepStruct> list)
    {
        if (currTaskName == menuName)
        {
            Debug.Log($"�ظ����� �� {menuName}");
            return;
        }

        if (currTask != null)
            currTask.Exit();

        BaseTask task = null;
        m_TaskDic.TryGetValue(menuName, out task);
        if (task == null)  // ����
        {
            switch (menuName)
            {
                case "��ѧ��Դ": //��֪ѧϰ
                    task = new RenZhiXueXi();
                    break;
                case "ʩ��׼��": //����ԭ��
                    task = new GouZaoYuanLi();
                    break;
                case "��װ����":
                    task = new ChaiZhuangFangZhen();
                    task.m_Drag = true;
                    break;
                case "����":
                    task = new ShiXunKaoHe();
                    break;
            }
            m_TaskDic.Add(menuName, task);

            // TODO..
            if (task.m_Drag)
            {
                // ��ʾ��ʾ���ڿؼ�
            }
            else
            {

            }
        }
        currTaskName = menuName;
        currTask = task;
        if (!currTask.IsInit)
        {
            currTask.Init(list, transform.Find("Content/BG"));
        }
        // ������Ϣ����
        GlobalData.stepStructs.Clear();
        GlobalData.stepStructs = list;

        currTask.Show();
        GameMode.Instance.Prepare(); // Step¼����ɺ���Ϸ׼��
    }
}
