using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class RepresentacionMetodo : CustomAnchorable
{
    LineaMetodo linea;
    // Start is called before the first frame update
    public void Init(CustomAnchor mainAnchor, LineaMetodo linea)
    {
        base.Init(mainAnchor);
        this.linea = linea;
        Interaction.OnGraspEnd += (() => Release());
    }

    // Update is called once per frame
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

    void Release()
    {
        StartCoroutine(ReleaseDelay());
    }

    public IEnumerator ReleaseDelay()
    {
        yield return null;


        if (Anchorable.anchor == Manager.Instance.papeleraCreadorClases)
        {
            linea.Eliminar();
        }

        selected = false;
        Anchorable.anchor = MainAnchor as Anchor;
        Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
        Anchorable.isAttached = true;
        Anchorable.anchor.NotifyAttached(Anchorable);

    }
}
