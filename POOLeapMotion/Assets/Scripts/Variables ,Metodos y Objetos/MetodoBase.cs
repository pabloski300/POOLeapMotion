using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class MetodoBase : MonoBehaviour {

	public string nombre;
	public string Nombre{get{return nombre;} set{nombre = value;}}
	public TMP_InputField input1;

	public string cabecera;
	public string Cabecera{get{return cabecera;} set{cabecera = value;}}

	public abstract void Init(ObjetoBase objeto);

	public abstract void Execute();

	public abstract string WriteFile();

	public abstract IEnumerator WaitForParams();

}
