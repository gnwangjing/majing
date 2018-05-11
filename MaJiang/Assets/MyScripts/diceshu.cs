using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceshu : MonoBehaviour {

    public int shu = 0;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Dice")
        {
            gameObject.AddComponent<dice>();
        }
    }
}
