using UnityEngine;  
using System.Collections;  
using Net;  
using System.Threading;
  
public class TestSocket : MonoBehaviour {  
    private Thread thread;
    private ClientSocket clientSocket;
  
    // Use this for initialization  
    void Start () {  
        
        // ThreadPool.QueueUserWorkItem(obj => {
        //     ClientSocket mSocket = new ClientSocket();  
        //     mSocket.ConnectServer("127.0.0.1", 8088);  
        //     mSocket.SendMessage("服务器傻逼！");  
        // });
        thread = new Thread(() => {
            clientSocket = new ClientSocket();  
            clientSocket.ConnectServer("127.0.0.1", 8088);  
            clientSocket.SendMessage("服务器傻逼！");  
        });
        thread.Start();
    }  
      
    // Update is called once per frame  
    void Update () {  
      
    }  

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        if(GUILayout.Button("发送"))
        {
            clientSocket.SendMessage("send test");
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        thread.Abort();
    }
}  
