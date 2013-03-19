using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Coins : CollidableCollection
{
	public Transform coinPrefab;

	protected override void HandleRequest (C2Request request){
		string type = request.Type;
		if (Network.peerType == NetworkPeerType.Server){
			if(type.Equals("SpawnCoin")) {
				Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
				SpawnCollidable(coinPrefab, spawnPos);
			}
		}
	}
	
}

