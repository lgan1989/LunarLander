using System;

public static class C2MessageType
{

    public const string Request_Input = "RequestInput";
	
	//
	public const string Request_SpawnCoin = "SpawnCoin";
	public const string Request_SpawnEnergyBlock = "SpawnEnergyBlock";
	public const string Request_SpawnAccelerator = "SpawnAccelerator";
	public const string Request_SpawnShield = "SpawnShield";
	public const string Request_SpawnStone = "SpawnStone";
	public const string Request_Spanw = "Spawn";
	
	//
	public const string Request_Movement = "RequestMovement";
	
	public const string Request_StartServer = "StartServer";
	public const string Request_CloseServer = "CloseServer";
	
	public const string Request_ConnectToServer = "ConnectToServer";
	public const string Request_DisconnectFromServer = "DisconnectFromServer";
	
	
	public const string Request_UpdateCoin = "UpdateCoin";
	public const string Request_UpdateEnergy = "UpdateEnergy";
	public const string Request_UpdateSpeed = "UpdateSpeed";
	public const string Request_UpdateShield = "UpdateShield";
	public const string Request_UpdateAlive = "UpdateAlive";
	
	public const string Request_PlayerStatus = "GetPlayerStatus";
	
	public const string Request_ClientAction = "ClientAction";
	
	//============================================================================
	
	
	public const string Notification_PlayerStatus = "SendPlayerStatus";
	
	public const string Notification_Input = "NotificationInput";
	
	public const string Notification_Item_Collided = "ItemCollided";
	public const string Notification_Obstacle_Collided = "ObstacleCollided";
	
	public const string Notification_Picked = "Picked";
	public const string Notification_CoinPicked = "CoinPicked";
	public const string Notification_EnergyBlockPicked = "EnergyBlockPicked";
	public const string Notification_AcceleratorPicked = "AcceleratorPicked";
	public const string Notification_ShieldPicked = "ShieldPicked";
	public const string Notification_EndGame = "EndGame";
	
	
	public const string Notification_Movement = "NotificationMovement";
	
}
