using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C2DistributedConnector : C2Brick
{

	public C2DistributedConnector(){
		this.acceptedReqTypes = new List<string>();
		this.acceptedNotiTypes = new List<string>();
	}
	
	protected override void HandleRequest (C2Request request)
	{
		foreach(C2Port topPort in this.tops) 
		{
			topPort.PassRequest(request);
		}	
	}
	
	protected override void HandleNotification (C2Notification notification)
	{
		foreach(C2Port bottomPort in this.bottoms) 
		{
			bottomPort.PassNotification(notification);
		}	
	}
}

