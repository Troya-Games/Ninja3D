using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    
    
    public static int LastIndex<T>(this List<T> trans)
    {
        return trans.Count-1;
    }

    public static void  ResetLocal(this Transform transform)
    {
       transform.localPosition=Vector3.zero;
       transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public static void ResetVelocity(this Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity=Vector3.zero;
    }

    public static void SetPositionAndRotationTo(this Transform transform,Transform to)
    {
        transform.position = to.position;
        transform.rotation = to.rotation;
    }
    
}