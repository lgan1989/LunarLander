using UnityEngine;
using System.Collections;

public class UserInput : C2Component
{

	// Use this for initialization
	float lastClientVInput = 0;
	float lastClientHIput = 0;
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		float VInput = Input.GetAxis("Vertical");
		float HInput = Input.GetAxis("Horizontal"); 
		if (VInput != lastClientHIput || HInput != lastClientHIput){
			this.NotifyInput(VInput , HInput);
			this.RequestInput(VInput , HInput);
			lastClientHIput = HInput;
			lastClientVInput = VInput;
		}
		
	}
	
	private void NotifyInput(float VInput , float HInput) {
		C2Notification notification = new C2Notification(C2MessageType.Notification_Input);
		notification.putData("VInput",VInput);
		notification.putData("HInput",HInput);
		this.SendNotification(notification);
	}
	private void RequestInput(float VInput , float HInput) {
		C2Request request = new C2Request(C2MessageType.Request_Input);
		request.putData("VInput",VInput);
		request.putData("HInput",HInput);
		this.SendRequest(request);
	}	

}

