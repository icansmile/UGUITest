using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketClientAsyncConsole : MonoBehaviour {

	private StringBuilder content;
	public InputField input;
	private Socket curTCPClient;

	// Use this for initialization
	void Start () 
	{
		content = new StringBuilder();
	}
	
	private void asyncTCPConnect()
	{
		IPAddress ip = IPAddress.Parse("127.0.0.1");
		IPEndPoint Ipep = new IPEndPoint(ip, 8686);

		Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		curTCPClient = tcpClient;

		tcpClient.BeginConnect(Ipep, asyncResult => {
			tcpClient.EndConnect(asyncResult);

			content.AppendLine("连接TCP服务器成功");

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

				content.AppendLine(Encoding.UTF8.GetString(data));

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
		public Socket udpClient = null;
		public byte[] buffer = new byte[1024];
		//服务端节点
		public IPEndPoint serverIP;
		//远程端节点
		public EndPoint remoteEP;
	}

	private UDPState udpState;

	private void initUDPClient()
	{
		udpState = new UDPState();
		udpState.udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		udpState.serverIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8686);
		udpState.remoteEP = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));

		asyncUDPSend("我来了");
		asyncUDPReceive();
	}

	private void asyncUDPReceive()
	{
		udpState.udpClient.BeginReceiveFrom(udpState.buffer, 0, udpState.buffer.Length, SocketFlags.None, ref udpState.remoteEP,
		asyncReuslt => {
			udpState.udpClient.EndReceiveFrom(asyncReuslt, ref udpState.remoteEP);
			content.AppendLine(Encoding.UTF8.GetString(udpState.buffer));
			asyncUDPReceive();
		}, null);
	}

	private void asyncUDPSend(string msg)
	{
		var buffer = Encoding.UTF8.GetBytes(msg);
		udpState.udpClient.BeginSendTo(buffer, 0, buffer.Length, SocketFlags.None, udpState.serverIP,
		asyncResult => {
			udpState.udpClient.EndSendTo(asyncResult);
		}, null);
	
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("连接TCP服务端"))
		{
			asyncTCPConnect();
		}
		
		if(GUILayout.Button("发送消息"))
		{
			asyncTCPSend(curTCPClient, input.text);
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("连接UDP服务端"))
		{
			initUDPClient();
		}
		
		if(GUILayout.Button("发送消息"))
		{
			asyncUDPSend(input.text);
		}
		GUILayout.EndHorizontal();

		GUILayout.Label(content.ToString());
	}
}
