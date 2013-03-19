using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class C2Message
{
    protected string type;
    public string Type
    {
        get
        {
            return type;
        }
    }

    protected Type msgSrcType = null;
    public Type MsgSrcType
    {
        get
        {
            return msgSrcType;
        }
        set
        {
            msgSrcType = value;
        }
    }

    protected Dictionary<string, object> package;
	
	public C2Message(){
		type = "";
		this.package = new Dictionary<string, object>();
	}

    public C2Message(string type)
    {
        this.type = type;
        this.package = new Dictionary<string, object>();
    }

    public void putData(string type, object val)
    {
        this.package.Add(type, val);
    }


    public object getData(string type)
    {
        return this.package[type];
    }


}

