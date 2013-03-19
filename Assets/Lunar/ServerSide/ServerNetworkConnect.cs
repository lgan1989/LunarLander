using UnityEngine;
using System.Collections;


public class ServerNetworkConnect:C2Component
{
	public ServerNetworkConnect ()
	{
		this.RegisterAcceptedReq("CloseServer");
		this.RegisterAcceptedReq("StartServer");
	}
	private int connectPort   = 25001;
	
	
	//Obviously the GUI is for both client&servers (mixed!)
	
	protected override void HandleRequest (C2Request request)
	{
		string type = request.Type;

		if(type.Equals("StartServer")) {
			Network.InitializeServer(32, connectPort , false);
		} else if(type.Equals("CloseServer")) {
			Network.Disconnect(200);
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

