using Leap.Unity.Examples;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnchorable : MonoBehaviour {

    InteractionBehaviour interaction;
    public InteractionBehaviour Interaction { get { return interaction; } }
    AnchorableBehaviour anchorable;
    public AnchorableBehaviour Anchorable { get { return anchorable; } }
    WorkstationBehaviourExample workStation;
    public WorkstationBehaviourExample WorkStation { get { return workStation; } }

	// Use this for initialization
	public void Init () {
        interaction = GetComponent<InteractionBehaviour>();
        anchorable = GetComponent<AnchorableBehaviour>();
        workStation = GetComponent<WorkstationBehaviourExample>();
        interaction.OnGraspBegin += (()=>Manager.Instance.ReturnToAnchor(this));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
