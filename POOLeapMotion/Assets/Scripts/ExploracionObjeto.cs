using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploracionObjeto : CustomMenu
{

    public static ExploracionObjeto Instance;

    string nombreVariable;

    VariableObjeto variable;

    ObjetoBase objeto;

    public CustomAnchor anchorObjeto;
    public CustomAnchor anchorVariable;
    public CustomAnchor anchorMetodo;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        GetButton("EliminarObjeto").OnPress += (()=>EliminarObjeto());
        GetButton("EliminarReferencia").OnPress += (()=>EliminarReferencia());
        GetButton("EliminarVariable").OnPress += (()=>EliminarVariable());
        GetButton("Contraer").gameObject.SetActive(false);
    }

    public void Open(VariableObjeto _variable)
    {
        Open();
        variable = _variable;
        variable.SubAnchor = variable.MainAnchor;
        variable.MainAnchor = anchorVariable;
        variable.Anchorable.anchorLerpCoeffPerSec = anchorVariable.LerpCoeficient;
        variable.Anchorable.anchor = anchorVariable;
        variable.Anchorable.isAttached = true;
        variable.Anchorable.anchor.NotifyAttached(variable.Anchorable);
        if (variable.objetoReferenciado != null)
        {
            objeto = variable.objetoReferenciado;
            objeto.SubAnchor = objeto.MainAnchor;
            objeto.MainAnchor = anchorObjeto;
            objeto.Anchorable.anchorLerpCoeffPerSec = anchorObjeto.LerpCoeficient;
            objeto.Anchorable.anchor = anchorObjeto;
            objeto.Anchorable.isAttached = true;
            objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
            GetButton("Expandir").gameObject.SetActive(true);
            GetButton("EliminarObjeto").gameObject.SetActive(true);
            GetButton("EliminarReferencia").gameObject.SetActive(true);
        }
        else
        {
            GetButton("Expandir").gameObject.SetActive(false);
            GetButton("EliminarObjeto").gameObject.SetActive(false);
            GetButton("EliminarReferencia").gameObject.SetActive(false);
        }
    }

    public void End()
    {
        base.Close();
        if (variable != null)
        {
            variable.MainAnchor = variable.SubAnchor;
            variable.SubAnchor = null;
            variable.Anchorable.anchorLerpCoeffPerSec = variable.MainAnchor.LerpCoeficient;
            variable.Anchorable.anchor = variable.MainAnchor;
            variable.Anchorable.isAttached = true;
            variable.Anchorable.anchor.NotifyAttached(variable.Anchorable);
            variable = null;
        }
        if (objeto != null)
        {
            Contraer();
            objeto.MainAnchor = objeto.SubAnchor;
            objeto.SubAnchor = null;
            objeto.Anchorable.anchorLerpCoeffPerSec = objeto.MainAnchor.LerpCoeficient;
            objeto.Anchorable.anchor = objeto.MainAnchor;
            objeto.Anchorable.isAttached = true;
            objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
            objeto = null;
        }
    }

    public void Expandir()
    {
        variable.Interaction.ignoreGrasping = true;
        anchorMetodo.gameObject.SetActive(true);
        objeto.Expandir();
        UnlockButtonsDelayed(0.5f);
    }

    public void Contraer()
    {
        if (variable)
        {
            variable.Interaction.ignoreGrasping = false;
        }
        anchorMetodo.gameObject.SetActive(false);
        objeto.Contraer();
        UnlockButtonsDelayed(0.5f);
    }

    public void EliminarObjeto()
    {
        variable.objetoReferenciado = null;
        MenuGrid.Instance.RemoveOneObject(objeto);
        objeto = null;
        GetButton("Expandir").gameObject.SetActive(false);
        GetButton("EliminarObjeto").gameObject.SetActive(false);
        GetButton("EliminarReferencia").gameObject.SetActive(false);
    }

    public void EliminarVariable()
    {
        if (variable.objetoReferenciado != null)
        {
            variable.objetoReferenciado = null;
        }
        MenuGrid.Instance.RemoveOneVariable(variable);
        variable = null;
        End();
        MenuGrid.Instance.Open();
    }

    public void EliminarReferencia()
    {
        variable.objetoReferenciado = null;
        objeto.MainAnchor = objeto.SubAnchor;
        objeto.SubAnchor = null;
        objeto.Anchorable.anchorLerpCoeffPerSec = objeto.MainAnchor.LerpCoeficient;
        objeto.Anchorable.anchor = objeto.MainAnchor;
        objeto.Anchorable.isAttached = true;
        objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
        objeto = null;
        GetButton("Expandir").gameObject.SetActive(false);
        GetButton("EliminarObjeto").gameObject.SetActive(false);
        GetButton("EliminarReferencia").gameObject.SetActive(false);
    }
}
