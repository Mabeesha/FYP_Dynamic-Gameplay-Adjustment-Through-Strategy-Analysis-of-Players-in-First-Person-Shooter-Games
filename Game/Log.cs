using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class Log : MonoBehaviour
{
    public static StreamWriter sw1;
    private static bool isCrouched = false;
    public string previousAction = "";
    private GameObject health;
    //private GlobalHealth healthScript;
    private int currentHealth = 100;
    string m_Path;
    string path;
   
    private vp_PlayerEventHandler m_Player = null;  // should never be referenced directly
    private GameObject uiScript;
    public string uidata;

   /// <summary>
   /// /////////////////////////////////////   data for ML model ////////////////////////////////////
   /// </summary>
    private DateTime walkingStartTime;
    private DateTime crouchStartTime;
    private DateTime runningStartTime;
    public static int numberOfJumps = 0;
    public static TimeSpan totalWalkTime ;
    public static TimeSpan totalRuntime ;
    public static TimeSpan totalCrouchtime ;


    void Awake()
    {
        uiScript= GameObject.Find("UIscript");
        uidata = UIscript.result;
        if (m_Player == null)
            m_Player = transform.GetComponent<vp_PlayerEventHandler>();
        Debug.Log("From Log: "+ uidata);
        sw1 = new StreamWriter(Application.persistentDataPath + "/GameLog2_" + DateTime.Now.ToString("h_mm_ss_tt") + "_"+uidata+".txt");
        //sw1 = new StreamWriter( "GameLog2_" + DateTime.Now.ToString("h_mm_ss_tt") + ".txt");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Player = transform.GetComponent<vp_PlayerEventHandler>();
        path = Directory.GetCurrentDirectory();
        m_Path = Application.dataPath;
        Debug.Log("path : " + path);
        Debug.Log("path : " + m_Path);
        //health = GameObject.FindWithTag("GlobalHealth");
        //healthScript = health.GetComponent<GlobalHealth>();
        
    }



    // Update is called once per frame
    void Update()
    {
        actionLogger();
        //logPlayerHealth();
    }
    
    void logPlayerHealth()
    {
        

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (m_Player == null)
            {
                Debug.Log("Player Null");
            }
            else
            {
                Debug.Log(m_Player.Health.Get() * 100f);
            }
            
        }
    }
    

    public static void logZombieDie()
    {
        Debug.Log("Zombie_dead : " + PrintTime());
  //      sw1.WriteLine("Zombie_dead :   " + PrintTime());

    }

    public static void logByAnotherObject(string line)
    {
        Debug.Log(line + PrintTime());
        sw1.WriteLine(line + PrintTime());
    }

    private static string PrintTime()
    {
        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;


        string timeString = DateTime.Now.ToString("h:mm:ss.fff tt");

        //Debug.Log("Time Stamp : " + DateTime.Now.ToString("h_mm_ss.fff tt"));
        return timeString;

    }

    private static TimeSpan getTimeDifference(DateTime startTime, DateTime endTime)
    {
        return (endTime - startTime);
    }

    private void actionLogger()
    {

        // bool isWalk=false;
        //bool isRun=false;

        // reload log
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!previousAction.Equals("reload_weapon"))
            {
                previousAction = "reload_weapon";
                sw1.WriteLine("reload_weapon   " + PrintTime());
                Debug.Log("reload_weapon : " + PrintTime());
            }
        }


        if (Input.GetKeyDown(KeyCode.C))
        {

            isCrouched = true;
            if (!previousAction.Equals("Crouch_started"))
            {
                crouchStartTime = DateTime.Now;
                
                previousAction = "Crouch_started";
                sw1.WriteLine("Crouch_started   " + PrintTime());
                //Debug.Log("Crouch_started : " + PrintTime());

            }
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouched = false;
            if (!previousAction.Equals("Crouch_end"))
            {
                totalCrouchtime += getTimeDifference(crouchStartTime,DateTime.Now);
                previousAction = "Crouch_end";
                sw1.WriteLine("Crouch_end   " + PrintTime());
                //Debug.Log("Crouch_end : " + PrintTime());
                Debug.Log("totalCrouchtime +"+ totalCrouchtime.TotalSeconds);


            }
        }

            if (Input.GetKeyUp(KeyCode.W) || (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (!previousAction.Equals("Walking_Forward_END"))
            {
                if (!previousAction.Equals("Running_Forward") )
                {
                    if (!isCrouched || isCrouched)
                    {
                        previousAction = "Walking_Forward_END";
                        sw1.WriteLine("Walking_Forward_END   " + PrintTime());
                        totalWalkTime += getTimeDifference(walkingStartTime,DateTime.Now);
                        Debug.Log("totalWalktime +" + totalWalkTime.TotalSeconds);
                        //Debug.Log("Walking_Forward_END : " + PrintTime());
                    }
                    else
                    {
                        // previousAction = "Walking_Forward_END";
                        // sw.WriteLine("Crouched_&&_Walking_Forward_END   " + PrintTime());
                        // Debug.Log("Crouched_&&_Walking_Forward_END : " + PrintTime());
                    }

                }
                else
                {
                    if (!previousAction.Equals("Running_Forward_END"))
                    {
                        totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                        Debug.Log("totalRuntime +" + totalRuntime.TotalSeconds);
                        previousAction = "Running_Forward_END";
                        sw1.WriteLine("Running_Forward_END   " + PrintTime());
                        //Debug.Log("Running_Forward_END : " + PrintTime());
                    }

                }

            }

        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Forward_END"))
                {
                    totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                    Debug.Log("totalRuntime +" + totalRuntime.TotalSeconds);
                    previousAction = "Running_Forward_END";
                    sw1.WriteLine("Running_Forward_END   " + PrintTime());
                    //Debug.Log("Running_Forward_END : " + PrintTime());
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.W) && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            if (!isCrouched || isCrouched)
            {
                if (!previousAction.Equals("Walking_Forward"))
                {
                    walkingStartTime = DateTime.Now;
                    previousAction = "Walking_Forward";
                    sw1.WriteLine("Walking_Forward   " + PrintTime());
                    //Debug.Log("Walking_Forward : " + PrintTime());
                }
            }
            else
            {

            }
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Forward"))
                {
                    runningStartTime = DateTime.Now;
                    previousAction = "Running_Forward";
                    sw1.WriteLine("Running_Forward   " + PrintTime());
                    //Debug.Log("Running_Forward : " + PrintTime());
                }
            }

        }
        // step back

        if (Input.GetKeyUp(KeyCode.S) || (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (!previousAction.Equals("Walking_Backword_END"))
            {
                if (!previousAction.Equals("Running_Backword") )
                {
                    if (!isCrouched || isCrouched)
                    {
                        totalWalkTime += getTimeDifference(walkingStartTime, DateTime.Now);
                        Debug.Log("totalWalktime +" + totalWalkTime.TotalSeconds);
                        previousAction = "Walking_Backword_END";
                        sw1.WriteLine("Walking_Backword_END   " + PrintTime());
                       // Debug.Log("Walking_Backword_END : " + PrintTime());
                    }
                    else
                    {

                    }

                }
                else
                {
                    if (!previousAction.Equals("Running_Backword_END"))
                    {
                        previousAction = "Running_Backword_END";
                        totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                        Debug.Log("totalRuntime +" + totalRuntime.TotalSeconds);
                        sw1.WriteLine("Running_Backword_END   " + PrintTime());
                        //Debug.Log("Running_Backword_END : " + PrintTime());
                    }

                }

            }

            //Debug.Log("Walking_Forward_END : " + PrintTime());
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Backword_END"))
                {
                    totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                    Debug.Log("totalRuntime +" + totalRuntime.TotalSeconds);
                    previousAction = "Running_Backword_END";
                    sw1.WriteLine("Running_Backword_END   " + PrintTime());
                    Debug.Log("Running_Backword_END : " + PrintTime());
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.S) && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            if (!isCrouched || isCrouched)
            {
                if (!previousAction.Equals("Walking_Backword"))
                {
                    walkingStartTime = DateTime.Now;
                    previousAction = "Walking_Backword";
                    sw1.WriteLine("Walking_Backword   " + PrintTime());
                    Debug.Log("Walking_Backword : " + PrintTime());
                }
            }
            else
            {
     
            }
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Backword"))
                {
                    runningStartTime = DateTime.Now;
                    previousAction = "Running_Backword";
                    sw1.WriteLine("Running_Backword   " + PrintTime());
                    Debug.Log("Running_Backword : " + PrintTime());
                }
            }

        }

        /// left move
        if (Input.GetKeyUp(KeyCode.A) || (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (!previousAction.Equals("Walking_Left_END"))
            {
                if (!previousAction.Equals("Running_Left"))
                {
                    if (!isCrouched || isCrouched)
                    {
                        //totalWalkTime += getTimeDifference(walkingStartTime,DateTime.Now);
                        previousAction = "Walking_Left_END";
                        sw1.WriteLine("Walking_Left_END   " + PrintTime());
                        Debug.Log("Walking_Left_END : " + PrintTime());
                    }
                    else
                    {
    
                    }

                }
                else
                {
                    if (!previousAction.Equals("Running_Left_END"))
                    {
                        totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                        previousAction = "Running_Left_END";
                        sw1.WriteLine("Running_Left_END   " + PrintTime());
                        Debug.Log("Running_Left_END : " + PrintTime());
                    }

                }

            }

            //Debug.Log("Walking_Forward_END : " + PrintTime());
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Left_END"))
                {
                    totalRuntime += getTimeDifference( runningStartTime,DateTime.Now );
                    previousAction = "Running_Left_END";
                    sw1.WriteLine("Running_Left_END   " + PrintTime());
                    Debug.Log("Running_Left_END : " + PrintTime());
                }
            }

            //Debug.Log("Running_Forward_END : " + PrintTime());
        }

        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.A) && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            if (!isCrouched || isCrouched)
            {
                if (!previousAction.Equals("Walking_Left"))
                {
                   // walkingStartTime = DateTime.Now;
                    previousAction = "Walking_Left";
                    sw1.WriteLine("Walking_Left   " + PrintTime());
                    Debug.Log("Walking_Left : " + PrintTime());
                }
            }
            else
            {
                /*   if (!previousAction.Equals("Walking_Forward"))
                   {
                     //  previousAction = "Walking_Forward";
                    //   sw.WriteLine("Crouched_&&_Walking_Forward   " + PrintTime());
                     //  Debug.Log("Crouched_&&_Walking_Forward : " + PrintTime());
                   } */
            }
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Left"))
                {
                    runningStartTime = DateTime.Now;
                    previousAction = "Running_Left";
                    sw1.WriteLine("Running_Left   " + PrintTime());
                    Debug.Log("Running_Left : " + PrintTime());
                }
            }

            //isRun = true;
            //Debug.Log("Running_Forward : " + PrintTime());
        }


        // Right move
        if (Input.GetKeyUp(KeyCode.D) || (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (!previousAction.Equals("Walking_Right_END"))
            {
                if (!previousAction.Equals("Running_Right"))
                {
                    if (!isCrouched || isCrouched)
                    {
                       // totalWalkTime += getTimeDifference(walkingStartTime,DateTime.Now);
                        previousAction = "Walking_Right_END";
                        sw1.WriteLine("Walking_Right_END   " + PrintTime());
                        Debug.Log("Walking_Right_END : " + PrintTime());
                    }
                    else
                    {
                        // previousAction = "Walking_Forward_END";
                        // sw.WriteLine("Crouched_&&_Walking_Forward_END   " + PrintTime());
                        // Debug.Log("Crouched_&&_Walking_Forward_END : " + PrintTime());
                    }

                }
                else
                {
                    if (!previousAction.Equals("Running_Right_END"))
                    {
                        totalRuntime += getTimeDifference(runningStartTime, DateTime.Now);
                        previousAction = "Running_Right_END";
                        sw1.WriteLine("Running_Right_END   " + PrintTime());
                        Debug.Log("Running_Right_END : " + PrintTime());
                    }

                }

            }

            //Debug.Log("Walking_Forward_END : " + PrintTime());
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Right_END"))
                {
                    totalRuntime += getTimeDifference(runningStartTime, DateTime.Now );
                    previousAction = "Running_Right_END";
                    sw1.WriteLine("Running_Right_END   " + PrintTime());
                    //Debug.Log("Running_Right_END : " + PrintTime());
                }
            }
            //Debug.Log("Running_Forward_END : " + PrintTime());
        }

        if (Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.D) && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            if (!isCrouched || isCrouched)
            {
                if (!previousAction.Equals("Walking_Right"))
                {
                    //walkingStartTime = DateTime.Now;
                    previousAction = "Walking_Right";
                    sw1.WriteLine("Walking_Right   " + PrintTime());
                    Debug.Log("Walking_Right : " + PrintTime());
                }
            }
            else
            {

            }
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouched)
            {
                if (!previousAction.Equals("Running_Right"))
                {
                    runningStartTime = DateTime.Now;
                    previousAction = "Running_Right";
                    sw1.WriteLine("Running_Right   " + PrintTime());
                    //Debug.Log("Running_Right : " + PrintTime());
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            numberOfJumps += 1;
            Debug.Log("Number of jumps :"+numberOfJumps);
            sw1.WriteLine("Jump   " + PrintTime());
            Debug.Log("Jump : " + PrintTime());
        }
    }

    public static void printCrouchStart()
    {
        isCrouched = true;
        sw1.WriteLine("crouch_started   " + PrintTime());
        Debug.Log("crouch_started : " + PrintTime());

    }

    public static void printCrouchEnd()
    {
        isCrouched = false;
        sw1.WriteLine("crouch_end   " + PrintTime());
        Debug.Log("crouch_end : " + PrintTime());

    }

    



}
