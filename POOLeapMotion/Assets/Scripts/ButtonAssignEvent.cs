using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAssignEvent : MonoBehaviour
{
    InteractionButton button3D;

    CustomAnchorable parent;
    // Start is called before the first frame update
    void Awake()
    {
        button3D = GetComponent<InteractionButton>();
        parent = GetComponentInParent<CustomAnchorable>();

        Debug.Log(parent.gameObject);

        button3D.OnPress += (()=>Manager.Instance.RemoveOne(parent));
    }
}
