using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine.UI;

public class SocketClientConsole : MonoBehaviour {

	private StringBuilder content1;
	private Socket curSocket;
	private IPEndPoint serverIP;
	private Vector2 scrollVec2;
	public InputField input;

	// Use this for initialization
	void Start ()
	{
		content1 = new StringBuilder();
		IPAddress ip = IPAddress.Parse("127.0.0.1");
		serverIP = new IPEndPoint(ip, 8686);
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("连接TCP"))
		{
			closeCurSocket();
			tcpClient(serverIP);
		}

		if(GUILayout.Button("发送消息"))
		{
			tcpSend(input.text);
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("连接UDP"))
		{
			closeCurSocket();
			udpClient(serverIP);
		}

		if(GUILayout.Button("发送消息"))
		{
			udpSend(input.text, serverIP);
		}
		GUILayout.EndHorizontal();


		if(GUILayout.Button("关闭当前接口"))
		{
			closeCurSocket();
		}

		scrollVec2 = GUILayout.BeginScrollView(scrollVec2);
		GUILayout.Label(content1.ToString());
		GUILayout.EndScrollView();
	}

	private void tcpClient(IPEndPoint _serverIP)
	{
		content1.AppendLine("连接TCP服务端");

		//创建套接字
		Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		curSocket = tcpClient;
		content1.AppendLine("创建套接字成功");

		//连接服务端
		try
		{
			tcpClient.Connect(_serverIP);
			content1.AppendLine("连接服务器成功");
		}
		catch(SocketException e)
		{
			content1.AppendLine(e.StackTrace);
			return;
		}

		new Thread(() => {
			content1.AppendLine("开始接收服务端消息");
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

				content1.AppendLine(string.Format("服务端：{0}", Encoding.UTF8.GetString(data)));
			}
		}).Start();
	}

	private void tcpSend(string msg)
	{
		//发送消息
		curSocket.Send(Encoding.UTF8.GetBytes(msg));
	}

	private void udpClient(IPEndPoint _serverIP)
	{
		content1.AppendLine("连接UDP服务器");
		//创建套接字
		Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		curSocket = udpClient;

		//为什么这里要重新创建一个ip
		IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
		EndPoint remote = ipep as EndPoint;

		//接收消息
		new Thread(() => {
			while(true)
			{
				content1.AppendLine("开始接收UDP服务端消息");
				byte[] data = new byte[1024];
				try
				{
					int length = udpClient.ReceiveFrom(data, ref remote);
				}
				catch(System.Exception e)
				{
					content1.AppendLine(e.StackTrace);
					break;
				}

				content1.AppendLine(string.Format("服务端：{0}", Encoding.UTF8.GetString(data)));
			}
		}).Start();
	}

	private void udpSend(string msg, IPEndPoint _serverIP)
	{
		//发送消息
		curSocket.SendTo(Encoding.UTF8.GetBytes(msg), SocketFlags.None, _serverIP);
	}

	private void closeCurSocket()
	{
		if(curSocket != null && curSocket.Connected)
		{
			curSocket.Close();
			curSocket = null;
			content1.AppendLine("成功关闭当前socket");
		}
	}
}
