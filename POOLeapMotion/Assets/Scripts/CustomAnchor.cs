using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class CustomAnchor : Anchor, IComparable
{
    [Range(0.1f,100)]
    public float LerpCoeficient;

    [HideInInspector]
    public CustomAnchorable objectAnchored;

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        CustomAnchor otherAnchor = obj as CustomAnchor;
        if (otherAnchor != null)
        {
            if (this.transform.localPosition.y == otherAnchor.transform.localPosition.y)
            {
                return this.transform.localPosition.x < otherAnchor.transform.localPosition.x ? -1 : 1;
            }
            return this.transform.localPosition.y > otherAnchor.transform.localPosition.y ? -1 : 1;
        }
        else
            throw new ArgumentException("Object is not a Temperature");
    }
}
