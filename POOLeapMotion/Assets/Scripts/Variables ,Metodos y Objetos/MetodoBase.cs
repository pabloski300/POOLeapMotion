using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leap.Unity.Interaction;

public abstract class MetodoBase : CustomAnchorable {

	ObjetoBase objeto;
	public string nombre;
	public string cabecera;

    public bool inputsReady;

	public virtual void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(main);
		this.objeto = objeto;
		Interaction.OnGraspEnd += (() => Release());
		textoPanelSuperior.text = cabecera;
	}

	new void Update()
    {

        if (isOver && Input.GetMouseButton(0) && !Interaction.ignoreGrasping)
        {
            base.Update();
        }

        if (selected && isOver && Input.GetMouseButtonUp(0))
        {
            Interaction.OnGraspEnd();
        }

    }

	public abstract void Execute(List<TMP_InputField> inputs, TextMeshPro output);

	public abstract string WriteFile();

	void Release()
    {
        StartCoroutine(ReleaseDelay());
    }

    public IEnumerator ReleaseDelay(){
        yield return null;
        Debug.Log(Anchorable.anchor);
        if(Anchorable.anchor == null || Anchorable.anchor == MainAnchor){
            Debug.Log(Anchorable.anchor);
            selected = false;
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
            Debug.Log(Anchorable.anchor);
        }else{
            selected = false;
            ExploracionMetodo.Instance.OpenNew(this);
            ExploracionObjeto.Instance.Close();
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
        }
        Debug.Log(Anchorable.anchor);
    }

}
