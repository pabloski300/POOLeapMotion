using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoBase : CustomAnchorable {

	public string nombre;

	public string codigo = "";


	public List<IntVariable> variablesInt;
	public List<FloatVariable> variablesFloat;
	public List<BoolVariable> variablesBool;

	public List<MetodoBase> metodos;

	public Transform variablesParent;
	public CustomAnchor[] anchorsVariables;
	public Transform metodoParent;
	public CustomAnchor[] anchorsMetodo;

	private void Start(){
		IntVariable[] intVariables = GetComponentsInChildren<IntVariable>();

		foreach(IntVariable k in variablesInt){
			k.Init(this);
		}
		foreach(FloatVariable k in variablesFloat){
			k.Init(this);
		}
		foreach(BoolVariable k in variablesBool){
			k.Init(this);
		}
		foreach(MetodoBase k in metodos){
			k.Init(this);
		}
		textoPanelSuperior.text = "new "+nombre+"();";
	}

	

}
