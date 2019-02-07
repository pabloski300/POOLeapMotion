using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class CustomButton : InteractionButton {

	bool locked = false;
	public bool Locked{ get{ return locked; } set{ locked = value; ignoreContact = value; } }

	public new void Update(){
		if(!locked){
			base.Update();
		}
	}


}
