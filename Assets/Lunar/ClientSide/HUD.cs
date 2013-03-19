using UnityEngine;
using System.Collections;

public class HUD : C2Component
{

	// Use this for GUI
	private Player player = new Player();
	
	
	void OnGUI ()
	{
	
		GUIStyle myStyle = new GUIStyle();
		Font myFont = (Font)Resources.Load("Fonts/batmfa", typeof(Font));
    	myStyle.font = myFont;
		myStyle.normal.textColor = Color.yellow;
		GUILayout.BeginArea( new Rect(Screen.width - 300, 20, 300 , 150 ));
		GUILayout.BeginHorizontal();
		GUILayout.Space(1);
		GUILayout.BeginVertical();
		
		GUILayout.Label("Score"  , myStyle);
		GUILayout.Label("Coin"  , myStyle);
		GUILayout.Label("Distance"  , myStyle);
		GUILayout.Label("Energy" , myStyle);
		GUILayout.Label("Speed", myStyle);
		GUILayout.Label("Shield" , myStyle);
		//GUILayout.Label("alive :" + player.Score);

		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		myStyle.margin.left = -100;
		GUILayout.Label(" " + player.Score.ToString() , myStyle);
		GUILayout.Label(" " + player.Coin.ToString() , myStyle);
		GUILayout.Label(" " + player.Distance.ToString("f2") , myStyle);
		GUILayout.Label(" " + player.Energy.ToString("f2"), myStyle);
		GUILayout.Label(" " + (((int)player.Speed + 100)) + " MPH", myStyle);
		GUILayout.Label(" " +  player.Shield.ToString("f2"), myStyle);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
	}
	protected override void HandleNotification (C2Notification notification)
	{
		string type = notification.Type;

		switch (type){
			case C2MessageType.Notification_PlayerStatus: 
				player = (Player)notification.getData("Player");
				break;
			 default:
			        break;
		}
	}	
}

