using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoBase : MonoBehaviour {

	public string nombre;

	public Dictionary<string,IntVariable> variablesInt;

	public Dictionary<string,MetodoBase> metodos;

	private void Start(){
		foreach (string key in metodos.Keys)
		{
			metodos[key].Init(this);
		}
	}

}
