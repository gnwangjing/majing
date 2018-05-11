using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour {
    bool hasdown = false;
    Rigidbody rg;
	// Use this for initialization
	void Start () {
        rg = transform.GetComponent<Rigidbody>();
        StartCoroutine(startDown());
	}
	IEnumerator startDown()
    {
        yield return new WaitForSeconds(2.0f);
        hasdown = true;
    }
	// Update is called once per frame
	void Update () {
        if(hasdown)
        {
           
            if(Vector3.Equals(rg.velocity,new Vector3(0,0,0)))
            {
                // Debug.Log("stop");
                int num = 1;
               float ymax = transform.Find("1").transform.position.y;
                for (int i = 2; i <= 6; i++)
                {
                    float ytemp = transform.Find(i.ToString()).transform.position.y;
                    if ( ytemp > ymax)
                    {
                        num = i;
                        ymax = ytemp;
                    }
                }
                transform.GetComponent<diceshu>().shu = num;
                Destroy(this);
            }
              
        }
		
	}
}
