using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollidableCollection : C2Component
{
	protected List<Collidable> collidables = new List<Collidable>();

	
	protected void SpawnCollidable(Transform prefab, Vector3 spawnPos) {
		if (Network.peerType == NetworkPeerType.Server){
			Transform t =  Network.Instantiate(prefab, spawnPos, Quaternion.identity, 0) as Transform;
			this.collidables.Add((Collidable)t.GetComponent(typeof(Collidable)));
		}
	}
	
	public int lastCount = 0;
	
	void Update () 
	{
		if (Network.peerType == NetworkPeerType.Client){
			enabled = false;
			return;
		}
		if (Network.peerType == NetworkPeerType.Server){
			for(int i = 0 ; i < collidables.Count;) {
				if (collidables[i] != null){
					if(collidables[i].Collided) {
						
						NetworkPlayer player = collidables[i].ColliderNetworkPlayer;
						Type itemType = collidables[i].GetType();
						collidables[i].DestroySelf();
	
						switch (itemType.ToString()){
						case "Coin":
						case "Accelerator":
						case "EnergyBlock":
						case "Shield":
							this.NotifyItemCollided(player , itemType);
							break;
						case "Obstacle":
							this.NotifyObstacleCollided(player , itemType);
							break;
						default:
							break;
						}
						
						collidables.RemoveAt(i);
					}
					else i++;
				}
				else i++;
			}
		}
		
	}
					
	protected void NotifyItemCollided(NetworkPlayer player , Type itemType) {
		C2Notification notification = new C2Notification("ItemCollided");
		notification.putData("NetworkPlayer" , player);
		notification.MsgSrcType = itemType;
		this.SendNotification(notification);
	}
	protected void NotifyObstacleCollided(NetworkPlayer player , Type itemType) {
		C2Notification notification = new C2Notification("ObstacleCollided");
		notification.putData("NetworkPlayer" , player);
		notification.MsgSrcType = itemType;
		this.SendNotification(notification);
	}
}

