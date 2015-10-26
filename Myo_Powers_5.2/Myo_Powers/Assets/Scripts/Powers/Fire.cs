using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    void Start()
    {
        MyoPoseCheck.onUseFire += UseFire;
        MyoPoseCheck.onStopFire += StopUsingFire;
    }

    void UseFire()
    {
        Debug.Log ("FIRE ACTIVATED -------------------");
    }

    void StopUsingFire()
    {
        Debug.Log ("FIRE STOPPED ---------------------");
    }
}
