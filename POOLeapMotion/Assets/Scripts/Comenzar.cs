using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Animation;
using UnityEngine;

public class Comenzar : MonoBehaviour
{

    public TransformTweenBehaviour tweenInicio;
    // Start is called before the first frame update
    void Start()
    {
        tweenInicio.tweenDuration = 0.01f;
        tweenInicio.PlayForward();
    }
}
