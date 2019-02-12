using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MetodoBase : MonoBehaviour {

	public abstract void Init(ObjetoBase objeto);

	public abstract void Execute();

	public abstract void WriteFile();

	public abstract IEnumerator WaitForParams();

}
