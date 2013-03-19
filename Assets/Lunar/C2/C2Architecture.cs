using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//architecture manager, used to establish and connect components
public class C2Architecture : MonoBehaviour
{
	protected List<C2Brick> bricks;
	
	//connect two bricks by adding port
	protected void Connect(C2Brick bottomBrick, C2Brick topBrick) 
	{
		
		C2Port conn = (C2Port)ScriptableObject.CreateInstance("C2Port");
		conn.TopBrick = topBrick;
		conn.ButtomBrick = bottomBrick;
		topBrick.AddBottomPort(conn);
		bottomBrick.AddTopPort(conn);
	}

	
	
	
}

