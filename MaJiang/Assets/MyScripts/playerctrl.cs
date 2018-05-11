using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerctrl : MonoBehaviour
{
    public int wanjialuexing = 1;//0 bengren 1 diannao 2 liangwangwanjia
    public int playerid;
    
    public int chulijieguo = -1;//0 buyao 1 chi 2 peng 3 gang 4 hu 
    public bool istingpai = false;

    public int yaochudemajiangshu = 0;
    public List<int> yaochudemajiangshuzhu = new List<int>();

    public bool wofangchumajiang = false;

    Transform majiangcamera;

    public bool yaobuyaochuli = false;

    public  bool wofangfirst = false;
    public bool bumajiang = false;
    IEnumerator ai1ChuMaJiang()
    {
        List<int> shulist = new List<int>();
        int shumu = UIMgr.Instance.majiangzhu[playerid].childCount;
        for (int i = 0; i < shumu; i++)
        {
            shulist.Add(System.Convert.ToInt32(UIMgr.Instance.majiangzhu[playerid].GetChild(i).name));
        }
        shulist.Sort((x, y) => x.CompareTo(y));
        int[] shuzhu = new int[shulist.Count];
        for (int i = 0; i < shulist.Count; i++)
        {
            shuzhu[i] = shulist[i];
          
        }
        UIMgr.Instance.playertools[playerid].woFangChuMaJiangXuanZhe(shuzhu, ref chulijieguo, ref istingpai, ref yaochudemajiangshu, ref yaochudemajiangshuzhu);
        if (chulijieguo == 4)//胡了
        {
            StartCoroutine(UIMgr.Instance.chuMaJiang(playerid, null));
        }
        else if (chulijieguo == 3)//暗杠
        {
            StartCoroutine(UIMgr.Instance.chuMaJiang(playerid, null));
        }
        else if (chulijieguo == 0)//只出一张
        {
            Transform mj = null;
            for (int i = 0; i < UIMgr.Instance.majiangzhu[playerid].childCount; i++)
            {
                if(System.Convert.ToInt32(UIMgr.Instance.majiangzhu[playerid].GetChild(i).name) == yaochudemajiangshu)
                {
                    mj = UIMgr.Instance.majiangzhu[playerid].GetChild(i);
                    break;
                }
            }
            if (mj)
            {
                StartCoroutine(UIMgr.Instance.chuMaJiang(playerid, mj));
            }
        }
        yield return null;
    }
    IEnumerator ai1YaoMaJiang()
    {
        yield return new WaitForSeconds(0.1f);
        List<int> shulist = new List<int>();
        int shumu = UIMgr.Instance.majiangzhu[playerid].childCount;
        for (int i = 0; i < shumu; i++)
        {
            shulist.Add(System.Convert.ToInt32(UIMgr.Instance.majiangzhu[playerid].GetChild(i).name));
        }
        int currentchudeshu = System.Convert.ToInt32(youxixingxi.Instance.currentchudemajiang.name);
        shulist.Add(currentchudeshu);
        shulist.Sort((x, y) => x.CompareTo(y));
        int[] shuzhu = new int[shulist.Count];
        for (int i = 0; i < shulist.Count; i++)
        {
            shuzhu[i] = shulist[i];
        }
        UIMgr.Instance.playertools[playerid].yaoBuYao(currentchudeshu, shuzhu, ref chulijieguo, ref istingpai, ref yaochudemajiangshuzhu);


    }
    private void Start()
    {
        majiangcamera = GameObject.Find("majiangcamera").transform;
        if (playerid == youxixingxi.Instance.myid)
        {
            wanjialuexing = 0;
        }
    }

    private void Update()
    {
        if (wanjialuexing == 1 )
        {
            if (yaobuyaochuli && youxixingxi.Instance.currentPersonId != playerid && !youxixingxi.Instance.isgameover)//要不要？
            {
                StartCoroutine(ai1YaoMaJiang());
               
                yaobuyaochuli = false;
                return;

            }
            if (playerid == youxixingxi.Instance.currentPersonId && !UIMgr.Instance.isanimation  && wofangchumajiang && !youxixingxi.Instance.isgameover)
            {
                wofangchumajiang = false;
                StartCoroutine(ai1ChuMaJiang());
                return;
            }
        }
        else if (wanjialuexing == 2)
        {

        }
        else if (wanjialuexing == 0)//本机玩家
        {
            if ( playerid == youxixingxi.Instance.myid && youxixingxi.Instance.currentPersonId != playerid && !youxixingxi.Instance.isgameover)//要不要？
            {
                if (yaobuyaochuli)
                {
                    
                    UIMgr.Instance.checkShowUIButtons();
                    yaobuyaochuli = false;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = majiangcamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.transform.name);
                        if (hit.transform.tag == "MaJiang")
                        {
                            majiangzhuinfo info = hit.transform.GetComponent<majiangzhuinfo>();
                            if (info)
                            {
                                if (info.majianglist.Count == UIMgr.Instance.majiangzhu[playerid].childCount + 1 || info.majianglist.Count == UIMgr.Instance.majiangzhu[playerid].childCount)
                                {
                                    Debug.Log("我方胡了");
                                    
                                    chulijieguo = 4;
                                    yaochudemajiangshuzhu.Clear();
                                    for (int i = 0; i < info.majianglist.Count; i++)
                                    {
                                        yaochudemajiangshuzhu.Add(info.majianglist[i]);
                                    }
                                }
                                else if (info.majianglist.Count == 4)
                                {
                                    chulijieguo = 3;
                                    yaochudemajiangshuzhu.Clear();
                                    for (int i = 0; i < 4; i++)
                                    {
                                        yaochudemajiangshuzhu.Add(info.majianglist[i]);
                                    }
                                }
                                else if (info.majianglist.Count == 3)
                                {
                                    if (info.majianglist[0] == info.majianglist[1])
                                    {
                                        chulijieguo = 2;

                                    }
                                    else
                                    {
                                        chulijieguo = 1;
                                    }
                                    yaochudemajiangshuzhu.Clear();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        yaochudemajiangshuzhu.Add(info.majianglist[i]);
                                    }
                                }
                                var linshi = GameObject.Find("linshi").transform;
                                List<Transform> list = new List<Transform>();
                                for (int i = 0; i < linshi.childCount; i++)
                                {
                                    list.Add(linshi.GetChild(i));
                                }
                                foreach (var obj in list)
                                {
                                    Destroy(obj.gameObject);

                                }
                                UIMgr.Instance.buyaobutton.SetActive(false);

                            }

                        }

                    }
                }
                }
            if(playerid == youxixingxi.Instance.currentPersonId && youxixingxi.Instance.currentPersonId == youxixingxi.Instance.myid && !UIMgr.Instance.isanimation && wofangchumajiang && !youxixingxi.Instance.isgameover)
            {
                if (wofangfirst)
                {
                    
                    UIMgr.Instance.woFangHuiHe();
                    wofangfirst = false;
                }
   
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = majiangcamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            
                            if (hit.transform.tag == "MaJiang")
                            {
                                youxixingxi.Instance.currentchudemajiang = hit.transform;
                                majiangzhuinfo info = hit.transform.GetComponent<majiangzhuinfo>();
                                if (info)
                                {
                                if (info.majianglist.Count == UIMgr.Instance.majiangzhu[playerid].childCount + 1 || info.majianglist.Count == UIMgr.Instance.majiangzhu[playerid].childCount)
                                {
                                    Debug.Log("我方胡了");
                                    chulijieguo = 4;
                                    yaochudemajiangshuzhu.Clear();
                                    for (int i = 0; i < info.majianglist.Count; i++)
                                    {
                                        yaochudemajiangshuzhu.Add(info.majianglist[i]);
                                    }
                                    chulijieguo = 4;
                                }
                                else if (info.majianglist.Count == 4)//an gang
                                    {
                                            yaochudemajiangshuzhu.Clear();
                                            for (int i = 0; i < 4; i++)
                                            {
                                                 int temp = info.majianglist[0];
                                                  yaochudemajiangshuzhu.Add(temp);
                                             }
                                    chulijieguo = 3;
                                }

                                var linshi = GameObject.Find("linshi").transform;
                                    List<Transform> list = new List<Transform>();
                                    for (int i = 0; i < linshi.childCount; i++)
                                    {
                                        list.Add(linshi.GetChild(i));
                                    }
                                    foreach (var obj in list)
                                    {
                                        Destroy(obj.gameObject);

                                    }
                                    wofangchumajiang = false;
                                    
                                   
                                }
                                else
                                {
                                 
                                    wofangchumajiang = false;
                                    chulijieguo = 0;
                                   
                                }

                            }

                        
                    }
                }
              

            }
        
            




        }

        
   
    }


    void canBuYao(bool b)
    {
        UIMgr.Instance.buyaobutton.SetActive(b);
    }


}
