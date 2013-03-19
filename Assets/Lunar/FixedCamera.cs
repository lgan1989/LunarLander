using UnityEngine;
using System.Collections;
		[AddComponentMenu("Camera-Control/FixedCamera")]
	public class FixedCamera : MonoBehaviour
	{
		private Transform target = null;

		public FixedCamera ()
		{
			
		}
		void Update() {
			transform.LookAt(target, new Vector3(0,-1,0));
		}
	}


