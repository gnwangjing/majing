using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name;
                    //切换场景之后不销毁单例对象
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }
    }
