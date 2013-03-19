using UnityEngine;
using System.Collections;

public class GameTitle : MonoBehaviour {
	private static int buttonWidth = 100;
	private static int buttonHeight = 50;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		int placing = 0;
		
		placing = -2;
		Rect NewGamePos = new Rect(Screen.width / 2 - buttonWidth / 2,
									Screen.height / 2 - (1 - placing) * buttonHeight / 2, 
									buttonWidth,
									buttonHeight);
		
		placing = 2;
		Rect QuitPos = new Rect(Screen.width / 2 - buttonWidth / 2,
								Screen.height / 2 - (1 - placing) * buttonHeight / 2, 
								buttonWidth,
								buttonHeight);
		
		if(GUI.Button(NewGamePos, "New Game")) {
			Application.LoadLevel("lunar");
		}
		
		if(GUI.Button(QuitPos, "Quit Game")) {
			Application.Quit();
		}
	}
}
