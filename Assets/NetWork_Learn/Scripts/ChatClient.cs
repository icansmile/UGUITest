using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using System.Threading;  
using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;

//https://www.one-tab.com/page/G0Va_dzET_KR3JuTh_6T6Q
public class ChatClient : MonoBehaviour {
   public string ipaddress = "127.0.0.1";  
    public int  port = 7799;  
    private Socket clientSocket;  
    public InputField MessageInput;  
    public Text MessageText;  
    private Thread thread;  
    private byte[] data=new byte[1024];// 数据容器  
    private string message = "";  

    void Start () {  
		IPAddress.Parse("127.0.0.1");
    	ConnectToServer();
		var btn = GameObject.Find("Button").GetComponent<Button>();
		btn.onClick.AddListener(OnSendButtonClick);
    }  
      
    void Update () {  
        //只有在主线程才能更新UI  
      if (message!="" && message!=null)  
       {  
            MessageText.text +="\n"+ message;  
            message = "";  
        }  
    }  
	public ChatClient(Socket s)  
	{  
		clientSocket = s;  
	}  
    /** 
     * 连接服务器端函数 
     * */  
    public void ConnectToServer()  
    {  
        clientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);  
        //跟服务器连接  
		var ip = IPAddress.Parse("192.168.16.205");
        clientSocket.Connect(new IPEndPoint(ip,port));  
        //客户端开启线程接收数据  
        thread = new Thread(ReceiveMessage);  
        thread.Start();  
    }  
  
    void ReceiveMessage()  
    {  
        while (true)  
        {  
            if (clientSocket.Connected == false)  
            {  
                break;  
            }  
        int length=clientSocket.Receive(data);  
        message = Encoding.UTF8.GetString(data,0,length);  
         print(message);  
        }  
       
    }  

	public bool Connected()  
	{  
		return clientSocket.Connected;  
	}  
  
    void SendMessage(string message)  
    {  
        byte[] data=Encoding.UTF8.GetBytes(message);  
        clientSocket.Send(data);  
    }  
  
    public void OnSendButtonClick()  
    {  
        string value = MessageInput.text;  
        SendMessage(value);  
        MessageInput.text = " ";  
    }  
    /** 
     * unity自带方法 
     * 停止运行时会执行 
     * */  
     void OnDestroy()  
    {  
        //关闭连接，分接收功能和发送功能，both为两者均关闭  
        clientSocket.Shutdown(SocketShutdown.Both);  
       clientSocket.Close();  
    }  
}
