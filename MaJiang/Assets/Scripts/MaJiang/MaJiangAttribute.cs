using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game
{
    public enum DIR
    {
        Tiao_1 = 1, Tiao_2, Tiao_3, Tiao_4, Tiao_5, Tiao_6, Tiao_7, Tiao_8, Tiao_9,
        Tong_1, Tong_2, Tong_3, Tong_4, Tong_5, Tong_6, Tong_7, Tong_8, Tong_9,
        Wan_1, Wan_2, Wan_3, Wan_4, Wan_5, Wan_6, Wan_7, Wan_8, Wan_9,
        Dong, Nan, Xi, Bei, Zhong, Fa, Bai



    }
    public class MaJiangAttribute : MonoBehaviour
    {
        public int id;
        public DIR dir;
       
    }
}

