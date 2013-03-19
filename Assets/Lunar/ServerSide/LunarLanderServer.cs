using UnityEngine;
using System.Collections;

public class LunarLanderServer : C2Architecture
{
	public C2Connector connector1;
	public C2Connector connector2;
	public ServerNetworkConnect networkConnect;
	public ServerDistributor serverDistributor;
	public Attacklogic attackLogic;
	public PickLogic pickLogic;
	public Spawner spawner;
	public CrashLogic crashLogic;
	public Coins coins;
	public Players players;
	public Items items;
	public Obstacles obstacles;
	public Statistics statistics;
	public Bullets bullets;
	public ClientAction clientAction;
	
	public ServerNetworkGUI serverGUI;
	
	public PlayerDeathLogic playerDeathLogic;
	public ServerPlayerAction playerAction;

	// Use this for initialization
	void Start () {
		this.InitC2Architecture();
	}
	
	public void makeConnect(C2Brick bottomBrick, C2Brick topBrick){
		
		this.Connect(bottomBrick , topBrick);
	}
	
	private void InitC2Architecture() {
		
		this.Connect(
			this.serverDistributor,
		    this.playerDeathLogic
		    );
		
		this.Connect(
		    this.serverDistributor,
		    this.connector2
		    );
		
		this.Connect(
		    this.serverDistributor,
		    this.connector1
		    );
			
		this.Connect( 
			this.serverDistributor,
			this.pickLogic
		     ); 			
		
		this.Connect( 
			this.serverDistributor,
			this.clientAction
		     ); 			
				
		this.Connect(this.playerDeathLogic, this.connector2);
		
		this.Connect(this.connector2, this.attackLogic);
		this.Connect(this.connector2, this.crashLogic);
		this.Connect(this.connector2, this.connector1);	
		
		this.Connect(this.attackLogic, this.connector1);
		this.Connect(this.pickLogic, this.connector1);
		this.Connect(this.spawner, this.connector1);
		this.Connect(this.crashLogic, this.connector1);
		
		
	//	this.Connect(this.connector1,this.bullets);
		this.Connect(this.connector1,this.players);
		this.Connect(this.connector1,this.coins);
		this.Connect(this.connector1,this.items);
		this.Connect(this.connector1,this.obstacles);
	//	this.Connect(this.connector1,this.statistics);
		
		this.Connect(this.connector1, this.networkConnect);
		this.Connect(this.serverGUI , this.connector1);
	
		this.Connect(this.connector1 , this.clientAction);
      	this.Connect(this.playerAction, this.connector1);

        this.connector1.RegisterAcceptedReq(C2MessageType.Request_Movement);
		
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_StartServer);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_CloseServer);
		

		
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_UpdateAlive);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_UpdateEnergy);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_UpdateCoin);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_UpdateSpeed);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_UpdateShield);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_PlayerStatus);
		
		this.connector1.RegisterAcceptedNoti(C2MessageType.Notification_Item_Collided);
		this.connector1.RegisterAcceptedNoti(C2MessageType.Notification_Obstacle_Collided);
		this.connector1.RegisterAcceptedNoti(C2MessageType.Notification_PlayerStatus);
		this.connector1.RegisterAcceptedNoti(C2MessageType.Notification_Input);
		
		pickLogic.RegisterAcceptedNoti(C2MessageType.Notification_Item_Collided);
		crashLogic.RegisterAcceptedNoti(C2MessageType.Notification_Obstacle_Collided);
		
		this.connector2.RegisterAcceptedNoti(C2MessageType.Notification_EndGame);
		
		//register for spawner
		
		coins.RegisterAcceptedReq(C2MessageType.Request_SpawnCoin);
		items.RegisterAcceptedReq(C2MessageType.Request_SpawnEnergyBlock);
		items.RegisterAcceptedReq(C2MessageType.Request_SpawnAccelerator);
		items.RegisterAcceptedReq(C2MessageType.Request_SpawnShield);
		obstacles.RegisterAcceptedReq(C2MessageType.Request_SpawnStone);
		
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_SpawnCoin);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_SpawnEnergyBlock);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_SpawnAccelerator);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_SpawnShield);
		this.connector1.RegisterAcceptedReq(C2MessageType.Request_SpawnStone);
		
		
	}

}