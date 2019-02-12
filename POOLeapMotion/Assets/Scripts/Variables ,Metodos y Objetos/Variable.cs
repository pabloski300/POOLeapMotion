using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable<T> : MonoBehaviour
{
	string nombre;
	public string Nombre{get{return nombre;}set{nombre = value;}}
	T valor;
	public T Valor {get{return valor;} set{valor = value;}}
	CreadorVariable.ProteccionVar proteccion;
	public CreadorVariable.ProteccionVar Proteccion{get{return proteccion;}set{proteccion=value;}}

	ObjetoBase objeto;

	
	public abstract void WriteFile();
	public virtual void Init(ObjetoBase objeto){
		this.objeto = objeto;
	}
}
