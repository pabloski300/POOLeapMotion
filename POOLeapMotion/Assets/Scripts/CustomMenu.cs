using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity.Animation;
using UnityEngine;

public class CustomMenu : MonoBehaviour {

	[Header("Buttons")]
	public List<CustomButton> buttons;
	List<Rigidbody> rbs = new List<Rigidbody>();
	
	[Header("Tweens")]
	public TransformTweenBehaviour tween;

	public void Awake(){
		for(int i=0; i<buttons.Count; i++){
			rbs.Add(buttons[i].gameObject.GetComponent<Rigidbody>());
		}
	}

	public void Open(){
		tween.PlayForward();
	}

	public void Close(){
		tween.PlayBackward();
	}
	
	public void LockButtons(){
		for(int i=0; i<buttons.Count; i++){
			buttons[i].Locked = true;
			rbs[i].isKinematic = true;
		}
	}

	public void UnlockButtons(){
		for(int i=0; i<buttons.Count; i++){
			buttons[i].Locked = false;
			rbs[i].isKinematic = false;
		}
	}

	public void UnlockButtonsDelayed(float time){
		StartCoroutine(ChangeButtonState(time, false));
	}

	IEnumerator ChangeButtonState(float time, bool locked){
		for(int i=0; i<buttons.Count; i++){
			rbs[i].isKinematic = true;
		}
		yield return null;
		LockButtons();
		yield return new WaitForSeconds(time);
		for(int i=0; i<buttons.Count; i++){
			buttons[i].Locked = locked;
			rbs[i].isKinematic = locked;
		}
	}
}
