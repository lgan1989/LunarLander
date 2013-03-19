using UnityEngine;
using System;
using System.Collections;

public class CrashLogic : C2Component
{
	protected override void HandleNotification (C2Notification notification)
	{
		string type = notification.Type;
		if(type.Equals(C2MessageType.Notification_Obstacle_Collided)) {
			Type msgSrcType = notification.MsgSrcType;		
			NetworkPlayer player = (NetworkPlayer)notification.getData("NetworkPlayer");
			Players players = (Players)(GameObject.Find("Players").GetComponent(typeof(Players)));
			if (players.playerList[player].Shield < Mathf.Epsilon){
				this.NotifyCrash(player);
				this.RequestUpdatePlayer(player);
			}
		}
	}
	
	void update(){
		
		Players players = (Players)(GameObject.Find("Players").GetComponent(typeof(Players)));
		foreach (Player player in players.playerList.Values){
			if (player.Speed < Mathf.Epsilon){
				this.NotifyCrash(player.Owner);
				this.RequestUpdatePlayer(player.Owner);			
			}
		}
		
	}
	
	
	private void NotifyCrash(NetworkPlayer player) {
		C2Notification notification = new C2Notification(C2MessageType.Notification_EndGame);
		notification.putData("NetworkPlayer" , player);
		this.SendNotification(notification);
	}
	
	private void RequestUpdatePlayer(NetworkPlayer player){
		
		C2Request request;
		request = new C2Request(C2MessageType.Request_UpdateAlive);
		request.putData("NetworkPlayer" , player);
		this.SendRequest(request);
		
	}
	
}

