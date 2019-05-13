using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leap.Unity.Interaction;

public abstract class MetodoBase : CustomAnchorable
{

    ObjetoBase objeto;
    public string nombre;
    public string cabecera;

    public bool inputsReady;

    protected ExploracionMetodo em;
    ExploracionObjeto eo;

    public void Init(ObjetoBase objeto, CustomAnchor main)
    {
        base.Init(main);

        em = (ExploracionMetodo)Manager.Instance.GetMenu("ExploracionMetodo");
        eo = (ExploracionObjeto)Manager.Instance.GetMenu("ExploracionObjeto");

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

    public IEnumerator ReleaseDelay()
    {
        yield return null;

        if(Anchorable.anchor == Manager.Instance.inspeccionarMetodo)
        {
            selected = false;
            em.OpenNew(this);
            eo.Close();
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
        }else{
            selected = false;
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
        }
        Debug.Log(Anchorable.anchor);
    }

}
