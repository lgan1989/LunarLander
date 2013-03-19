using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour
{

	protected bool collided = false;
	protected float lasttime = 0.0f;
	private NetworkPlayer colliderNetworkPlayer ;
	
	

	public NetworkPlayer ColliderNetworkPlayer
	{
		get
		{
			return colliderNetworkPlayer;
		}
	}
	public bool Collided
	{
		get
		{
			return collided;
		}
	}
	
	public float Lasttime
	{
		get
		{
			return lasttime;
		}
		set
		{
			lasttime = value;
		}
	}

	void OnTriggerEnter (Collider other) {
		collided = true;
		if (other.isTrigger ==  false){
			colliderNetworkPlayer = ((CurrentPlayer)other.GetComponent(typeof(CurrentPlayer))).owner;
		}
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Destroy(this.gameObject);

	}
	public void DestroySelf(){
		if (Network.peerType == NetworkPeerType.Server){
			Network.RemoveRPCs(this.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
			Network.Destroy(this.gameObject.networkView.viewID);//Destroying the GO will destroy everything
		}
	}
}


