using UnityEngine;
using System.Collections;

//the port connecting bricks
public class C2Port : ScriptableObject
{

	private C2Brick topBrick;
	public C2Brick TopBrick 
	{
		get 
		{
			return topBrick;
		}
		set
		{
			topBrick = value;
		}
	}
	private C2Brick buttomBrick;
	public C2Brick ButtomBrick 
	{
		get 
		{
			return buttomBrick;
		}
		set
		{
			buttomBrick = value;
		}
	}
	
	public C2Port(C2Brick top, C2Brick bottom) 
	{
		this.TopBrick = top;
		this.ButtomBrick = bottom;
	}
	
	//send request to upper brick through this port
	public bool PassRequest(C2Request request) 
	{			
		if (this.topBrick == null)return false;
		if(this.topBrick.AcceptReq(request.Type)) {
			//Debug.Log(this.topBrick.name + " accepted request " + request.Type + " from " + this.ButtomBrick.name);
			
		//	this.topBrick.PrintAcceptedReqType();
			
			this.topBrick.SendMessage("HandleRequest",request);
			return true;
		}
		return false;
	}

	
	//send notification to lower brick through this port
	public bool PassNotification(C2Notification notification) 
	{			
		
		if (this.buttomBrick == null)return false;
		if(this.buttomBrick.AcceptNoti(notification.Type)) {
			this.buttomBrick.SendMessage("HandleNotification",notification);
		}
		return false;
	}
	
}

