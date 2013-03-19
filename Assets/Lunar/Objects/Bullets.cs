using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Bullets : CollidableCollection
{
	public Transform bulletPrefab;

	protected override void HandleRequest (C2Request request){
		string type = request.Type;
		if(type.Equals("SpawnBullet")) {
			Vector3 spawnPos = new Vector3((float)request.getData("x"), (float)request.getData("y"), (float)request.getData("z"));
			SpawnCollidable(bulletPrefab, spawnPos);
		}
	}
	
}
