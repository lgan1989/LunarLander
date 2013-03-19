using UnityEngine;
using System.Collections;

public class Player
{
	
	private int score = 0;
	private float distance = 0;
	private float energy = 1500;
	private int coin = 0;
	private float speed = 100;
	private bool alive = true;
	private float shield = 10.0f;
	private NetworkPlayer owner;
	
	public const float MAX_SPEED = 1500.0f;
	private const float MAX_ENERGY = 1500.0f;

	
	public Player(NetworkPlayer player){
		owner = player;
	}
	public Player(){
	}	
	public Player(NetworkPlayer _owner , int _score , float _distance , int _coin , float _energy , float _speed , float _shield,  bool _alive){
		owner = _owner;
		score = _score;
		distance = _distance;
		coin = _coin;
		energy = _energy;
		speed = _speed;
		shield = _shield;
		alive = _alive;
	}

	public NetworkPlayer Owner{
		get
		{
			return owner;	
		}
		set
		{
			owner = value;
		}
	}
	public int Score{
		get
		{
			return score;	
		}
		set
		{
			score = value;
		}
	}
	public float Distance{
		get
		{
			return distance;	
		}
		set
		{
			distance = value;
		}
	}
	public int Coin{
		get
		{
			return coin;	
		}
		set
		{
			coin = value;
		}
	}
		
	public float Energy{
		get
		{
			return energy;	
		}
		set
		{
			energy = value;
		}
	}
	public float Speed{
		get
		{
			return speed;	
		}
		set
		{
			speed = value;
		}
	}
	public float Shield{
		get
		{
			return shield;	
		}
		set
		{
			shield = value;
		}
	}
	public bool Alive{
		get
		{
			return alive;	
		}
		set
		{
			Alive = value;
		}
	}
	public void updatePlayerSco(){
		score ++;
	}
	public void updatePlayerCoin(){
		coin ++;
	}	
	public void updatePlayerSpeed(){
		speed += 100;
		speed = speed < MAX_SPEED ? speed : MAX_SPEED;
	}

	public void updatePlayerEnergy(){
		energy += 500;
		energy = energy < MAX_ENERGY ? energy : MAX_ENERGY;
	}
	public void updatePlayerAlive(){
		if (alive == true){
			alive = false;
		}
	}
	public void updatePlayerShield(){
		shield = 10.0f;
	}
}   

