using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class MetodoBase : MonoBehaviour {

	public string nombre;
	public TMP_InputField input1;

	public string cabecera;

	public abstract void Init(ObjetoBase objeto);

	public abstract void Execute();

	public abstract string WriteFile();

	public abstract IEnumerator WaitForParams();

}
