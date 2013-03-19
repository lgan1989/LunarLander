using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ConstantForce))]
public class ClientPlayerAction : C2Component
{
	
	public ClientPlayerAction(){
		
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
            this.RequestMovement(VInput, HInput);
			
        }
    }

    private void RequestMovement(float VInput, float HInput)
    {

        float liftForce = VInput * Time.deltaTime * upForce;
        float rotation = HInput * Time.deltaTime;
        C2Request request = new C2Request(C2MessageType.Request_Movement);
        request.putData("VForce", liftForce);
        request.putData("Rotation", rotation);
        this.SendRequest(request);

    }

}

