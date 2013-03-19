using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//basic element in C2 architecture
public abstract class C2Brick : MonoBehaviour
{
	protected List<C2Port> tops = new List<C2Port>();
	protected List<C2Port> bottoms = new List<C2Port>();
	protected List<string> acceptedReqTypes = null;
	protected List<string> acceptedNotiTypes = null;
	
	protected abstract void HandleRequest(C2Request request);	
	protected abstract void HandleNotification(C2Notification notification);
	
	protected static bool testConnectionMode = false;
	
	public bool AcceptReq(string type) {
		if(acceptedReqTypes != null) {
			if(acceptedReqTypes.Count >0 ) {
				if(acceptedReqTypes.Contains(type)) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		} else {
			return false;
		}			
	}
	
	public bool AcceptNoti(string type) {
		if(acceptedNotiTypes != null) {
			if(acceptedNotiTypes.Count >0 ) {
				if(acceptedNotiTypes.Contains(type)) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}	
		} else {
			return false;
		}
	}
	
	public void RegisterAcceptedReq(string type) {
		
		if(this.acceptedReqTypes == null) {
			this.acceptedReqTypes = new List<string>();
		}
		this.acceptedReqTypes.Add(type);
	
	}
	
	public void PrintAcceptedNotiType(){
		foreach (string t in this.acceptedNotiTypes){
			if (testConnectionMode)
				Debug.Log(name + " has registed notification type " + t );
		}
	}
	
	public void PrintAcceptedReqType(){
		foreach (string t in this.acceptedReqTypes){
			if (testConnectionMode)
				Debug.Log(name + " has registed request type " + t );
		}
	}
	
	public void RegisterAcceptedNoti(string type) {
		//Debug.Log(name + " has registered notification type " + type);
		if(this.acceptedNotiTypes == null) {
			this.acceptedNotiTypes = new List<string>();
		}
		this.acceptedNotiTypes.Add(type);
	}
	
	public void AddTopPort(C2Port top) {
		tops.Add(top);
	}	
	
	public void AddBottomPort(C2Port bottom) {
		bottoms.Add(bottom);
	}	
	

}

