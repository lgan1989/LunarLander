using UnityEngine;
using System.Collections;

public class  ClientNetworkGUI: C2Component
{

	// Use this for GUI
	
	private string connectToIP = "127.0.0.1";
	private int connectPort = 25001;     
	void OnGUI ()
	{
		
		if (Network.peerType == NetworkPeerType.Disconnected){
			//We are currently disconnected: Not a client or host
			GUILayout.Label("Connection status: Disconnected");
			connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
			connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
			GUILayout.BeginVertical();    
			if (GUILayout.Button ("Connect To Server"))
			{
				C2Request request = new C2Request(C2MessageType.Request_ConnectToServer);
				request.putData("IP" , connectToIP);
				request.putData("Port" , connectPort);
				this.SendRequest(request);

			}
			GUILayout.EndVertical();
			
			
		}else{
			//We've got a connection(s)!	
	
			if (Network.peerType == NetworkPeerType.Connecting){
			
				GUILayout.Label("Connection status: Connecting");
				
			} else if (Network.peerType == NetworkPeerType.Client){
				
				GUILayout.Label("Connection status: Client!");
				GUILayout.Label("Ping to server: "+Network.GetAveragePing(  Network.connections[0] ) );				
			}
	
			if (GUILayout.Button ("Disconnect"))
			{
				C2Request request = new C2Request(C2MessageType.Request_DisconnectFromServer);
				this.SendRequest(request);				
			}
			
		}
		/*if (gameState == LunarLanderGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }*/
        //We display the game GUI from the playerscript
        //It would be nicer to have a seperate script dedicated to the GUI though...
		
	}
	
}

