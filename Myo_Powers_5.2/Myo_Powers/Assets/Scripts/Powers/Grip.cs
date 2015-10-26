using UnityEngine;
using System.Collections;

public class Grip : MonoBehaviour
{
    void Start()
    {
        MyoPoseCheck.onUseGrip += UseGrip;
        MyoPoseCheck.onStopGrip += StopUsingGrip;
    }

    void UseGrip()
    {
        Debug.Log ("GRIP ACTIVATED -------------------");
    }

    void StopUsingGrip()
    {
        Debug.Log ("GRIP STOPPED ---------------------");
    }
}
