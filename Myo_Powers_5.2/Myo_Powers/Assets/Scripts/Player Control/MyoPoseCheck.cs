using UnityEngine;
using System.Collections;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyoPoseCheck : MonoBehaviour
{
    public bool isPoseCheckEnabled = true;

    [Header ("Myo")]
    public GameObject myoGameObject;

    [Header ("Settings")]
    public float checkNewMyoPoseRate = 1f;

    private float nextMyoPoseCheck = 0f;

    private bool isLightningOn = false;
    private bool isFireOn = false;
    private bool isGripOn = false;

    Pose lastMyoPose;

    ThalmicMyo myo;

    public delegate void PoseAction();
    public static event PoseAction onUseLightning;
    public static event PoseAction onStopLightning;
    public static event PoseAction onUseFire;
    public static event PoseAction onStopFire;
    public static event PoseAction onUsePush;
    public static event PoseAction onUsePull;
    public static event PoseAction onUseGrip;
    public static event PoseAction onStopGrip;

    void Start()
    {
        myo = myoGameObject.GetComponent<ThalmicMyo> ();
    }

    void Update()
    {
        if(isPoseCheckEnabled)
        {
            GetCurrentPose ();
            GetPower ();
        }
    }

    void GetCurrentPose()
    {
        if(Time.time > nextMyoPoseCheck)
        {
            lastMyoPose = myo.pose;

            nextMyoPoseCheck = Time.time + checkNewMyoPoseRate;
        }
    }

    void GetPower()
    {
        switch(myo.pose)
        {
            case Pose.FingersSpread:
                if(onUseLightning != null)
                {
                    onUseLightning ();
                    isLightningOn = true;
                }

                StopUsingGrip ();
                StopUsingFire ();
                break;
            case Pose.Fist:
                if(onUseGrip != null)
                {
                    onUseGrip ();
                    isGripOn = true;
                }

                StopUsingFire ();
                StopUsingLightning ();
                break;
            case Pose.WaveIn:
                StopUsingFire ();
                StopUsingGrip ();
                StopUsingLightning ();
                break;
            case Pose.WaveOut:
                StopUsingFire ();
                StopUsingGrip ();
                StopUsingLightning ();
                break;
            case Pose.DoubleTap:
            case Pose.Rest:
            case Pose.Unknown:
                StopUsingFire ();
                StopUsingGrip ();
                StopUsingLightning ();
                break;
        }
    }

    void StopUsingLightning()
    {
        if(isLightningOn)
        {
            onStopLightning ();
            isLightningOn = false;
        }
    }

    void StopUsingFire()
    {
        if(isFireOn)
        {
            onStopFire ();
            isFireOn = false;
        }
    }

    void StopUsingGrip()
    {
        if(isGripOn)
        {
            onStopGrip ();
            isGripOn = false;
        }
    }
}
