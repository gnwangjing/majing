using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game.Tool;
public class PlayerMaJiangDataBase : Singleton<PlayerMaJiangDataBase> {

    public Transform MyMaJiang;

    public string MyMaJiangInit(string s)
    {

        MyMaJiang = new GameObject().transform;
        Transform obj = Instantiate(MyMaJiang);
        obj.SetParent(GameObject.Find(s).transform);
        obj.name = "MyMaJiangLoctionAll";
        return s + "/" + obj.name;
    }



}
