using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity.Animation;
using UnityEngine;

public class CustomMenu : MonoBehaviour {

	[Header("Buttons")]
	public List<CustomButton> buttons;

	[Header("Tweens")]
	public TransformTweenBehaviour tween;

	public void Open(){
		tween.PlayForward();
	}

	public void Close(){
		tween.PlayBackward();
	}
	
	public void LockButtons(){
		for(int i=0; i<buttons.Count; i++){
			buttons[i].Locked = true;
		}
	}

	public void UnlockButtons(){
		for(int i=0; i<buttons.Count; i++){
			buttons[i].Locked = false;
		}
	}
}
