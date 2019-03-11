using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoBase : CustomAnchorable {

	public string nombre;

	public string codigo = "";

	public Dictionary<string,IntVariable> variablesInt = new Dictionary<string, IntVariable>();
	public Dictionary<string,FloatVariable> variablesFloat = new Dictionary<string, FloatVariable>();
	public Dictionary<string,BoolVariable> variablesBool = new Dictionary<string, BoolVariable>();

	public Dictionary<string,MetodoBase> metodos = new Dictionary<string, MetodoBase>();

	public Transform variablesParent;
	public Transform metodoParent;

	private void Start(){
	}

	

}
