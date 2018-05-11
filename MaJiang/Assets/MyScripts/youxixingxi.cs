using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youxixingxi : Singleton<youxixingxi> {
    public Transform bianshemajiang = null;
    public int currentlookuserid = 0;
    public int zhuangjiaid = 0;
    public int myid = 0;//不为0，1，2，3则为观众
    public string []wanjianame = new string[4] { "南风", "东风","北风","西风"};
    public int[] wanjiahuchishu = new int[4] { 0, 0, 0, 0 };
    public int currentPersonId = 0;//当前的麻将权ID
    public Transform currentchudemajiang;
    public Transform currentchudemajiangoldparent;
    public bool isgameover = false;
    public bool isxunhuan = false;
    void Awake()
    {
        currentlookuserid = 0;//初始视角为0
    }
}
