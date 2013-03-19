using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Obstacles : CollidableCollection
{
	public Transform stonePrefab;

	protected override void HandleRequest (C2Request request){
		string type = request.Type;
		if(type.Equals(C2MessageType.Request_SpawnStone)) {

			Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
			SpawnCollidable(stonePrefab, spawnPos);
		}
	}
	
}