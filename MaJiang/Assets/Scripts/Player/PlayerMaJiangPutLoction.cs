using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaJiangPutLoction : MonoBehaviour {

    public struct PlayerMaJianglocation
    {
        public List<GameObject> location;
    }
    public PlayerMaJianglocation _playerMaJianglocation;

    private void Awake()
    {
        _playerMaJianglocation = new PlayerMaJianglocation();
        _playerMaJianglocation.location = new List<GameObject>();
      
    }
    private void Start()
    {
        PlayerMaJiangLocation();
    }
    
    void PlayerMaJiangLocation()
    {

        string s = PlayerMaJiangDataBase.Instance.MyMaJiangInit("East");
        GameObject obj = new GameObject();

        Transform Trs = GameObject.Find(s).transform;

        for (int i = 0; i < 13; i++) 
        {
            var V = new Vector3(180, 0, 0);
            GameObject _obj = Instantiate(obj);
            _obj.transform.SetParent(Trs);
            _obj.transform.Rotate(V);
            _obj.name = (i + 1).ToString();
            _playerMaJianglocation.location.Add(_obj);
        }


        float x = 0f;
        float y = 0.25f;
        float z = -3.5f;

        float _x = 0.75f;//x差值；

    }
    

}
