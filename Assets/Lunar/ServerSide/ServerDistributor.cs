using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class ServerDistributor : C2DistributedConnector
{
		
	public NetworkPlayer owner;
	
	public ServerDistributor(){
		RegisterAcceptedReq(C2MessageType.Request_Input);
		RegisterAcceptedReq(C2MessageType.Request_PlayerStatus);
		
		
		RegisterAcceptedNoti(C2MessageType.Notification_CoinPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_EnergyBlockPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_AcceleratorPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_ShieldPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_EndGame);
		
		RegisterAcceptedNoti(C2MessageType.Notification_PlayerStatus);
		
	}

	protected override void HandleRequest (C2Request request)
	{
		foreach(C2Port topPort in this.tops) 
		{
			topPort.PassRequest(request);
		}	
	}
	
	protected override void HandleNotification (C2Notification notification)
	{
		foreach(C2Port bottomPort in this.bottoms) 
		{
			bottomPort.PassNotification(notification);
		}	
	
		string type = notification.Type;
		NetworkPlayer player = (NetworkPlayer)notification.getData("NetworkPlayer");
		switch (type)
		{
		case C2MessageType.Notification_CoinPicked: 
		case C2MessageType.Notification_EnergyBlockPicked:
		case C2MessageType.Notification_AcceleratorPicked:
		case C2MessageType.Notification_ShieldPicked:
			networkView.RPC ("ItemPicked" , player , notification.Type , player);
		    break;	   
		case C2MessageType.Notification_PlayerStatus:
			Player playerStauts = (Player)notification.getData("Player");
			networkView.RPC("GetPlayerStatus" , 
								player,
								player, 
								playerStauts.Score,
								playerStauts.Distance,
								playerStauts.Coin,
								playerStauts.Energy,
								playerStauts.Speed,
								playerStauts.Shield,
								playerStauts.Alive);
			break;
		case C2MessageType.Notification_EndGame:
			networkView.RPC ("EndGame" , player);
			break;
		    default:
		        break;
		}		

	}
	
	[RPC]
	void SetPlayer(NetworkPlayer player){
		owner = player;
		if(Network.isClient && player==Network.player){
			LunarLanderClient clientArchitecture = ((LunarLanderClient)GameObject.Find("LunarLanderGame").GetComponent(typeof(LunarLanderClient)));
			clientArchitecture.clientDistributor = this;
		}
	}
	
	[RPC]
    public void SendClientAction(float VInput, float HInput , NetworkPlayer player)
    {
        C2Request request = new C2Request(C2MessageType.Request_ClientAction);
        request.putData("VInput", VInput);
        request.putData("HInput", HInput);
		request.putData("NetworkPlayer" , player);
		
        this.HandleRequest(request);

    }


	[RPC]
	public void RequestPlayerStatus(NetworkPlayer player){ //request for lasted data

		C2Request request = new C2Request(C2MessageType.Request_PlayerStatus);
		request.putData("NetworkPlayer" , player);
		this.HandleRequest(request);
		
	}
	[RPC]
	public void GetPlayerStatus(NetworkPlayer player , int score , float distance , int coin , float energy , float speed , float shield,  bool alive){
		if (Network.player == player){
			C2Notification notification = new C2Notification(C2MessageType.Notification_PlayerStatus);
			notification.putData("Player" , new Player(player , score , distance , coin , energy , speed , shield , alive));
			this.HandleNotification(notification);
		}
	}
	
	[RPC]
	public void EndGame(){
		if (Network.isClient){
			
			C2Notification notification = new C2Notification(C2MessageType.Notification_EndGame);
			this.HandleNotification(notification);
			
		}
	}
   
	[RPC]
	public void ItemPicked(string type , NetworkPlayer player){

		if (player == Network.player){
			C2Notification notification = new C2Notification(type);
			notification.putData("NetworkPlayer" , player);
			this.HandleNotification(notification);
		}
	}


}

