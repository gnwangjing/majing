using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    Text[]playertexts = new Text[4];
    public bool showxijie = false;
    public bool showchixijie = false;
    public bool showpengxijie = false;
    public bool showgangxijie = false;
    public bool showhuxijie = false;
    bool nexthasup = true;
    bool endhasup = true;
    public static UIMgr Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject zhidongbutton;
    public GameObject chibutton;
    public GameObject pengbutton;
    public GameObject gangbutton;
    public GameObject hubutton;
    public GameObject buyaobutton;
    public GameObject quxiaobutton;
    public GameObject shijiao1button;
    public GameObject shijiao2button;
    public GameObject shijiao3button;
    public GameObject shijiao4button;
    float famajiangshijian = 0.1f;
    float yidongmajiangshijian = 0.2f;
    public bool isanimation = false;
    int myid = 0;
    public GameObject dice1;
    public GameObject dice2;

    public Vector3 A, B;
    Quaternion q1 = Quaternion.identity;
    Quaternion q2 = Quaternion.identity;
    diceshu shu1;
    diceshu shu2;
    GameObject dicecamera;
    int he;
    int xiao;
    GameObject cubekuang;
    Transform majiangall;
    Transform wodemajiang;
    Transform dongpos;
    Transform beipos;
    Transform xipos;
    Transform nanpos;
    Transform[] players = new Transform[4];
    public Transform[] majiangzhu = new Transform[4];
    Transform[] pengdemajiangzhu = new Transform[4];
    Transform[] dachudemajiangzhu = new Transform[4];
    public playerctrl[] playerctrls = new playerctrl[4];
    public tool[] playertools = new tool[4];
    public int ibegin = 0; //当前顺序摸的麻将对
    public int iend = 0;//麻将尾部对
    int majiangshu = 136;
    Transform majiangcamera;

    public List<Transform> feimajiang;
    public bool hasfamajiang = false;
    public Transform linshi;

    Text hua;
    Text zhidong;
    void saySomething(string str)
    {
        hua.enabled = true;
        hua.text = str;
    }
    void moveCameraToPerson(int personid)
    {
        if (youxixingxi.Instance.currentlookuserid < 0 || youxixingxi.Instance.currentlookuserid > 3)
        {
            youxixingxi.Instance.currentlookuserid = 0;//游戏初始化时就为0
        }
        if (personid < 0 || personid > 3)
        {
            return;
        }

        if (personid - youxixingxi.Instance.currentlookuserid == 0)
            return;
        int i = 0;
        int id = youxixingxi.Instance.currentlookuserid;
        while (id != personid)
        {
            id = getNextPersonId(id);
            i++;
        }
        //i>2向左转 否则向右转 
        //左转时角度为 (4-i)*90;右转时角度为 i*90;
        Transform cameractrl = GameObject.Find("cameracontrol").transform;
        float rotatetime = 1.0f;
        int oldlookuserid = youxixingxi.Instance.currentlookuserid;
        youxixingxi.Instance.currentlookuserid = personid;
        if (hasfamajiang)
        {
            paiXuZhuAndShow(oldlookuserid, 0);
        }
        if (i > 2)
        {
            cameractrl.DORotate(cameractrl.eulerAngles + (4 - i) * new Vector3(0, 90, 0), (4 - i) * rotatetime).OnComplete(() => onMoveCameraComplete());
        }
        else
        {
            cameractrl.DORotate(cameractrl.eulerAngles + i * new Vector3(0, -90, 0), i * rotatetime).OnComplete(() => onMoveCameraComplete());
        }
        enableShiJiaoButtons(false);
    }
    void onMoveCameraComplete()
    {
        if (hasfamajiang)
        {
            paiXuZhuAndShow(youxixingxi.Instance.currentlookuserid, 0);
        }
        if (youxixingxi.Instance.myid > 3 || youxixingxi.Instance.myid < 0)
        {
            enableShiJiaoButtons(true);
        }
        else
        {
            enableShiJiaoButtons(false);
        }

    }
    public void unshowUIButtons()
    {
        chibutton.SetActive(false);
        pengbutton.SetActive(false);
        gangbutton.SetActive(false);
        hubutton.SetActive(false);
        buyaobutton.SetActive(false);
        quxiaobutton.SetActive(false);
    }
    void Start()
    {

        youxixingxi.Instance.bianshemajiang = null;
        hua = GameObject.Find("Canvas/hua").GetComponent<Text>();
        zhidong = GameObject.Find("Canvas").transform.Find("zhidong").Find("Text").GetComponent<Text>();
        playertexts[0] = GameObject.Find("Canvas/bifengban/wanjia1").GetComponent<Text>();
        playertexts[1] = GameObject.Find("Canvas/bifengban/wanjia2").GetComponent<Text>();
        playertexts[2] = GameObject.Find("Canvas/bifengban/wanjia3").GetComponent<Text>();
        playertexts[3] = GameObject.Find("Canvas/bifengban/wanjia4").GetComponent<Text>();
        linshi = GameObject.Find("linshi").transform;

        feimajiang = new List<Transform>();
        majiangall = GameObject.Find("MaJiangAll").transform;
        shijiao1button = GameObject.Find("Canvas").transform.Find("shijiao1").gameObject;
        shijiao2button = GameObject.Find("Canvas").transform.Find("shijiao2").gameObject;
        shijiao3button = GameObject.Find("Canvas").transform.Find("shijiao3").gameObject;
        shijiao4button = GameObject.Find("Canvas").transform.Find("shijiao4").gameObject;

        for (int i = 0; i < 4; i++)
        {
            showNewText(i, youxixingxi.Instance.wanjianame[i] + "胡了" + youxixingxi.Instance.wanjiahuchishu[i] + "次");
        }
        if (youxixingxi.Instance.isxunhuan)
        {
            startGame();
        }
    }
    public void showNewText(int personid,string str)
    {
        playertexts[personid].text = str;
    }
    public void startGame()
    {
        for (int i = 0; i < 4; i++)
        {
            showNewText(i, youxixingxi.Instance.wanjianame[i] + "胡了" + youxixingxi.Instance.wanjiahuchishu[i] + "次");
        }
        youxixingxi.Instance.currentPersonId = youxixingxi.Instance.zhuangjiaid;
        youxixingxi.Instance.isgameover = false;
        if (youxixingxi.Instance.myid > 3 || youxixingxi.Instance.myid < 0)
        {
            enableShiJiaoButtons(true);
        }
        else
        {
            enableShiJiaoButtons(false);
        }


        zhidongbutton = GameObject.Find("Canvas").transform.Find("zhidong").gameObject;
        chibutton = GameObject.Find("Canvas").transform.Find("chi").gameObject;
        pengbutton = GameObject.Find("Canvas").transform.Find("peng").gameObject;
        gangbutton = GameObject.Find("Canvas").transform.Find("gang").gameObject;
        hubutton = GameObject.Find("Canvas").transform.Find("hu").gameObject;
        buyaobutton = GameObject.Find("Canvas").transform.Find("buyao").gameObject;
        quxiaobutton = GameObject.Find("Canvas").transform.Find("quxiao").gameObject;
        unshowUIButtons();
        moveCameraToPerson(youxixingxi.Instance.myid);
        for (int i = 0; i < 4; i++)
        {
            GameObject player = GameObject.Find("Players").transform.GetChild(i).gameObject;
            GameObject yongdemajiangzhu = new GameObject();
            yongdemajiangzhu.name = "majiangzhu";
            GameObject pengdemajiangzhu2 = new GameObject();
            pengdemajiangzhu2.name = "pengdemajiangzhu";
            GameObject dachudemajiangzhu2 = new GameObject();
            dachudemajiangzhu2.name = "dachudemajiangzhu";
            yongdemajiangzhu.transform.SetParent(player.transform);
            pengdemajiangzhu2.transform.SetParent(player.transform);
            dachudemajiangzhu2.transform.SetParent(player.transform);
            majiangzhu[i] = yongdemajiangzhu.transform;
            players[i] = player.transform;
            player.AddComponent<playerctrl>();
            playerctrls[i] = player.transform.GetComponent<playerctrl>();
            playerctrls[i].playerid = i;

            player.gameObject.AddComponent<tool>();
            playertools[i] = player.transform.GetComponent<tool>();
            dachudemajiangzhu[i] = dachudemajiangzhu2.transform;
            pengdemajiangzhu[i] = pengdemajiangzhu2.transform;

        }
        zhidongbutton.SetActive(true);
        majiangcamera = GameObject.Find("majiangcamera").transform;
        wodemajiang = GameObject.Find("majiangcamera/wodemajiang").transform;
        dongpos = GameObject.Find("dongpos").transform;
        beipos = GameObject.Find("beipos").transform;
        xipos = GameObject.Find("xipos").transform;
        nanpos = GameObject.Find("nanpos").transform;
        
        prefabInitMaJiang();
        daLuanShunXu();
        GameObject.Find("Canvas/kaishiyouxi").SetActive(false);
        cubekuang = GameObject.Find("cameracontrol/cubekuang");
        dicecamera = GameObject.Find("dicecamera").transform.GetChild(0).gameObject;
        dice1.SetActive(true);
        dice2.SetActive(true);
        shu1 = dice1.GetComponent<diceshu>();
        shu2 = dice2.GetComponent<diceshu>();
        shu1.shu = 0;
        shu2.shu = 0;
        A = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        B = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        q1.SetFromToRotation(A, B);
        dice1.transform.rotation = q1;
        A = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        B = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        q2.SetFromToRotation(A, B);
        dice2.transform.rotation = q2;
        dice1.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 1, 0), new Vector3(1, 1, 1));
        dice2.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 1, 0), new Vector3(1, 1, 1));
        StartCoroutine(waitForDicesStop());
    }
    IEnumerator waitForDicesStop()
    {
    
        while (shu1.shu == 0 || shu2.shu == 0)
        {
            yield return null;
        }

        Debug.Log("shu1:" + shu1.shu + "  shu2:" + shu2.shu);
        he = shu1.shu + shu2.shu;
        xiao = shu1.shu < shu2.shu ? shu1.shu : shu2.shu;
        dicecamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dicecamera.SetActive(false);
        StartCoroutine(startFamajiangAnimation());

    }
    void prefabInitMaJiang()
    {
        int j = 0;
        for (int i = 0; i < majiangshu; i += 4)
        {
            for (int k = 0; k < 4; k++)
            {
                switch (j)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        majiangall.GetChild(i + k).GetChild(0).name = (100 + j).ToString();
                      
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                        majiangall.GetChild(i + k).GetChild(0).name = (200 + j).ToString();
                      
                        break;
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                        majiangall.GetChild(i + k).GetChild(0).name = (300 + j).ToString();
                       
                        break;
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                        majiangall.GetChild(i + k).GetChild(0).name = (400 + j * 10).ToString();
                  
                        break;
                }

            }
            j++;
        }
    }
    void daLuanShunXu()
    {
        for (int i = 0; i < majiangshu; i++)
        {
            Transform a = majiangall.GetChild(i).GetChild(0);
            int rd = Random.Range(0, 135);
            Transform b = majiangall.GetChild(rd).GetChild(0);
            a.parent = majiangall.GetChild(rd);
            b.parent = majiangall.GetChild(i);
            a.localPosition = Vector3.zero;
            b.localPosition = Vector3.zero;
            Vector3 temp = a.eulerAngles;
            a.eulerAngles = b.eulerAngles;
            b.eulerAngles = temp;
        }
    }
    IEnumerator startFamajiangAnimation()
    {
        int kaishidui = (he + youxixingxi.Instance.zhuangjiaid - 1) % 4;
        ibegin = 17 * (kaishidui + 1) - xiao - 1;
        iend = ibegin + 1;
        int playerid;
        for (int i = 0; i < 3; i++)
        {
            playerid = youxixingxi.Instance.zhuangjiaid;
            for (int k = 0; k < 4; k++)
            {

                for (int j = 0; j < 4; j++)
                {
                    StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));

                    while (isanimation)
                    {
                        yield return null;
                    }
                }
                playerid = getNextPersonId(playerid);
            }
        }
        playerid = youxixingxi.Instance.zhuangjiaid;
        ibegin -= 1;
        StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));
        while (isanimation)
        {
            yield return null;
        }

        StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));
        while (isanimation)
        {
            yield return null;
        }
        ibegin += 2;
        playerid = getNextPersonId(playerid);

        StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));
        while (isanimation)
        {
            yield return null;
        }
        playerid = getNextPersonId(playerid);

        StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));
        while (isanimation)
        {
            yield return null;
        }
        ibegin -= 1;
        playerid = getNextPersonId(playerid);

        StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getNextMaJiangTransform(), playerid, 0));
        while (isanimation)
        {
            yield return null;
        }
        
        hasfamajiang = true;
        playerctrls[youxixingxi.Instance.zhuangjiaid].bumajiang = true;
        StartCoroutine(changeMaJiangQuanToPerson(youxixingxi.Instance.zhuangjiaid));

    }


    void onMoveComplete(int personid, int majiangzhu)
    {
        isanimation = false;
        chongPaiMaJiang(personid, 0);
    }
    public void freshFeiMaJiang()   //刷新共用的废麻将堆
    {
        float _x_N = 0.75f;//x位置差值
        float _z_N = 1f;
        Vector3 left;

        left = nanpos.position - new Vector3(_x_N, 0, 0) * 5 + Vector3.forward * 14;
        left = new Vector3(left.x, 0.25f, left.z);

        int num = 0;
        foreach (Transform tr in feimajiang)
        {
            tr.eulerAngles = new Vector3(0, 0, 0);
            tr.position = left + new Vector3(_x_N, 0, 0) * (num % 11) - new Vector3(0, 0, _z_N) * (num / 11);//每行11个
            num++;
        }
    }
    public void test()//自动手动功能切换
    {
        
        if(playerctrls[youxixingxi.Instance.myid].wanjialuexing == 0)
        {
            playerctrls[youxixingxi.Instance.myid].wanjialuexing = 1;
            Debug.Log("test1");
            zhidong.text = "状态：自动";
        }
        else if (playerctrls[youxixingxi.Instance.myid].wanjialuexing == 1)
        {
            playerctrls[youxixingxi.Instance.myid].wanjialuexing = 0;
            Debug.Log("test2");
            zhidong.text = "状态：手动";
        }
      
    }
    public void paiXuZhuAndShow(int personid, int zhuid)
    {
        float _x_N = 0.75f;//x位置差值
        float _z_N = 1f;
        switch (personid)
        {
            case 0:
                {
                    if (zhuid == 0)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player0/majiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));
                        Vector3 left;
                        if (personid == youxixingxi.Instance.currentlookuserid)
                        {
                            left = wodemajiang.position - new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 0);
                                tr.position = left + new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }
                        else
                        {
                            left = nanpos.position - new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 0);
                                tr.position = left + new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }

                    }
                    else if (zhuid == 1)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player0/pengdemajiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        Vector3 left;
                        left = nanpos.position +Vector3.left*4 + Vector3.forward;
                        left = new Vector3(left.x, 0.25f, left.z);
                        int num = 0;
                        foreach (Transform tr in list)
                        {
                            tr.eulerAngles = new Vector3(0, 0, 0);
                            tr.position = left + new Vector3(_x_N, 0, 0) * num;
                            num++;
                        }
                    }
                    else if (zhuid == 2)
                    {

                        freshFeiMaJiang();

                    }
                }
                break;
            case 1:
                {
                    if (zhuid == 0)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player1/majiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));
                        Vector3 left;
                        if (personid == youxixingxi.Instance.currentlookuserid)
                        {
                            left = wodemajiang.position - new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 0);
                                tr.position = left + new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }
                        else
                        {
                            left = dongpos.position - new Vector3(0, 0, _x_N) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, -90);
                                tr.position = left + new Vector3(0, 0, _x_N) * num;
                                num++;
                            }
                        }

                    }
                    else if (zhuid == 1)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player1/pengdemajiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        Vector3 left;
                        left = dongpos.position +Vector3.back* 4 + Vector3.left;
                        left = new Vector3(left.x, 0.25f, left.z);
                        int num = 0;
                        foreach (Transform tr in list)
                        {
                            tr.eulerAngles = new Vector3(0, -90, 0);
                            tr.position = left + new Vector3(0, 0, _x_N) * num;
                            num++;
                        }
                    }
                    else if (zhuid == 2)
                    {
                        freshFeiMaJiang();

                    }
                }
                break;
            case 2:
                {
                    if (zhuid == 0)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player2/majiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));
                        Vector3 left;
                        if (personid == youxixingxi.Instance.currentlookuserid)
                        {
                            left = wodemajiang.position - new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 0);
                                tr.position = left + new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }
                        else
                        {
                            left = beipos.position + new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, -180);
                                tr.position = left - new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }
                    }
                    else if (zhuid == 1)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player2/pengdemajiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        Vector3 left;
                        left = beipos.position + Vector3.right * 4 + Vector3.back;
                        left = new Vector3(left.x, 0.25f, left.z);
                        int num = 0;
                        foreach (Transform tr in list)
                        {
                            tr.eulerAngles = new Vector3(0, -180, 0);
                            tr.position = left - new Vector3(_x_N, 0, 0) * num;
                            num++;
                        }
                    }
                    else if (zhuid == 2)
                    {
                        freshFeiMaJiang();
                    }
                }
                break;
            case 3:
                {
                    if (zhuid == 0)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player3/majiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));

                        Vector3 left;
                        if (personid == youxixingxi.Instance.currentlookuserid)
                        {
                            left = wodemajiang.position - new Vector3(_x_N, 0, 0) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 0);
                                tr.position = left + new Vector3(_x_N, 0, 0) * num;
                                num++;
                            }
                        }
                        else
                        {
                            left = xipos.position + new Vector3(0, 0, _x_N) * (list.Count / 2);
                            int num = 0;
                            foreach (Transform tr in list)
                            {
                                tr.eulerAngles = new Vector3(-90, 0, 90);
                                tr.position = left - new Vector3(0, 0, _x_N) * num;
                                num++;
                            }
                        }
                    }
                    else if (zhuid == 1)
                    {
                        List<Transform> list = new List<Transform>();
                        Transform zhu = GameObject.Find("Players/Player3/pengdemajiangzhu").transform;
                        int shu = zhu.childCount;
                        for (int i = 0; i < shu; i++)
                        {
                            Transform ctr = zhu.GetChild(i);
                            list.Add(ctr);
                        }
                        Vector3 left;
                        left = xipos.position + Vector3.forward*4 + Vector3.right;
                        left = new Vector3(left.x, 0.25f, left.z);
                        int num = 0;
                        foreach (Transform tr in list)
                        {
                            tr.eulerAngles = new Vector3(0, 90, 0);
                            tr.position = left - new Vector3(0, 0, _x_N) * num;
                            num++;
                        }
                    }
                    else if (zhuid == 2)
                    {
                        freshFeiMaJiang();
                    }

                }
                break;

        }
    }

    void chongPaiMaJiang(int renid, int majiangzhu)
    {


        switch (renid)
        {
            case 0:
                if (majiangzhu == 0)
                {
                    paiXuZhuAndShow(0, 0);

                }
                break;
            case 1:
                if (majiangzhu == 0)
                {
                    paiXuZhuAndShow(1, 0);

                }
                break;
            case 2:
                if (majiangzhu == 0)
                {
                    paiXuZhuAndShow(2, 0);

                }
                break;
            case 3:
                if (majiangzhu == 0)
                {
                    paiXuZhuAndShow(3, 0);

                }
                break;
        }

    }

    public IEnumerator changeMaJiangQuanToPerson(int personid)
    {
        Debug.Log("轮转到:" + personid);
        
        if(personid == getNextPersonId(youxixingxi.Instance.myid))
        {
            List<Transform> list = new List<Transform>();
            for(int i = 0;i<linshi.childCount;i++)
            {
                list.Add(linshi.GetChild(i));
            }
            for(int i=0;i<list.Count;i++)
            {
                Destroy(list[i].gameObject);
            }
        }
        youxixingxi.Instance.currentPersonId = personid;
        Transform player = players[personid];
        int majiangzhucount = player.GetChild(0).childCount;

        
        if (majiangzhucount % 3 == 1)//补一麻将
        {
            Debug.Log("补一麻将");
            Transform majiang = getNextMaJiangTransform();
            if(youxixingxi.Instance.bianshemajiang)
            {
                youxixingxi.Instance.bianshemajiang.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
            }
            majiang.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 1, 0.6f,1f);
            youxixingxi.Instance.bianshemajiang = majiang;
            if (majiang)
            {
                StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(majiang, personid, 0));
                yield return StartCoroutine(doAnimation(personid,7));
                playerctrls[personid].bumajiang = true;
            }
            else
            {
                //流局
                Debug.Log("流局");
                yield return StartCoroutine(doAnimation(personid,8));
                //SceneManager.LoadScene("麻将");
            }
            

        }
       
        if (playerctrls[personid].wanjialuexing == 1)
        {
            playerctrls[personid].wofangchumajiang = true;//自动出麻将
        }
        else if (playerctrls[youxixingxi.Instance.currentPersonId].wanjialuexing == 0)//本机玩家
        {
            
            unshowUIButtons();
            if (playerctrls[personid].bumajiang)//补麻将
            {
                //判断能不能暗杠或胡，显示UI
                playerctrls[personid].wofangchumajiang = true;//我方选择出麻将
                playerctrls[personid].wofangfirst = true;
                playerctrls[personid].chulijieguo = -1;
                while (playerctrls[personid].chulijieguo == -1)
                { 
                    yield return null;
                }
                StartCoroutine(UIMgr.Instance.chuMaJiang(personid, youxixingxi.Instance.currentchudemajiang));
            }
            
        }
    }
    public IEnumerator doAnimation(int personid,int id)
    {
        switch(id)
        {
            case 8://流局时的动画
                saySomething("流局");
                for (int i = 0; i < 4; i++)
                {
                    showNewText(i, youxixingxi.Instance.wanjianame[i] + "胡了" + youxixingxi.Instance.wanjiahuchishu[i] + "次");
                }
                yield return new WaitForSeconds(5.0f);
                hua.enabled = false;
                SceneManager.LoadScene("麻将");
                break;
            case 4://胡了时的动画
                
                saySomething(youxixingxi.Instance.wanjianame[personid] + "胡了");
                for (int i = 0; i < 4; i++)
                {
                    showNewText(i, youxixingxi.Instance.wanjianame[i] + "胡了" + youxixingxi.Instance.wanjiahuchishu[i] + "次");
                }
                yield return new WaitForSeconds(5.0f);
                hua.enabled = false;
                SceneManager.LoadScene("麻将");
                break;
            case 3://暗杠时的动画
                saySomething(youxixingxi.Instance.wanjianame[personid] + "要杠");
                yield return new WaitForSeconds(2.0f);
                hua.enabled = false;
                break;
            case 5://杠时的动画
                saySomething(youxixingxi.Instance.wanjianame[personid] + "要杠");
                yield return new WaitForSeconds(2.0f);
                hua.enabled = false;
                break;
            case 2://碰时的动画
                saySomething(youxixingxi.Instance.wanjianame[personid] + "要碰");
                yield return new WaitForSeconds(2.0f);
                hua.enabled = false;
                break;
            case 1://吃时的动画
                saySomething(youxixingxi.Instance.wanjianame[personid] + "要吃");
                yield return new WaitForSeconds(2.0f);
                hua.enabled = false;
                break;
            case 6://出单牌时的动画
                yield return new WaitForSeconds(1.0f);
                break;
            case 7://摸牌时的动画
                if (youxixingxi.Instance.currentPersonId != youxixingxi.Instance.myid)
                    yield return new WaitForSeconds(1.0f);
                else
                    yield return null;
                break;
        }
    }
    public IEnumerator chuMaJiang(int personid,Transform majiang)
    {
        if (playerctrls[personid].chulijieguo == 4)
        {
            youxixingxi.Instance.isgameover = true;
            //把新出的麻将添加到personid的麻将组里，再把所有的堆0中麻将移除到堆1中
            feimajiang.RemoveAt(feimajiang.Count - 1);
            yield return StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(youxixingxi.Instance.currentchudemajiang, personid, 0));
            paiXuZhuAndShow(personid, 0);
            Transform[] majiangs = new Transform[majiangzhu[personid].childCount];
            for (int i = 0; i < majiangzhu[personid].childCount; i++)
            {
                majiangs[i] = majiangzhu[personid].GetChild(i);
            }
            yield return StartCoroutine(moveSomeMaJiangToPersonMaJiangZhu(majiangs, personid, 1));
            Debug.Log(personid + "胡了");
            youxixingxi.Instance.wanjiahuchishu[personid]++;
            yield return StartCoroutine(doAnimation(personid,4));
            //SceneManager.LoadScene("麻将");
            
        }
        else if (playerctrls[personid].chulijieguo == 3)
        {
            Debug.LogWarning("暗杠");
            //an gang
            List<Transform> list = new List<Transform>();
            int shu = majiangzhu[personid].childCount;
            for (int i = 0; i < shu; i++)
            {
                Transform ctr = majiangzhu[personid].GetChild(i);
                list.Add(ctr);
            }
            list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));

            List<Transform> temp = new List<Transform>();
            int index = 0;
            for (int j = 0; j < playerctrls[personid].yaochudemajiangshuzhu.Count; j++)
            {
                
                for (int i = index; i < shu; i++)
                {
                    if (System.Convert.ToInt32(list[i].name) == playerctrls[personid].yaochudemajiangshuzhu[j])
                    {
                        temp.Add(list[i]);
                        index = i + 1;
                        break;
                    }
                    index = i + 1;
                }
            }
            yield return StartCoroutine(doAnimation(personid,3));
            yield return StartCoroutine(moveSomeMaJiangToPersonMaJiangZhu(temp, personid, 1));
            yield return StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(getEndMaJiangTransform(), personid, 0));
            yield return StartCoroutine(changeMaJiangQuanToPerson(personid));

        }
        else if(playerctrls[personid].chulijieguo == 0)
        {
            Debug.Log(personid + "出麻将" + majiang.name);
            youxixingxi.Instance.currentchudemajiang = majiang;
            majiang.SetParent(dachudemajiangzhu[youxixingxi.Instance.currentPersonId]);
            feimajiang.Add(majiang);
            youxixingxi.Instance.currentchudemajiangoldparent = majiang.parent;
            paiXuZhuAndShow(youxixingxi.Instance.currentPersonId, 0);
            paiXuZhuAndShow(youxixingxi.Instance.currentPersonId, 2);

            yield return StartCoroutine(chuMaJiangChuLi(personid, majiang));
        }
    }

    IEnumerator chuMaJiangChuLi(int personid, Transform majiang)
    {

        int id1 = getNextPersonId(personid);
        int id2 = getNextPersonId(id1);
        int id3 = getNextPersonId(id2);
        playerctrls[id1].chulijieguo = -1;
        playerctrls[id2].chulijieguo = -1;
        playerctrls[id3].chulijieguo = -1;

        playerctrls[id1].yaobuyaochuli = true;
        playerctrls[id2].yaobuyaochuli = true;
        playerctrls[id3].yaobuyaochuli = true;
        
        
        while (playerctrls[id1].chulijieguo == -1 || playerctrls[id2].chulijieguo == -1 || playerctrls[id3].chulijieguo == -1)
        {
            yield return null;
        }
        Debug.Log("都有回复了，可以判断有没有人要了");
        int imax = 0;
        if(playerctrls[id2].chulijieguo == 1 )
        {
            playerctrls[id2].chulijieguo = 0;

        }
        if (playerctrls[id3].chulijieguo == 1)
        {
            playerctrls[id3].chulijieguo = 0;
        }


            if (playerctrls[id1].chulijieguo > imax)
         {
            imax = playerctrls[id1].chulijieguo;
         }
            if (playerctrls[id2].chulijieguo > imax)
        {
            imax = playerctrls[id2].chulijieguo;
        }
        if (playerctrls[id3].chulijieguo > imax)
        {
            imax = playerctrls[id3].chulijieguo;
        }
        if (imax == 0)//没人要
        {
            Debug.Log("没人要");
            StartCoroutine(changeMaJiangQuanToPerson(getNextPersonId(personid)));
         
        }
        else if (imax >= 2)//有碰杠或胡优先
        {
            Debug.Log("有杠或胡优先");
            if (playerctrls[id1].chulijieguo == imax)
            {
                Debug.Log(id1 + "要了");
               
                StartCoroutine(showWithPlayerCtrlProcess(id1));
                yield return StartCoroutine(doAnimation(id1, imax));
                StartCoroutine(changeMaJiangQuanToPerson(id1));
             
            }
            else if (playerctrls[id2].chulijieguo == imax)
            {
                Debug.Log(id2 + "要了");
                
                 StartCoroutine(showWithPlayerCtrlProcess(id2));
                yield return StartCoroutine(doAnimation(id2, imax));
                StartCoroutine(changeMaJiangQuanToPerson(id2));
            }
            else if (playerctrls[id3].chulijieguo == imax)
            {
                Debug.Log(id3 + "要了");
                
                 StartCoroutine(showWithPlayerCtrlProcess(id3));
                yield return StartCoroutine(doAnimation(id3, imax));
                StartCoroutine(changeMaJiangQuanToPerson(id3));
            }
        }
        else
        {
            if (playerctrls[id1].chulijieguo > 0)
            {
                Debug.Log(id1 + "要了");
               
                 StartCoroutine(showWithPlayerCtrlProcess(id1));
                yield return StartCoroutine(doAnimation(id1, playerctrls[id1].chulijieguo));

                StartCoroutine(changeMaJiangQuanToPerson(id1));
            }
            else if (playerctrls[id2].chulijieguo > 0)
            {
                Debug.Log(id2 + "要了");
                
                StartCoroutine(showWithPlayerCtrlProcess(id2));
                yield return StartCoroutine(doAnimation(id2, playerctrls[id2].chulijieguo));
                StartCoroutine(changeMaJiangQuanToPerson(id2));
            }
            else if (playerctrls[id3].chulijieguo > 0)
            {
                Debug.Log(id3 + "要了");
               
                StartCoroutine(showWithPlayerCtrlProcess(id3));
                yield return StartCoroutine(doAnimation(id3, playerctrls[id3].chulijieguo));
                StartCoroutine(changeMaJiangQuanToPerson(id3));
            }
        }

    }
   
       
   
    IEnumerator showWithPlayerCtrlProcess(int personid)
    {
        switch(playerctrls[personid].chulijieguo)
        {
            case 4://胡
                youxixingxi.Instance.isgameover = true;
                //把新出的麻将添加到personid的麻将组里，再把所有的堆0中麻将移除到堆1中
                feimajiang.RemoveAt(feimajiang.Count - 1);
                yield return StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(youxixingxi.Instance.currentchudemajiang, personid, 0));
                Instance.paiXuZhuAndShow(personid, 0);
                Transform[] majiangs = new Transform[majiangzhu[personid].childCount];
                for (int i = 0; i < Instance.majiangzhu[personid].childCount; i++)
                {
                    majiangs[i] = Instance.majiangzhu[personid].GetChild(i);
                }
                yield return StartCoroutine(moveSomeMaJiangToPersonMaJiangZhu(majiangs, personid, 1));
                Debug.Log(personid + "胡了");
                youxixingxi.Instance.wanjiahuchishu[personid]++;
                yield return StartCoroutine(doAnimation(personid,4));
                //SceneManager.LoadScene("麻将");
                
                break;
            default:
                //把新出的麻将添加到personid的麻将组里，再把yaochudemajiangshuzhu从中移除到堆1中
                feimajiang.RemoveAt(feimajiang.Count - 1);
                yield return StartCoroutine(moveOneMaJiangToPersonMaJiangZhu(youxixingxi.Instance.currentchudemajiang, personid, 0));
                paiXuZhuAndShow(personid, 0);

                List<Transform> list = new List<Transform>();
                int shu = majiangzhu[personid].childCount;
                for (int i = 0; i < shu; i++)
                {
                    Transform ctr = majiangzhu[personid].GetChild(i);
                    list.Add(ctr);
                }
                list.Sort((x, y) => System.Convert.ToInt32(x.name).CompareTo(System.Convert.ToInt32(y.name)));
               
                List<Transform> temp = new List<Transform>();
                int index = 0;
                for (int j = 0; j < playerctrls[personid].yaochudemajiangshuzhu.Count; j++)
                {
                    
                    for (int i = index; i < shu; i++)
                    {
                        if(System.Convert.ToInt32(list[i].name) == playerctrls[personid].yaochudemajiangshuzhu[j])
                        {
                            temp.Add(list[i]);
                            index = i + 1;
                            break;
                        }
                        index = i+1;
                    }
                }
               
                yield return StartCoroutine(moveSomeMaJiangToPersonMaJiangZhu(temp, personid, 1));
                break;
           

        }
    }
    //IEnumerator waitChuMaJiangChuLiAndPlayAnimation(int personid)
    //{
    //    if (personid == youxixingxi.Instance.myid)//wo fang yao bu yao?
    //    {
    //        if (!checkShowUIButtons())   //要不起
    //            unshowUIButtons();
    //    }
    //    while (playerctrls[personid].chulijieguo == -1)
    //    {
    //        yield return null;
    //    }
    //}
    public int getNextPersonId(int id)//得到下一个人ID you bian
    {
        if (id < 3)
            return id + 1;
        else
            return 0;
    }
    public int getRightPersonId(int id)//得到下一个人ID you bian
    {
        if (id < 3)
            return id + 1;
        else
            return 0;
    }
    public int getLeftPersonId(int id)//得到上一个人ID 左 bian
    {
        if (id > 1)
            return id - 1;
        else
            return 3;
    }
    public void gotoNextPerson()//变成下一个人
    {
        youxixingxi.Instance.currentPersonId = getNextPersonId(youxixingxi.Instance.currentPersonId);
    }
    //从中间的麻将堆得到前面或尾部一个麻将给某人的某组麻将
    //人分为0，1，2，3
    //人的麻将组分为0,1,2 对应手中的麻将、吃碰杠的麻将、没人要的废麻将
    Transform getNextMaJiangTransform() //取出下一个麻将
    {
        int majiangid;
        if(nexthasup)
        {
           
            majiangid = ibegin * 2;
        }
        else
        {
            majiangid = ibegin * 2 + 1;
        }
        if (majiangid < 0)
        {
                majiangid = 136 + majiangid;
        }
        if (majiangshu > 0)
        {
            Transform majiang;
            majiang = majiangall.GetChild(majiangid).transform.GetChild(0);
            majiangshu--;
            nexthasup = !nexthasup;
            if(nexthasup)
                ibegin--;

            return majiang;
        }
        else
        {
            return null;
        }

    }
    Transform getEndMaJiangTransform() //从尾部取出下一个麻将
    {
        int majiangid;
        if (endhasup)
        {

            majiangid = iend * 2;
        }
        else
        {
            majiangid = iend * 2 + 1;
        }
        if (majiangid > 135)
        {
            majiangid = majiangid -136;
        }
        if (majiangshu > 0)
        {
            Transform majiang;
            majiang = majiangall.GetChild(majiangid).transform.GetChild(0);
            majiangshu--;
            endhasup = !endhasup;
            if (endhasup)
                iend++;

            return majiang;
        }
        else
        {
            return null;
        }

    }


    public IEnumerator moveOneMaJiangToPersonMaJiangZhu(Transform majiang, int personid, int majiangzhuid)//只对majiangzhuid为0有效
    {
        switch (majiangzhuid)
        {
            case 0:
                while (isanimation)
                {
                    yield return null;
                }
                isanimation = true;
                switch (personid)
                {
                    case 0:
                        if (youxixingxi.Instance.myid == personid)
                        {

                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(cubekuang.transform.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));

                        }
                        else
                        {

                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(nanpos.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                        }
                        break;
                    case 1:
                        if (youxixingxi.Instance.myid == personid)
                        {


                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(cubekuang.transform.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));

                        }
                        else
                        {

                            majiang.parent = majiangzhu[personid];

                            majiang.DOMove(dongpos.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                  
                        }

                        break;
                    case 2:
                        if (youxixingxi.Instance.myid == personid)
                        {


                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(cubekuang.transform.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                    

                        }
                        else
                        {


                            majiang.parent = majiangzhu[personid];

                            majiang.DOMove(beipos.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                        
                        }
                        break;
                    case 3:
                        if (youxixingxi.Instance.myid == personid)
                        {


                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(cubekuang.transform.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                           

                        }
                        else
                        {


                            majiang.parent = majiangzhu[personid];
                            majiang.DOMove(xipos.position, famajiangshijian).OnComplete(() => onMoveComplete(personid, 0));
                         
                        }
                        break;

                }
               


                break;
         

        }
        yield return new WaitForSeconds(yidongmajiangshijian);
      
    }

    public IEnumerator moveSomeMaJiangToPersonMaJiangZhu(Transform []majiangs, int personid, int majiangzhuid) //只对majiangzhuid为1有效
    {
        int num = majiangs.Length;
       

        for (int i = 0; i < num; i++)
        {
            switch (majiangzhuid)
            {
                case 1:
                    majiangs[i].parent = pengdemajiangzhu[personid];
                    paiXuZhuAndShow(personid, 0);
                    paiXuZhuAndShow(personid, 1);
                 break;

            }
        }
        yield return new WaitForSeconds(yidongmajiangshijian);
    }
    public IEnumerator moveSomeMaJiangToPersonMaJiangZhu(List<Transform> majiangs, int personid, int majiangzhuid) //只对majiangzhuid为1有效
    {
        int num = majiangs.Count;


        for (int i = 0; i < num; i++)
        {
            switch (majiangzhuid)
            {
                case 1:
                    majiangs[i].parent = pengdemajiangzhu[personid];
                  
                    break;

            }
        }
        paiXuZhuAndShow(personid, 0);
        paiXuZhuAndShow(personid, 1);
        yield return new WaitForSeconds(yidongmajiangshijian);
    }
    static int Partition(int[] A, int p, int q)
    {
        int i, j, x, t;
        x = A[q];
        i = p - 1;
        for (j = p; j <= q; j++)
            if (A[j] < x)
            {
                i++;
                t = A[j];
                A[j] = A[i];
                A[i] = t;
            }
        A[q] = A[i + 1];
        A[i + 1] = x;
        return i + 1;
    }
    /*
    递归调用的QuickSort程序
    */
    static void QuickSort(int[] A, int p, int r)
    {
        if (p < r)
        {
            int q = Partition(A, p, r);
            QuickSort(A, p, q - 1);
            QuickSort(A, q + 1, r);
        }
    }
    void showKeYiChuDeShuZhu(List<int[]> shuzhulist)
    {
        float _x_N = 0.75f;//x位置差值
       
        for (int i = 0;i< shuzhulist.Count;i++)
        {
            duohang++;
            Vector3 left = wodemajiang.position - new Vector3(_x_N, 0, 0) * 4 + new Vector3(0, duohang * yheight, 0);
            int num = 0;
            List<Transform> oldlist = new List<Transform>();
            for(int k = 0;k<majiangzhu[youxixingxi.Instance.myid].childCount;k++)
            {
                oldlist.Add(majiangzhu[youxixingxi.Instance.myid].GetChild(k));
            }
            oldlist.Add(youxixingxi.Instance.currentchudemajiang);
            List<Transform> list = new List<Transform>();
        
            for (int j = 0;j< shuzhulist[i].Length;j++)
            {
               
                for(int b = 0;b<oldlist.Count;b++)
                {
                    if(shuzhulist[i][j] == System.Convert.ToInt32(oldlist[b].name))
                    {
                        var obj = Instantiate(oldlist[b]);
                        obj.parent = GameObject.Find("linshi").transform;
                        list.Add(obj);
                 
                        break;
                    }
                }
            }
           
            for (int j =0;j<list.Count;j++)
            {
                list[j].gameObject.AddComponent<majiangzhuinfo>();
                majiangzhuinfo info = list[j].GetComponent<majiangzhuinfo>();

                for (int k = 0; k < list.Count; k++)
                {
                    
                    string majiangname = list[k].name;
                    int endIndex = majiangname.IndexOf("(");
                    var name = majiangname.Substring(0, endIndex);
                    Debug.Log("name:" + name);
                    info.majianglist.Add(System.Convert.ToInt32(name));
                }
                list[j].eulerAngles = new Vector3(-90, 0, 0) ;
                list[j].position = left + new Vector3(_x_N, 0, 0) * num;
                num++;
            }
        }
    }
    float yheight = 1.2f;
    int duohang = 1;
    public void woFangHuiHe()
    {
        duohang = 1;
        playertools[youxixingxi.Instance.myid].keyihudeshuzhu.Clear();
        playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Clear();
        int shumu = majiangzhu[youxixingxi.Instance.myid].childCount;
        int[] shuzhu = new int[shumu];
        for (int i = 0; i < shumu; i++)
        {
            shuzhu[i] = System.Convert.ToInt32(majiangzhu[youxixingxi.Instance.myid].GetChild(i).name);
        }
        QuickSort(shuzhu, 0, shumu - 1);
        for (int i = 0; i < shumu; i++)
        {
            if ((i + 3) <= shumu - 1)//找暗杠
            {
                if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2] && shuzhu[i] == shuzhu[i + 3])
                {

                       int[] temp = new int[4] { shuzhu[i],shuzhu[i],shuzhu[i],shuzhu[i] };

                        playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Add(temp);
                        i += 2;
                        continue;
                    
                }
            }
        }
        int[] newshuzhu = new int[shuzhu.Length - 1];
        for(int i = 0;i<newshuzhu.Length;i++)
        {
            newshuzhu[i] = shuzhu[i];
        }
        playertools[youxixingxi.Instance.myid].canHu(shuzhu[shuzhu.Length - 1], newshuzhu);
        if (playertools[youxixingxi.Instance.myid].keyihudeshuzhu.Count > 0)//显示胡
        {
            showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyihudeshuzhu);
        }

        if (playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Count > 0)//显示杠
        {

            showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyigangdeshuzhu);
        }
    }
    public bool checkShowUIButtons()
    {
        duohang = 1;
        playertools[youxixingxi.Instance.myid].keyihudeshuzhu.Clear();
        playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Clear();
        playertools[youxixingxi.Instance.myid].keyipengdeshuzhu.Clear();
        playertools[youxixingxi.Instance.myid].keyichideshuzhu.Clear();

        int currentchudemajiangshu = System.Convert.ToInt32(youxixingxi.Instance.currentchudemajiang.name);
        Debug.Log("currentchudemajiangshu" + currentchudemajiangshu);
        int shumu = majiangzhu[youxixingxi.Instance.myid].childCount;
        int[] shuzhu = new int[shumu];
        for (int i = 0; i < shumu; i++)
        {
            shuzhu[i] = System.Convert.ToInt32(majiangzhu[youxixingxi.Instance.myid].GetChild(i).name);
        }
        QuickSort(shuzhu, 0, shumu - 1);
        playertools[youxixingxi.Instance.myid].PengOrGangShu(currentchudemajiangshu, shuzhu);
        if (getNextPersonId(youxixingxi.Instance.currentPersonId) == youxixingxi.Instance.myid)//判断吃我方能不能吃
        {
            playertools[youxixingxi.Instance.myid].ChiXuanZheShu(currentchudemajiangshu, shuzhu);
        }
        playertools[youxixingxi.Instance.myid].canHu(currentchudemajiangshu, shuzhu);
        if(playertools[youxixingxi.Instance.myid].keyihudeshuzhu.Count == 0 && playertools[youxixingxi.Instance.myid].keyichideshuzhu.Count == 0
           &&  playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Count == 0 && playertools[youxixingxi.Instance.myid].keyipengdeshuzhu.Count == 0)
        {
             buyao();
             return false;
        }
        else
        {
            buyaobutton.SetActive(true);
            //显示可以出的麻将组
            if(playertools[youxixingxi.Instance.myid].keyihudeshuzhu.Count > 0 )//显示胡
            {
                showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyihudeshuzhu);
            }
            if (playertools[youxixingxi.Instance.myid].keyigangdeshuzhu.Count > 0)//显示杠
            {
                showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyigangdeshuzhu);
            }
            if (playertools[youxixingxi.Instance.myid].keyipengdeshuzhu.Count > 0)//显示碰
            {
                showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyipengdeshuzhu);
            }
            if (playertools[youxixingxi.Instance.myid].keyichideshuzhu.Count > 0)//显示吃
            {
                showKeYiChuDeShuZhu(playertools[youxixingxi.Instance.myid].keyichideshuzhu);
            }
            return true;
        }
    }

    public void buyao()
    {

        playerctrls[youxixingxi.Instance.myid].chulijieguo = 0;
        var linshi = GameObject.Find("linshi").transform;
        List<Transform> list = new List<Transform>();
        for(int i = 0;i<linshi.childCount;i++)
        {
            list.Add(linshi.GetChild(i));
        }
        foreach(var obj in list)
        {
            Destroy(obj.gameObject);
        }
        unshowUIButtons();
    }

    public void enableShiJiaoButtons(bool b)
    {
        shijiao1button.SetActive(b);
        shijiao2button.SetActive(b);
        shijiao3button.SetActive(b);
        shijiao4button.SetActive(b);
    }
    public void shijiao1()
    {
        moveCameraToPerson(0);

    }
    public void shijiao2()
    {
        moveCameraToPerson(1);

    }
    public void shijiao3()
    {
        moveCameraToPerson(2);

    }
    public void shijiao4()
    {
        moveCameraToPerson(3);
    }
}
