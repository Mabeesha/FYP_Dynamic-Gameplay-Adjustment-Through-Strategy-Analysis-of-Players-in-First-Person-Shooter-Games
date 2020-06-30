using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class server: MonoBehaviour
{
    const int PORT_NO = 1234;
    const string SERVER_IP = "127.0.0.1";
    public static NetworkStream nwStream;
    public static TcpClient client;
    public static string received_data;
    public static double aggressivePercentage=0;
    public static double playerGunDamage=0;
    public static double enemyGunDamage=0;
    public static double healthDecFactor=0;
    public static double Ammo=0;
    public static double TotalEnemyCount=0;
    //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

    // Start is called before the first frame update
    void Start()
    {
        Server_Load();
        UnityEngine.Debug.Log("Server code running !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        receive_data_test();

    }

    public static void receive_data_test()
    {
        //---read back the text---
        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
        received_data = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
        Debug.Log("Received : " + received_data);
        String str = received_data;

        String[] spearator = { "_" };

        // using the method 
        String[] strlist = str.Split(spearator,StringSplitOptions.RemoveEmptyEntries);
 
        Debug.Log(Convert.ToDouble(strlist[1]));
        aggressivePercentage = Convert.ToDouble(strlist[1]);
        playerGunDamage = Convert.ToDouble(strlist[2]);
        enemyGunDamage = Convert.ToDouble(strlist[3]);
        healthDecFactor = Convert.ToDouble(strlist[4]);
        Ammo = Convert.ToDouble(strlist[5]);
        TotalEnemyCount = Convert.ToDouble(strlist[6]);
        /*
        playerGunDamage = Convert.ToDouble(strlist[]);
        enemyGunDamage = Convert.ToDouble(strlist[2]);
        healthDecFactor = Convert.ToDouble(strlist[3]);
        Ammo = Convert.ToDouble(strlist[4]);
        TotalEnemyCount = Convert.ToDouble(strlist[5]);
        */
        Debug.Log("Aggresive rate : " + aggressivePercentage);
        Debug.Log( "playerGunDam : "+playerGunDamage );
        Debug.Log("EnemyGunDam : " + enemyGunDamage);
        Debug.Log("healthDecFactor : " + healthDecFactor);
        Debug.Log("Ammo : " + Ammo);
        Debug.Log("TotalEnemyCount : " + TotalEnemyCount);
    }

    public static void send_data(string message)
    {
        //---data to send to the server---
        string textToSend = message;
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
        nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        receive_data_test();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private static void Server_Load()
    {
        client = new TcpClient(SERVER_IP, PORT_NO);
        nwStream = client.GetStream();

        //---data to send to the server---
        string textToSend = DateTime.Now.ToString();
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

        //---send the text---
        Console.WriteLine("Sending : " + textToSend);
        nwStream.Write(bytesToSend, 0, bytesToSend.Length);
    }

    IEnumerator waitSeconds()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}

