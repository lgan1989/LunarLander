using UnityEngine;
using System.Collections;


public class ClientNetworkConnect:C2Component
{
	public ClientNetworkConnect ()
	{
		this.RegisterAcceptedReq(C2MessageType.Request_ConnectToServer);
		this.RegisterAcceptedReq(C2MessageType.Request_DisconnectFromServer);
	}

	//Obviously the GUI is for both client&servers (mixed!)
	
	protected override void HandleRequest (C2Request request)
	{
		string type = request.Type;
	
		if(type.Equals(C2MessageType.Request_ConnectToServer)) {
			string ConnectToIP = (string)request.getData("IP");
			int ConnectPort = (int)request.getData("Port");
			Network.Connect(ConnectToIP , ConnectPort);
			Application.LoadLevel(Application.loadedLevel);
		} else if(type.Equals(C2MessageType.Request_DisconnectFromServer)) {
			Network.Disconnect(200);
			Application.LoadLevel(Application.loadedLevel);
		} 

	}
	
	// NONE of the functions below is of any use in this demo, the code below is only used for demonstration.
	// First ensure you understand the code in the OnGUI() function above.
	
	//Client functions called by Unity
	void OnConnectedToServer() {
		Debug.Log("This CLIENT has connected to a server");	
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log("Could not connect to server: "+ error);
	}
	
	
	//Server functions called by Unity
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}

	                                                       
}

