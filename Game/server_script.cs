using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class server_script : MonoBehaviour
{
    const int PORT_NO = 1234;
    const string SERVER_IP = "127.0.0.1";
    public static NetworkStream nwStream;
    public static TcpClient client;
    //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

    // Start is called before the first frame update
    void Start()
    {
        Server_Load();
        //Option1_ExecProcess();
        UnityEngine.Debug.Log("Server code running !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        //---read back the text---
        //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
        //Debug.Log("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
        
    }

    public static void receive_data()
    {
        //---read back the text---
        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
        Debug.Log("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));

    }

    public static void send_data()
    {
        //---data to send to the server---
        string textToSend = DateTime.Now.ToString();
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
    }

    // Update is called once per frame
    void Update()
    {
        receive_data();
        System.Threading.Thread.Sleep(1000);
        send_data();
    }
    private void Server_Load()
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
}

///////////////////////
//using IronPython.Hosting;
/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RunPythonScriptFromCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Execute python process...");
            Option1_ExecProcess();

            Console.WriteLine();

            //Console.WriteLine("Execute python IronPython...");
            //Option2_IronPython();

            Console.ReadKey();
        }

        static void Option1_ExecProcess()
        {
            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\PythonInstall\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\AllTech\Code\DaysBetweenDates.py";
            var start = "2019-1-1";
            var end = "2019-1-22";

            psi.Arguments = $"\"{script}\" \"{start}\" \"{end}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(results);

        }
        
        static void Option2_IronPython()
        {
            // 1) Create engine
            var engine = Python.CreateEngine();

            // 2) Provide script and arguments
            var script = @"C:\AllTech\Code\DaysBetweenDates.py";
            var source = engine.CreateScriptSourceFromFile(script);

            var argv = new List<string>();
            argv.Add("");
            argv.Add("2019-1-1");
            argv.Add("2019-1-22");

            engine.GetSysModule().SetVariable("argv", argv);

            // 3) Output redirect
            var eIO = engine.Runtime.IO;

            var errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            var results = new MemoryStream();
            eIO.SetOutput(results, Encoding.Default);

            // 4) Execute script
            var scope = engine.CreateScope();
            source.Execute(scope);

            // 5) Display output
            string str(byte[] x) => Encoding.Default.GetString(x);

            Console.WriteLine("ERRORS:");
            Console.WriteLine(str(errors.ToArray()));
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(str(results.ToArray()));

        }
        
    }
}
*/

