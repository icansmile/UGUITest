using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class SocketServerConsole : MonoBehaviour {

	private IPEndPoint serverIP;
	private StringBuilder content1;
	private Socket serverSocket;
	private Socket curClientSocket;
	private Vector2 scrollVec2;

	void Start () 
	{
		content1 = new StringBuilder();
		//获取IP地址
		IPAddress ip = IPAddress.Parse("127.0.0.1");
		//设置主机端口
		serverIP = new IPEndPoint(ip, 8686);
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		if(GUILayout.Button("创建TCP服务"))
		{
			closeCurSocket();
			buildTcpServer(serverIP);
		}

		if(GUILayout.Button("创建UDP服务"))
		{
			closeCurSocket();
			buildUdpServer(serverIP);
		}

		if(GUILayout.Button("关闭当前接口"))
		{
			closeCurSocket();
		}

		scrollVec2 = GUILayout.BeginScrollView(scrollVec2);
		GUILayout.Label(content1.ToString());
		GUILayout.EndScrollView();
	}

	private void buildTcpServer(EndPoint _serverIP)
	{
		content1.AppendLine("建立TCP服务");

		//建立socket
		Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		serverSocket = tcpServer;
		//绑定地址和端口
		tcpServer.Bind(_serverIP);
		content1.AppendLine("绑定地址和端口成功");
		//开启监听
		tcpServer.Listen(100);
		content1.AppendLine("开始监听");

		//开启子线程，持续监听
		new Thread(() => {
			while(true)
			{
				try
				{
					//接收客户端连接
					Socket _clientSocket = tcpServer.Accept();
					curClientSocket = _clientSocket;
					content1.AppendLine("监听到客户端连接, 开始接收消息");

					tcpReceive(_clientSocket);
				}
				catch(System.Exception e)
				{
					content1.AppendLine(e.StackTrace);
				}
			}
		}).Start();

		//关闭
		// tcpServer.Close();
	}

	private void tcpReceive(Socket tcpClient)
	{
		//开启子线程，持续监听
		new Thread(() => {
			while(true)
			{
				byte[] data = new byte[1024];
				try
				{
					int length = tcpClient.Receive(data);
				}
				catch(System.Exception e)
				{
					content1.AppendLine(e.StackTrace);
					break;
				}

				content1.AppendLine(string.Format("客户端：{0}", System.Text.Encoding.UTF8.GetString(data)));

				string sendMsg = "服务器：收到你的消息了！= " + System.Text.Encoding.UTF8.GetString(data);
				tcpSend(sendMsg, tcpClient);
			}
		}).Start();
	}

	private void tcpSend(string msg, Socket clientSocket)
	{
		clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(msg));
	}

	private void buildUdpServer(EndPoint _serverIP)
	{
		content1.AppendLine("建立UDP服务");

		//创建UDP Socket。 SocketType.Dgram, ProtocolType.Udp
		Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		serverSocket = udpServer;
		//绑定主机地址和端口号
		udpServer.Bind(_serverIP);
		content1.AppendLine("绑定地址和端口号成功");

		IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
		EndPoint remote = ipep as EndPoint;

		new Thread(() => {
			while(true)
			{
				byte[] data = new byte[1024];
				try
				{
					int length = udpServer.ReceiveFrom(data, ref remote);
					content1.AppendLine("接收到客户端发来的消息");
				}
				catch(System.Exception e)
				{
					content1.AppendLine(e.StackTrace);
				}

				content1.AppendLine(string .Format("客户端：{0}", System.Text.Encoding.UTF8.GetString(data)));
				//发送消息给客户端
				string sendMsg = "服务端收到UDP消息! ";
				udpSend(sendMsg, remote);
			}
		}).Start();

		//关闭端口
		// udpServer.Close();
	}

	private void udpSend(string msg, EndPoint remote)
	{
		serverSocket.SendTo(Encoding.UTF8.GetBytes(msg), SocketFlags.None, remote);
	}

	private void closeCurSocket()
	{
		if(serverSocket != null && serverSocket.Connected)
		{
			if(curClientSocket != null)
			{
				curClientSocket.Close();
				curClientSocket = null;
			}

			serverSocket.Close();
			serverSocket = null;

			content1.AppendLine("成功关闭当前socket");
		}
	}
}