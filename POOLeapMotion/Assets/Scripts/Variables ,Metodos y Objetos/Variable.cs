using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable<T> : CustomAnchorable
{
	
	ObjetoBase objeto;
	public string nombre;
	public T valor;
	public CreadorVariable.ProteccionVar proteccion;

	public abstract string WriteFile();
	public virtual void Init(ObjetoBase objeto, CustomAnchor main){
		base.Init(main);
		Interaction.OnGraspEnd += (()=>GraspEnd());
		this.objeto = objeto;
	}
}
