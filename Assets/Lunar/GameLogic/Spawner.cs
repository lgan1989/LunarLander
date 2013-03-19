using UnityEngine;
using System.Collections;


public class Spawner : C2Component {

    private float nextCoinTime = 0.0f;
	private float nextEnergyBlockTime = 0.0f;
	private float nextAcceleratorTime = 0.0f;
	private float nextShieldTime = 0.0f;
	
	
    private float spawnCoinRate = 2.0f;
	private float spawnEnergyBlockRate = 3.0f;
	private float spawnAcceleratorRate = 3.0f;
	private float spawnShieldRate = 3.0f;
	
	
	
	private ArrayList coins = new ArrayList();
	private float r;
 	
	void Awake(){
		
		Transform sphere = GameObject.Find("Sphere").transform;
		r = sphere.lossyScale.y;
		
	}
	void Update () {
		float polar = Random.Range(0f, 2 * Mathf.PI);
		float elevation = Random.Range(0f, 2 * Mathf.PI);
		Vector3 spawnPos;
        if (nextCoinTime < Time.time)
        {

			Math.SphericalToCartesian(r/2.0f + 20, polar, elevation,out spawnPos);
            RequestSpawnItem(spawnPos , "Coin");
            nextCoinTime = Time.time + spawnCoinRate;
			
			polar = Random.Range(0f, 2 * Mathf.PI);
			elevation = Random.Range(0f, 2 * Mathf.PI);
			Math.SphericalToCartesian(r/2.0f + 15, polar, elevation ,out spawnPos);
            RequestSpawnItem(spawnPos , "Stone");
		}
		if (nextEnergyBlockTime < Time.time){
			
			polar = Random.Range(0f, 2 * Mathf.PI);
			elevation = Random.Range(0f, 2 * Mathf.PI);
			Math.SphericalToCartesian(r/2.0f + 20, polar, elevation,out spawnPos);
			RequestSpawnItem(spawnPos , "EnergyBlock");
			nextEnergyBlockTime += spawnEnergyBlockRate;
		}
		if (nextAcceleratorTime < Time.time){
				
        	polar = Random.Range(0f, 2 * Mathf.PI);
			elevation = Random.Range(0f, 2 * Mathf.PI);
			Math.SphericalToCartesian(r/2.0f + 20, polar, elevation,out spawnPos);
			RequestSpawnItem(spawnPos , "Accelerator");
			nextAcceleratorTime += spawnAcceleratorRate;
		}
		if (nextShieldTime < Time.time){

        	polar = Random.Range(0f, 2 * Mathf.PI);
			elevation = Random.Range(0f, 2 * Mathf.PI);
			Math.SphericalToCartesian(r/2.0f + 20, polar, elevation,out spawnPos);
			RequestSpawnItem(spawnPos , "Shield");
			nextShieldTime += spawnShieldRate;
		}
		
		
	}

   
  void RequestSpawnItem(Vector3 spawnPos , string item) {	

		C2Request request= new C2Request("Spawn" + item);
		request.putData("x",spawnPos.x);
		request.putData("y",spawnPos.y);
		request.putData("z",spawnPos.z);
		this.SendRequest(request);
    }	
	
}
