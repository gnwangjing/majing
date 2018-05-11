using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game
{
    public class GameInit : MonoBehaviour
    {

        Transform player0;
        Transform player1;
        Transform player2;
        Transform player3;


        private void Awake()
        {
            CreateInit();
        }
        void CreateInit()
        {
            player0 = new GameObject("Player0").transform;

            player0.SetParent(transform);
            player0.tag = "Player";
            
            player1 = new GameObject("Player1").transform;
            player1.SetParent(transform);
            player1.tag = "Player";
            
            player2 = new GameObject("Player2").transform;
            player2.SetParent(transform);
            player2.tag = "Player";
           
            player3 = new GameObject("Player3").transform;
            player3.SetParent(transform);
            player3.tag = "Player";
           
        }
    }
}

