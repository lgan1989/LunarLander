
#pragma strict


@script AddComponentMenu("Camera-Control/Mouse Look")


 	public var target : Transform; 

// Private memeber data
private var mainCamera : Camera;	

private var mainCameraTransform : Transform;
private var cameraVelocity : Vector3 = Vector3.zero;
private var cameraOffset : Vector3 = Vector3.zero;
private var initOffsetToPlayer : Vector3;
		// Cursor settings
	public var cursorPlaneHeight : float = 0;
	public var cursorFacingCamera : float = 0;
	public var cursorSmallerWithDistance : float = 0;
	public var cursorSmallerWhenClose : float = 1;
    
    public var targetHeight:float = 1.7f; 
    public var distance:float = 55.0f;
    public var offsetFromWall:float = 0.1f;

    public var maxDistance : float= 100.0f; 
    public var minDistance: float = 30.0f; 

    public var xSpeed : float= 200.0f; 
    public var ySpeed : float= 200.0f; 

    public var yMinLimit : int= -80; 
    public var yMaxLimit : int = 80; 

    public var zoomRate : int = 40; 

    public var rotationDampening: float = 3.0f; 
    public var zoomDampening: float = 5.0f; 
    
    public var collisionLayers : LayerMask = -1;

    private var xDeg : float= 0.0f; 
    private var yDeg : float= 0.0f; 
    private var currentDistance: float; 
    private var desiredDistance: float; 
    private var correctedDistance: float; 
	private var playerMovementPlane : Plane;

private var screenMovementSpace : Quaternion;
private var screenMovementForward : Vector3;
private var screenMovementRight : Vector3;

    function Start () 
    { 
        var  angles : Vector3 = transform.eulerAngles; 
        xDeg = angles.x; 
        yDeg = angles.y; 
        targetHeight = target.lossyScale.y;
        currentDistance = distance; 
        desiredDistance = distance; 
        correctedDistance = distance; 
	
        // Make the rigid body not change rotation 
        if (rigidbody) 
            rigidbody.freezeRotation = true; 
           
    } 
    
    /** 
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */ 
    function LateUpdate () 
    { 
    	var  vTargetOffset:Vector3;
    	
    	
       // Don't do anything if target is not defined 
        if (!target) 
            return; 
		targetHeight = target.lossyScale.y;
        // If either mouse buttons are down, let the mouse govern camera position 
        var pressedRight : boolean = false;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) 
        { 
            xDeg += Input.GetAxis ("Mouse X") * xSpeed * 0.02f; 
            yDeg -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f; 
        } 
           // otherwise, ease behind the target if any of the directional keys are pressed 
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) 
        { 
            var targetRotationAngle = target.eulerAngles.y; 
            var currentRotationAngle = transform.eulerAngles.y; 
            xDeg = Mathf.LerpAngle (currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime); 
          	
        } 
      	if (Input.GetMouseButton(1)){
      		pressedRight = true;
      	}

        yDeg = ClampAngle (yDeg, yMinLimit, yMaxLimit); 

        // set camera rotation 
        var rotation : Quaternion = Quaternion.Euler (yDeg, xDeg, 0); 

        // calculate the desired distance 
        desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (desiredDistance); 
        desiredDistance = Mathf.Clamp (desiredDistance, minDistance, maxDistance); 
        correctedDistance = desiredDistance; 

        // calculate desired camera position
        vTargetOffset = new Vector3 (0, -targetHeight, 0);
        var position : Vector3 = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset); 

        // check for collision using the true target's desired registration point as set by user using height 
        var collisionHit : RaycastHit; 
        var trueTargetPosition : Vector3 = new Vector3 (target.position.x, target.position.y + targetHeight, target.position.z); 

        // if there was a collision, correct the camera position and calculate the corrected distance 
        var isCorrected = false; 
        if (Physics.Linecast (trueTargetPosition, position,  collisionHit, collisionLayers.value)) 
        { 
            // calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - offsetFromWall; 
                 
            isCorrected = true;
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance 
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance; 

		// keep within legal limits
        currentDistance = Mathf.Clamp (currentDistance, minDistance, maxDistance); 

        // recalculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset); 
       
		// used to adjust the camera based on cursor or joystick position
		
		
        transform.rotation = rotation; 
        transform.position = position; 
       
		
		
    } 

    function ClampAngle ( angle: float,  min: float,  max: float) 
    { 
        if (angle < -360) 
            angle += 360; 
        if (angle > 360) 
            angle -= 360; 
        return Mathf.Clamp (angle, min, max); 
    } 
