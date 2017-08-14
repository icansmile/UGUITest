using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketServerAsyncConsole : MonoBehaviour {

	private StringBuilder content;
	private Socket curTCPClient;

	// Use this for initialization
	void Start () 
	{
		content = new StringBuilder();
	}

	private void startTCPListen()
	{
		IPAddress serverIP = IPAddress.Parse("127.0.0.1");
		IPEndPoint serverIpep = new IPEndPoint(serverIP, 8686);

		Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		tcpServer.Bind(serverIpep);
		content.AppendLine("绑定端口成功");

		tcpServer.Listen(100);

		content.AppendLine("异步监听开启");

		asyncTCPAccept(tcpServer);
	}

	private void asyncTCPAccept(Socket tcpServer)
	{
		tcpServer.BeginAccept(asyncResult => {
			Socket tcpClient = tcpServer.EndAccept(asyncResult);
			curTCPClient = tcpClient;

			content.AppendLine("监听到客户端连接");
			asyncTCPReceive(tcpClient);

		}, null);
	}

	private void asyncTCPReceive(Socket tcpClient)
	{
		byte[] data = new byte[1024];
		try
		{
			tcpClient.BeginReceive(data, 0, data.Length, SocketFlags.None, asyncResult => {
				int length = tcpClient.EndReceive(asyncResult);
				content.AppendLine("客户端: " + Encoding.UTF8.GetString(data));

				asyncTCPSend(tcpClient, "服务端: 收到消息 = " + Encoding.UTF8.GetString(data));

				asyncTCPReceive(tcpClient);

			}, null);
		}
		catch(System.Exception e)
		{
			content.AppendLine(e.StackTrace);
		}
	}

	private void asyncTCPSend(Socket tcpClient, string msg)
	{
		byte[] data = Encoding.UTF8.GetBytes(msg);
		try
		{
			tcpClient.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult => {
				int length = tcpClient.EndSend(asyncResult);
			}, null);
		}
		catch(System.Exception e)
		{
			content.AppendLine(e.StackTrace);
		}
	}



	public class UDPState
	{
		//服务端
		public Socket udpServer = null;
		//接收数据缓冲区
		public byte[] buffer = new byte[1024];
		//远程终端	
		public EndPoint remoteEP;
	}

	private UDPState udpState;

	private void udpServerBind()
	{
		IPAddress ip = IPAddress.Parse("127.0.0.1");
		IPEndPoint Ipep = new IPEndPoint(ip, 8686);

		Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

		udpServer.Bind(Ipep);

		IPEndPoint clientIP = new IPEndPoint(IPAddress.Any, 0);

		udpState = new UDPState();
		udpState.udpServer = udpServer;
		udpState.remoteEP = clientIP;

		asyncUDPReceive();
	}

	private void asyncUDPReceive()
	{
		udpState.udpServer.BeginReceiveFrom(udpState.buffer, 0, udpState.buffer.Length, SocketFlags.None, ref udpState.remoteEP,
		asyncResult => {
			IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
			EndPoint remoteEP = (EndPoint)ipep;

			udpState.udpServer.EndReceiveFrom(asyncResult, ref remoteEP);

			content.AppendLine("客户端: " + Encoding.UTF8.GetString(udpState.buffer));

			udpState.remoteEP = remoteEP;

			asyncUDPSend("收到消息 " + Encoding.UTF8.GetString(udpState.buffer));

			//继续接收消息
			asyncUDPReceive();
		}, null);
	}

	private void asyncUDPSend(string msg)
	{
		byte[] data =  Encoding.UTF8.GetBytes(msg);
		udpState.udpServer.BeginSendTo(data, 0, data.Length, SocketFlags.None, udpState.remoteEP, asyncResult => {
			udpState.udpServer.EndSendTo(asyncResult);
		}, null);
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		if(GUILayout.Button("创建TCP服务"))
		{
			startTCPListen();
		}

		if(GUILayout.Button("创建UDP服务"))
		{
			udpServerBind();
		}

		if(GUILayout.Button("连续发送"))
		{
			asyncTCPSend(curTCPClient, "我");
			asyncTCPSend(curTCPClient, "我");
		}

		GUILayout.Label(content.ToString());
	}
}
