using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    
    public static void DeleteAllChilds(this Transform transform)
    {
        foreach (Transform trans in transform) Object.Destroy(trans.gameObject);
    }
}
