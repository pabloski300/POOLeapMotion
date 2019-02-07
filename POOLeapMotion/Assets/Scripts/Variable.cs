using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable<T> : MonoBehaviour
{
	string nombre;
	public string Name{get{return nombre;}set{nombre = value;}}
	T valor;
	public T Valor {get{return valor;} set{valor = value;}}

	
	public abstract void WriteFile();
	public abstract void Init(ObjetoBase objeto);
}
