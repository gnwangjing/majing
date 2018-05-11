using game.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game
{
    public class MaJiangInit : MonoBehaviour
    {

        [System.Serializable]
        public struct MaJiangPicInfo
        {
            public Transform[] Game;
        }


        public List<Transform> majiangs = new List<Transform>();
        public List<MaJiangPicInfo> MaJiangList;
       
        private void Awake()
        {
            maJiangInit();
            maJiangInitLocation();
        }
        public void maJiangInit()
        {
            GameObject obj2 = new GameObject();
            for (int i = 0; i < MaJiangList.Count; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    GameObject majiang = Instantiate(obj2);
                    majiang.name = "majiang";
                    majiang.AddComponent<weizhixingxi>();
                    majiang.transform.SetParent(transform);
                    Transform obj = Instantiate(MaJiangList[i].Game[j]);
                    obj.SetParent(majiang.transform);
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                    majiangs.Add(majiang.transform);

                }
               
            }
           
        }
        public void maJiangInitLocation()
        {

            int n = 34;
            Vector3 origin = GameObject.Find("MaJiangInitLocation").transform.position;

            #region 南生成
            float x_N = 0.5f;
            float y_N = 0.75f;

            float _x_N = 0.75f;//x位置差值
            float _y_N = -0.5f;//y位置差值
            majiangs[0].position = origin +  new Vector3(x_N, y_N, 0f);
            majiangs[0].GetChild(0).eulerAngles = new Vector3(180, 0, 0);
            majiangs[1].position = origin + new Vector3(x_N, y_N + _y_N, 0f);
            majiangs[1].GetChild(0).eulerAngles = new Vector3(180, 0, 0);
            for (int i = 2; i < n; i++)
            {

              
                majiangs[i].transform.position = new Vector3(majiangs[i - 2].transform.position.x + _x_N, majiangs[i - 2].transform.position.y, majiangs[i - 2].transform.position.z);
                
                majiangs[i].GetChild(0).eulerAngles = new Vector3(180, 0, 0);
               
            }
            #endregion
            #region 东生成
            float x_D = 10.5f;
            float y_D = 0.75f;
            float z_D = 1f;

            float _z_D = 0.75f;//z位置差值
            float _y_D = -0.5f;//y位置差值
            majiangs[34].transform.position = origin + new Vector3(x_D, y_D, z_D);
            majiangs[34].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
            majiangs[35].transform.position = origin + new Vector3(x_D, y_D + _y_D, z_D);
            majiangs[35].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
            for (int i = 36; i < n * 2; i++)
            {
               majiangs[i].transform.position = new Vector3(majiangs[i - 2].transform.position.x, majiangs[i - 2].transform.position.y, majiangs[i - 2].transform.position.z + _z_D);
               majiangs[i].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
            }
            #endregion
            #region 北生成
            float x_B = 9.5f;
            float y_B = 0.75f;
            float z_B = 11f;
            
            float _x_B = -0.75f;//x位置差值
            float _y_B = -0.5f;//y位置差值
            majiangs[68].transform.position = origin + new Vector3(x_B, y_B, z_B);
            majiangs[68].GetChild(0).eulerAngles = new Vector3(180, 180, 0);
            majiangs[69].transform.position = origin + new Vector3(x_B, y_B + _y_B, z_B);
            majiangs[69].GetChild(0).eulerAngles = new Vector3(180, 180, 0);

            for (int i = 70; i < n*3; i++)
            {
               
                 majiangs[i].transform.position = new Vector3(majiangs[i - 2].transform.position.x + _x_B, majiangs[i - 2].transform.position.y, majiangs[i - 2].transform.position.z);
       
                majiangs[i].GetChild(0).eulerAngles = new Vector3(180, 180, 0);
              
            }
            #endregion
            #region 西生成
            float x_X = -0.5f;
            float y_X = 0.75f;
            float z_X = 10f;

            float _z_X = -0.75f;//z位置差值
            float _y_X = -0.5f;//y位置差值
            majiangs[102].transform.position = origin + new Vector3(x_X, y_X, z_X);
            majiangs[102].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
            majiangs[103].transform.position = origin + new Vector3(x_X, y_X + _y_X, z_X);
            majiangs[103].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
            for (int i = 104; i < n*4; i++)
            {
               
                majiangs[i].transform.position = new Vector3(majiangs[i - 2].transform.position.x, majiangs[i - 2].transform.position.y, majiangs[i - 2].transform.position.z + _z_X);
               
                majiangs[i].GetChild(0).eulerAngles = new Vector3(180, 90, 0);
           
            }
            #endregion
        }
    }

}

