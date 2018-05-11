using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.Tool
{
    public class Singleton<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        public static T Instance
        {
            get;
            private set;
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
