using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomMenu : MonoBehaviour {

	public List<CustomButton> buttons;
	
	public void BlockButtons(){
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
