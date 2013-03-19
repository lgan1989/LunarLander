using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ConstantForce))]
public class ServerPlayerAction : C2Component
{
	
	public ServerPlayerAction(){
		
		this.RegisterAcceptedNoti(C2MessageType.Notification_Input);
	}
	
    public float upForce;
    protected override void HandleNotification(C2Notification notification)
    {
        string type = notification.Type;
	
        if (type.Equals(C2MessageType.Notification_Input))
        {
			float VInput = (float)notification.getData("VInput");
            float HInput = (float)notification.getData("HInput");
			NetworkPlayer player = (NetworkPlayer)notification.getData("NetworkPlayer");
			this.RequestServerMovement(VInput , HInput , player);
			
        }
    }

	private void RequestServerMovement(float VInput , float HInput , NetworkPlayer player){
  		float liftForce = VInput * Time.deltaTime * upForce;
        float rotation = HInput * Time.deltaTime;
        C2Request request = new C2Request(C2MessageType.Request_Movement);
		request.putData("NetworkPlayer" , player);
        request.putData("VForce", liftForce);
        request.putData("Rotation", rotation);
        this.SendRequest(request);		
	}

}

