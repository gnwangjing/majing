using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseHover : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler{
    public bool ismousein;
    public Transform current;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        ismousein = true;
        if(transform.name == "chi")
        {
            UIMgr.Instance.showchixijie = true;
        }
        else if(transform.name == "peng")
        {
            UIMgr.Instance.showpengxijie = true;
        }
        else if(transform.name == "gang")
        {
            UIMgr.Instance.showgangxijie = true;
        }
        else if(transform.name == "hu")
        {
            UIMgr.Instance.showhuxijie = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ismousein = false;
    }

  
}
