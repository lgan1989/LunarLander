using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Items : CollidableCollection
{
	public Transform acceleratorPrefab;
	public Transform shieldPrefab;
	public Transform energyBlockPrefab;
	

	protected override void HandleRequest (C2Request request){
		string type = request.Type;
		if(type.Equals(C2MessageType.Request_SpawnAccelerator)) {
			Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
			SpawnCollidable(acceleratorPrefab, spawnPos);
		}
		if(type.Equals(C2MessageType.Request_SpawnEnergyBlock)) {
			Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
			SpawnCollidable(energyBlockPrefab, spawnPos);
		}
		if(type.Equals(C2MessageType.Request_SpawnShield)) {
			Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
			SpawnCollidable(shieldPrefab, spawnPos);
		}
	}
	
}

