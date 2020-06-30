using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SendTringer : MonoBehaviour
{
    private bool isTriggered = false;
    public GameObject serverobject;
    public GameObject logObject;
    private void OnTriggerEnter()
    {
        if (!isTriggered)
        {
            //Log logFile;
            TimeSpan totalWalkTime = Log.totalWalkTime;
            TimeSpan totalRunningTime = Log.totalRuntime;
            int totalJumps = Log.numberOfJumps;
            TimeSpan totalCrouchtime = Log.totalCrouchtime;
            string message = "" + ( (totalWalkTime.TotalSeconds+totalCrouchtime.TotalSeconds)/(totalWalkTime.TotalSeconds + totalCrouchtime.TotalSeconds + totalRunningTime.TotalSeconds))+"_"+ (totalRunningTime.TotalSeconds / (totalWalkTime.TotalSeconds + totalCrouchtime.TotalSeconds + totalRunningTime.TotalSeconds))+"_"+ (totalJumps / (totalWalkTime.TotalSeconds + totalCrouchtime.TotalSeconds + totalRunningTime.TotalSeconds));

            Debug.Log("Walk time : "+totalWalkTime);
            Debug.Log("run time : " + totalRunningTime);
            Debug.Log("crouch time : " + totalCrouchtime);
            Debug.Log("Jums : " + totalJumps);
            server.send_data(message);
        }
    }

}
