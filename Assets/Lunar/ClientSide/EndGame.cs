using UnityEngine;
using System.Collections;

public class EndGame : C2Component
{
	
	private bool show = false;
	
	public EndGame(){
		RegisterAcceptedNoti(C2MessageType.Notification_EndGame);
	}
	
	void OnGUI(){
		if (Network.isClient && show){
			GUIStyle myStyle = new GUIStyle();
			Font myFont = (Font)Resources.Load("Fonts/batmfa", typeof(Font));
	    	myStyle.fontSize = 40;
			myStyle.font = myFont;
			myStyle.normal.textColor = Color.yellow;
			
			GUILayout.BeginArea( new Rect(Screen.width/2 - 150, 30, 300 , 150 ));
			GUILayout.BeginHorizontal();

			GUILayout.Label("Game Over"  , myStyle);
		
			GUILayout.EndHorizontal();
			GUILayout.EndArea();	
			
		}
	}
	
	protected override void HandleNotification (C2Notification notification)
	{
		if (notification.Type.Equals(C2MessageType.Notification_EndGame)){
			show = true;
		}
	}	

}

