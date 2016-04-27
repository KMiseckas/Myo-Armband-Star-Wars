using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    public DynamicLightning[] lightningScripts;

    void Start()
    {
        MyoPoseCheck.onUseLightning += UseLightning;
        MyoPoseCheck.onStopLightning += StopUsingLightning;
    }

    void UseLightning()
    {
        //Debug.Log ("LIGHTNING ACTIVATED -------------------");

        foreach(DynamicLightning script in lightningScripts)
        {
            script.StartLightning ();
        }

    }

    void StopUsingLightning()
    {
        //Debug.Log ("LIGHTNING STOPPED ---------------------");

    }

}
