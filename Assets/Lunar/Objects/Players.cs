using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Players : CollidableCollection 
{
	public Transform playerPrefab;
    public Transform distributor;

	public List <CurrentPlayer> playerScripts = new List<CurrentPlayer>();
   // public List<ClientDistributor> distributorScripts = new List<ClientDistributor>();
	
	public Dictionary <NetworkPlayer , Player> playerList = new Dictionary <NetworkPlayer , Player>();
	
	private float r;
	
	public Players(){
		RegisterAcceptedReq(C2MessageType.Request_UpdateAlive);
		RegisterAcceptedReq(C2MessageType.Request_UpdateEnergy);
		RegisterAcceptedReq(C2MessageType.Request_UpdateCoin);
		RegisterAcceptedReq(C2MessageType.Request_UpdateSpeed);
		RegisterAcceptedReq(C2MessageType.Request_UpdateShield);
		RegisterAcceptedReq(C2MessageType.Request_UpdateEnergy);
		
		RegisterAcceptedReq(C2MessageType.Request_PlayerStatus);
	}

	void OnServerInitialized(){
		//Spawn a player for the server itself
		//Spawnplayer(Network.player);
	}
	
	void OnPlayerConnected(NetworkPlayer newPlayer) {
		//A player connected to me(the server)!
		Spawnplayer(newPlayer);
	}	
	
	void Awake(){
		Transform sphere = GameObject.Find("Sphere").transform;
		r = sphere.lossyScale.y;
	}
	
	
		
	void Spawnplayer(NetworkPlayer newPlayer){
		//Called on the server only
		
		int playerNumber = int.Parse(newPlayer+"");
		//Instantiate a new object for this player, remember; the server is therefore the owner.
		float polar = Random.Range(0f, 2 * Mathf.PI);
		float elevation = Random.Range(0f, 2 * Mathf.PI);
		Vector3 spawnPos;
		
		Math.SphericalToCartesian(r/2.0f - 10, polar, elevation,out spawnPos);
		
     //   Transform myNewDistributor = (Transform)Network.Instantiate(distributor, transform.position, transform.rotation, playerNumber);
	//	NetworkView newDistributorNetworkview = myNewDistributor.networkView;
	//	distributorScripts.Add((Distributor)myNewDistributor.GetComponent(typeof(Distributor)));	
	//	newDistributorNetworkview.RPC("SetPlayer", newPlayer, newPlayer);//Set it on the owner
		//Network.SetSendingEnabled(newPlayer , 1 , false);
		
		
		
		Transform myNewTrans = (Transform)Network.Instantiate(playerPrefab, spawnPos, transform.rotation, 1);
		//Get the networkview of this new transform
		playerList.Add(newPlayer , new Player(newPlayer));
			
		NetworkView newObjectsNetworkview = myNewTrans.networkView;
		
		//Keep track of this new player so we can properly destroy it when required.
		playerScripts.Add((CurrentPlayer)myNewTrans.GetComponent(typeof(CurrentPlayer)));
      
		//Call an RPC on this new networkview, set the player who controls this player
		newObjectsNetworkview.RPC("SetPlayer", RPCMode.AllBuffered, newPlayer);//Set it on the owner
		
		//((Spawner)GameObject.Find("Spawner").GetComponent(typeof(Spawner))).enabled = true;
 
	}

	protected override void HandleRequest (C2Request request){
		
		string type = request.Type;
		NetworkPlayer player = (NetworkPlayer)request.getData("NetworkPlayer");

		switch (type){
		case C2MessageType.Request_UpdateCoin:
			playerList[player].updatePlayerCoin();
			break;
		case C2MessageType.Request_UpdateEnergy:
			playerList[player].updatePlayerEnergy();
			break;
		case C2MessageType.Request_UpdateSpeed:
			playerList[player].updatePlayerSpeed();
			break;
		case C2MessageType.Request_UpdateShield:
			playerList[player].updatePlayerShield();
			break;
		case C2MessageType.Request_UpdateAlive:
			playerList[player].updatePlayerAlive();
			break;
		case C2MessageType.Request_PlayerStatus:
		
			C2Notification notification = new C2Notification(C2MessageType.Notification_PlayerStatus);
			notification.putData("NetworkPlayer" , player);
			notification.putData("Player" , playerList[player]);
			this.SendNotification(notification);
			break;
			default:
				break;
		}
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		//Debug.Log("Clean up after player " + player);
	
		foreach(CurrentPlayer script in playerScripts){
			if(player == script.owner){//We found the players object
				Network.RemoveRPCs(script.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
				Network.Destroy(script.gameObject.networkView.viewID);//Destroying the GO will destroy everything
				playerList.Remove(script.owner);
				playerScripts.Remove(script);//Remove this player from the list
				
				break;
			}
		}
        	
		//Remove the buffered RPC call for instantiate for this player.
		int playerNumber = int.Parse(player+"");
		Network.RemoveRPCs(player , 1);
		//Debug.Log(playerNumber);
		
		
		// The next destroys will not destroy anything since the players never
		// instantiated anything nor buffered RPCs
		//Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);

	}   

}

