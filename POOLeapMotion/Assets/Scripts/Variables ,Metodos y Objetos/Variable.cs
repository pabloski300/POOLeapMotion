using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable<T> : MonoBehaviour
{
	public string nombre;
	public T valor;
	public CreadorVariable.ProteccionVar proteccion;

	ObjetoBase objeto;

	
	public abstract string WriteFile();
	public virtual void Init(ObjetoBase objeto){
		this.objeto = objeto;
	}
}
