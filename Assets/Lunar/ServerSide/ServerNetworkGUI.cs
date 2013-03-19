using UnityEngine;
using System.Collections;

public class  ServerNetworkGUI: C2Component
{

	// Use this for GUI
	void OnGUI ()
	{
		
		if (Network.peerType == NetworkPeerType.Disconnected){
			//We are currently disconnected: Not a client or host
			GUILayout.Label("Connection status: Disconnected");
			
			GUILayout.BeginVertical();    
			if (GUILayout.Button ("Start Server"))
			{
				//Start a server for 32 clients using the "connectPort" given via the GUI
				//Ignore the nat for now	
				C2Request request = new C2Request("StartServer");
				this.SendRequest(request);
				//Network.InitializeServer(32, connectPort , false);
			}
			GUILayout.EndVertical();
			
			
		}else{
			//We've got a connection(s)!

			if (Network.peerType == NetworkPeerType.Connecting){
			
				GUILayout.Label("Connection status: Connecting");
				
			} else if (Network.peerType == NetworkPeerType.Server){
				
				GUILayout.Label("Connection status: Server!");
				GUILayout.Label("Connections: "+Network.connections.Length);
				if(Network.connections.Length>=1){
					GUILayout.Label("Ping to first player: "+Network.GetAveragePing(  Network.connections[0] ) );
				}			
			}
	
			if (GUILayout.Button ("Close Server"))
			{
				C2Request request = new C2Request("CloseServer");
				this.SendRequest(request);				
				//Network.Disconnect(200);
			}
			
		}
		
	}
	
}

