using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoBase : MonoBehaviour {

	public string nombre;

	public Dictionary<string,IntVariable> variablesInt = new Dictionary<string, IntVariable>();
	public Dictionary<string,FloatVariable> variablesFloat = new Dictionary<string, FloatVariable>();
	public Dictionary<string,BoolVariable> variablesBool = new Dictionary<string, BoolVariable>();

	public Dictionary<string,MetodoBase> metodos = new Dictionary<string, MetodoBase>();

	private void Start(){
	}

}
