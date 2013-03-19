using UnityEngine;
using System.Collections;

public enum LunarLanderGameState { playing, won, lost };

public class LunarLanderClient : C2Architecture {
	//public C2DistributedConnector clientDistrConn;
	public C2Connector clientConn;
	public ClientNetworkConnect networkConnect;
	public ClientNetworkGUI networkGUI;
	public C2DistributedConnector clientDistributor; 
	
	public CurrentPlayer currentPlayer;

	public StatisticGUI statisticGUI;
	public UserInput input;
	public ClientPlayerAction playerAction;
	public HUD hud;
	public EndGame endGame;
	
	//private LunarLanderGameState gameState = LunarLanderGameState.playing;

	// Use this for initialization
	void Start () {
		this.InitC2Architecture();
	}
	
	// Update is called once per frame
	void Update () {
		//GUILayout.Label("Energy: " + player.Energy);

	}  
	//OnGUI is called multiple times per frame. Use this for GUI stuff only!
    void OnGUI() {
		/*if (gameState == LunarLanderGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }*/
        //We display the game GUI from the playerscript
        //It would be nicer to have a seperate script dedicated to the GUI though...
      
	

    }  	
	public void makeConnect(C2Brick bottomBrick, C2Brick topBrick) 
	{
		this.Connect(bottomBrick , topBrick);
	}

    public void SetGameOver() {
        Time.timeScale = 0.0f; //Pause game
       // gameState = LunarLanderGameState.lost;
    }
	
	private void InitC2Architecture() {
		
		
		this.Connect(this.playerAction, this.clientConn);
		playerAction.RegisterAcceptedNoti(C2MessageType.Notification_Input);
		
		this.clientConn.RegisterAcceptedReq(C2MessageType.Request_ConnectToServer);
		this.clientConn.RegisterAcceptedReq(C2MessageType.Request_DisconnectFromServer);
		this.clientConn.RegisterAcceptedReq(C2MessageType.Request_Movement);
		
		this.clientConn.RegisterAcceptedNoti(C2MessageType.Notification_PlayerStatus);
		this.clientConn.RegisterAcceptedNoti(C2MessageType.Notification_Input);
		this.clientConn.RegisterAcceptedNoti(C2MessageType.Notification_EndGame);
		
		//this.Connect(this.clientConn , this.currentPlayer);
		this.Connect(this.hud, this.clientConn);
		
		this.hud.RegisterAcceptedNoti(C2MessageType.Notification_PlayerStatus);
		
		this.Connect(this.statisticGUI, this.clientConn);
		this.Connect(this.endGame, this.clientConn);
		

		this.Connect(this.clientConn, this.input);
		
		this.Connect(this.input, this.clientDistributor);
		
		this.Connect(this.clientConn, this.clientDistributor);
		
		this.Connect(this.networkGUI , this.clientConn);
		this.Connect(this.clientConn , this.networkConnect);
		
	}
	
	private C2Component makeObject(string type) {
		GameObject coinSpawner = new GameObject();
		coinSpawner.AddComponent(type);
		//coinSpawner.GetComponent(type).;
		return null;
	}
}

