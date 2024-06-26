using LitJson;
using sugar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

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
    public GameObject m_Item; // �˵��б��е�Item

    public Transform menuItemParent;

    //private List<Menu> m_MenuList = new List<Menu>(); // �Y������Ҫ���ù��ߵ���Ϣ
    private string currTaskName; // Ŀǰ��Task����
    private BaseTask currTask; // Ŀǰ������ʵ��

    private Dictionary<string, BaseTask> m_TaskDic = new Dictionary<string, BaseTask>(); // �����ֵ�

    private List<GameObject> m_Menus = new List<GameObject>(); // ����˵���ť�б�
    private List<GameObject> m_Menulist = new List<GameObject>(); // ����˵��б�

    private GameObject currMeunList; //Ŀǰ�򿪵Ĳ˵��б�

    private void Start()
    {
        BuildMenuList();
        //Init();
    }

    private void BuildMenuList()
    {
        foreach (var proj in GlobalData.Projs)
        {
            GameObject menuItem = Instantiate(m_MenuItem, menuItemParent);
            GameObject list = menuItem.transform.Find("SubMenuGrid").gameObject;

            BuildMenuItem(proj.targets, list);
            list.gameObject.SetActive(false);
            menuItem.gameObject.SetActive(true);

            Button menuBtn = menuItem.transform.GetChild(0).GetComponent<Button>();
            menuBtn.GetComponentInChildren<TextMeshProUGUI>().text = proj.ProjName;
            menuBtn.onClick.AddListener(() => 
            {
                bool b = list.activeSelf;
                SetActiveMenuItem(list, !b);
            });

            m_Menus.Add(menuItem);
            m_Menulist.Add(list);
        }
    }

    private void BuildMenuItem(List<Target> targets, GameObject list)
    {
        foreach (var target in targets)
        {
            GameObject item = Instantiate(m_Item, list.transform);
            item.gameObject.SetActive(true);
            Button itemBtn = item.transform.GetChild(0).GetComponent<Button>();
            itemBtn.GetComponentInChildren<TextMeshProUGUI>().text = target.menuName;
            itemBtn.onClick.AddListener(() => { ChooseThisItem(target, list); });
        }
    }

    /// <summary>
    /// ѵ��ģʽ���첽����ģ�ͳ����л�
    /// ����ģʽ����ʾ�˵�
    /// </summary>
    /// <param name="target"> ����Ŀ��info </param>
    /// <param name="obj"> �˵����ڵ�ʵ�� </param>
    private async void ChooseThisItem(Target target, GameObject obj)
    {
        GlobalData.ModelTarget = target;
        GlobalData.currModuleCode = target.modelCode.ToString();
        //GlobalData.currModuleName = target.modelName;

        if (GlobalData.currModuleName == "ѵ��")
        {
            await LoadSceneModel();
        }
        else
        {
            MenuGridPanel.Instance.gameObject.SetActive(true);
        }
        SetActiveMenuItem(obj, false);
    }

    private void SetActiveMenuItem(GameObject menu, bool b)
    {
        if (currMeunList != null)
        {
            currMeunList.SetActive(false);
        }
        currMeunList = menu;
        currMeunList.SetActive(b);
    }

    private void Init(string name)
    {
        if (m_MenuItem == null)
        {
            Debug.Log("�ˆΌ��������ڣ�");
            return;
        }

        if (name == "ѵ��")
        {
            NetworkManager._Instance.DownLoadTextFromServer((Application.streamingAssetsPath + "/ModelExplain/" + GlobalData.ModelTarget.modelName + "Step.txt"), (dataStr) =>
            {
                //Debug.Log(dataStr);
                List<StepStruct> list = new List<StepStruct>();
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
                SpawnTask(GlobalData.currModuleName, list); // ��ͬ��ģʽ�ַ���ͬ���¼�
                SetActiveMenuList(false);
            });
        }
        else
        {

        }
    }

    // TODO.. �޸��������
    private void SpawnTask(string menuName, List<StepStruct> list)
    {
        if (currTaskName == menuName)
        {
            Debug.Log($"�ظ����� �� {menuName}");
            return;
        }

        BaseTask task;
        m_TaskDic.TryGetValue(menuName, out task);
        if (task == null)  // ����
        {
            switch (menuName)
            {
                case "��ѧ": //��֪ѧϰ
                    task = new RenZhiXueXi();
                    break;
                case "ѵ��":
                    task = new ChaiZhuangFangZhen();
                    task.m_Drag = true;

                    // ������Ϣ����
                    GlobalData.stepStructs.Clear();
                    GlobalData.stepStructs = list;
                    GlobalData.canClone = true;
                    GameMode.Instance.Prepare(); // Step¼����ɺ���Ϸ׼��
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
                InfoPanel._instance.gameObject.SetActive(true);
            }
            else
            {
                InfoPanel._instance.gameObject.SetActive(false);
            }
        }

        currTaskName = menuName;
        currTask = task;
        if (!currTask.IsInit)
        {
            currTask.Init(list, transform.Find("Content/BG"));
        }
        currTask.Show();
    }

 
    public async UniTask LoadSceneModel()
    {
        GlobalData.DestroyModel = false;
        await LoadModel();
    }

    private async UniTask LoadModel()
    {
        // ģ�ͳ����첽����
        GameObject obj;        
        AsyncOperationHandle<GameObject> model_async = Addressables.LoadAssetAsync<GameObject>(GlobalData.ModelTarget.modelName);
        await UniTask.WaitUntil(() => model_async.IsDone == true);

        obj = Instantiate(model_async.Result);
        obj.name = GlobalData.ModelTarget.modelName;
        Init(GlobalData.currModuleName);
    }

    private void SetActiveMenuList(bool b)
    {
        foreach (var menu in m_Menus)
        {
            menu.gameObject.SetActive(b);
        }
    }
}
