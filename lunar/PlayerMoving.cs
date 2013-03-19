using UnityEngine;
using System.Collections;

public class PlayerMoving : MonoBehaviour {
	
	private float radius = 260;
	private float polar = 4 / Mathf.PI;
	private float elevation = 4 / Mathf.PI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //These two lines are all there is to the actual movement..
        float moveInput = -Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
		this.polar += moveInput;
		this.elevation += Time.deltaTime * 3;
		Vector3 newPos = new Vector3();
		Math.SphericalToCartesian(this.radius, this.polar, this.elevation, newPos);
        transform.position = newPos;
	}
}
