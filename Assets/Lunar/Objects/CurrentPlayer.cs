using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (AudioSource))]
public class CurrentPlayer : C2Component
{
	public NetworkPlayer owner;
	public Player currentPlayer = new Player();
	public AudioSource ClipCoinPicked;
	public AudioSource ClipEnergyBlockPicked;
	public AudioSource ClipAcceleratorPicked;
	public AudioSource ClipShieldPicked;

    public CurrentPlayer()
    {
        RegisterAcceptedReq(C2MessageType.Request_Movement);
		RegisterAcceptedNoti(C2MessageType.Notification_CoinPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_EnergyBlockPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_AcceleratorPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_ShieldPicked);
		RegisterAcceptedNoti(C2MessageType.Notification_PlayerStatus);
		RegisterAcceptedNoti(C2MessageType.Notification_EndGame);
    }
	
	void Awake(){
		if (Network.isClient){
			enabled=false;	
			
		}
		this.rigidbody.freezeRotation = true;
		
	}
	
	// Use this for initialization
	void Start ()
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentPlayer.Alive == true){
			if (Network.isClient){
				transform.position += transform.forward * Time.deltaTime  * (currentPlayer.Speed)/3.0f;
				currentPlayer.Distance += (currentPlayer.Speed) * Time.deltaTime;
				float temp = Mathf.Log(currentPlayer.Distance , currentPlayer.Speed)/7.0f;
				temp = temp > 0 ? temp : 0;
				if (currentPlayer.Shield - Time.deltaTime > 0){
					currentPlayer.Shield -= Time.deltaTime;
				}
				else{
					currentPlayer.Shield = 0;
					transform.FindChild("Circle").gameObject.SetActive(false);
				}
				if (currentPlayer.Energy > 0){

					if (currentPlayer.Speed + temp < Player.MAX_SPEED){
						currentPlayer.Speed +=  temp;
					}
					currentPlayer.Energy -= 0.8f;
				}
				else{
					currentPlayer.Energy = 0;
					if (currentPlayer.Speed - 0.2f > Mathf.Epsilon){
						currentPlayer.Speed -= 0.2f;
					}
					else currentPlayer.Speed = 0;
					
				}
				currentPlayer.Score += (int)(100 +  50 * (temp) + 25 * currentPlayer.Coin);
				//send request to get the latest data
				C2Request request = new C2Request(C2MessageType.Request_PlayerStatus);
				this.SendRequest(request);
			}
			else if (Network.isServer){
				currentPlayer = ((Players)GameObject.Find("Players").GetComponent(typeof(Players))).playerList[owner];
				transform.position += transform.forward * Time.deltaTime  * (currentPlayer.Speed)/3.0f;
				
				currentPlayer.Distance += (currentPlayer.Speed) * Time.deltaTime;
				//currentPlayer.Shield -= Time.deltaTime;
				float temp = Mathf.Log(currentPlayer.Distance , currentPlayer.Speed)/7.0f;
				temp = temp > 0 ? temp : 0;
				
				if (currentPlayer.Shield - Time.deltaTime > 0){
					currentPlayer.Shield -= Time.deltaTime;
				}
				else{
					currentPlayer.Shield = 0;				
				}
				if (currentPlayer.Energy > 0){
					
					
					if (currentPlayer.Speed + temp < Player.MAX_SPEED){
						currentPlayer.Speed +=  temp;
					}
					currentPlayer.Energy -= 0.8f;
				}
				else{
					currentPlayer.Energy = 0;
					if (currentPlayer.Speed - 0.2f > Mathf.Epsilon){
						currentPlayer.Speed -= 0.2f;
					}
					else currentPlayer.Speed = 0;
					
				}
				currentPlayer.Score += (int)(100 +  50 * (temp) + 25 * currentPlayer.Coin);
			}
		}
	
	}
	

	
	protected override void HandleRequest (C2Request request)
	{
		if (Network.isClient && (owner == Network.player))	{
	        string type = request.Type;
			
	        if (currentPlayer.Alive == true && type.Equals(C2MessageType.Request_Movement))
	        {

		           float liftForce = (float)request.getData("VForce");
					
		            Vector3 upVec = transform.position;
		            upVec.Normalize();
					if (currentPlayer.Energy > 0){
			            constantForce.force = upVec * liftForce * 700;
						currentPlayer.Energy -= liftForce;
					}
					else{
						constantForce.force  = Vector3.zero;
						currentPlayer.Energy = 0;
					}
				
					
	            float rotation = (float)request.getData("Rotation");
	            transform.RotateAround(transform.position, rotation);
	        } 
		}
		else if (Network.isServer){
			string type = request.Type;

	        if ( type.Equals(C2MessageType.Request_Movement))
	        {
				NetworkPlayer nid = (NetworkPlayer)request.getData("NetworkPlayer");
			
				if (nid == owner){
					
		           float liftForce = (float)request.getData("VForce");
					
		            Vector3 upVec = transform.position;
					currentPlayer = ((Players)GameObject.Find("Players").GetComponent(typeof(Players))).playerList[nid];
					if (currentPlayer.Alive == false)return;
		            upVec.Normalize();
					if (currentPlayer.Energy > 0){
			            constantForce.force = upVec * liftForce * 700;
						currentPlayer.Energy -= liftForce;
					}
					else{
						constantForce.force  = Vector3.zero;
						currentPlayer.Energy = 0;
					}

			        float rotation = (float)request.getData("Rotation");
			        transform.RotateAround(transform.position, rotation);
					
				}
	        } 
		}
		

	}
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Destroy(this.gameObject);
	}
	protected override void HandleNotification (C2Notification notification)
	{
		string type = notification.Type;
		switch (type){
		case C2MessageType.Notification_CoinPicked: 
			this.OnCoinPicked();
			break;
		case C2MessageType.Notification_EnergyBlockPicked:
			this.OnEnergyBlockPicked();
			break;
		case C2MessageType.Notification_AcceleratorPicked:
			this.OnAcceleratorPicked();
			break;
		case C2MessageType.Notification_ShieldPicked:
			this.OnShieldPicked();
			break;		
		case C2MessageType.Notification_PlayerStatus:
			currentPlayer = (Player)notification.getData("Player");
			break;
		case C2MessageType.Notification_EndGame:
			transform.FindChild("Explosion").gameObject.SetActive(true);
			break;
		 default:
		        break;
		}
		
	}
	
	public void OnEnergyBlockPicked(){
		ClipEnergyBlockPicked.Play();

	}
	
	public void OnCoinPicked() {
		ClipCoinPicked.Play();
	}
	public void OnAcceleratorPicked() {
		ClipAcceleratorPicked.Play();
	}	
	public void OnShieldPicked() {
		transform.FindChild("Circle").gameObject.SetActive(true);
		ClipShieldPicked.Play();
	}	
	[RPC]
	void SetPlayer(NetworkPlayer player){
		owner = player;
		if(player==Network.player){
			enabled = true;
			((FauxGravityBody)transform.GetComponent(typeof(FauxGravityBody))).attractor = (FauxGravityAttractor)GameObject.Find ("Sphere").GetComponent(typeof(FauxGravityAttractor));
	
		    LunarLanderClient clientArchitecture = ((LunarLanderClient)GameObject.Find("LunarLanderGame").GetComponent(typeof(LunarLanderClient)));
		    clientArchitecture.makeConnect(
			clientArchitecture.clientConn,
			this
			);
			clientArchitecture.currentPlayer = this;
			clientArchitecture.makeConnect(clientArchitecture.currentPlayer , clientArchitecture.clientDistributor);

			transform.FindChild("Camera").camera.enabled = true;
				//((SmoothLookAt)transform.FindChild("Camera").GetComponent(typeof(SmoothLookAt))).target = transform;
			((AudioListener)transform.gameObject.GetComponent(typeof(AudioListener))).enabled = true;
		}
		else{
			transform.FindChild("Camera").camera.enabled = false;
				//(AudioListener)transform.gameObject.GetComponent(Typeof(AudioListener)).enabled = false ;
		}

		if (Network.isServer){
			
			((FauxGravityBody)transform.GetComponent(typeof(FauxGravityBody))).attractor = (FauxGravityAttractor)GameObject.Find ("Sphere").GetComponent(typeof(FauxGravityAttractor));
	
		    LunarLanderServer serverArchitecture = ((LunarLanderServer)GameObject.Find("LunarLanderGame").GetComponent(typeof(LunarLanderServer)));
		    serverArchitecture.makeConnect(
			serverArchitecture.connector1,
			this
			);
			//transform.gameObject.GetComponent(AudioListener).enabled = false;
		}
	}     
	
	
}

