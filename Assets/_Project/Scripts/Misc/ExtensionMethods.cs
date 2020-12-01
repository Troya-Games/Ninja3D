using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    
    
    public static int LastIndex<T>(this List<T> trans)
    {
        return trans.Count-1;
    }
    
}