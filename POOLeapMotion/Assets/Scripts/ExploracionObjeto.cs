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
            buttons[0].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);
            buttons[4].gameObject.SetActive(true);
        }
        else
        {
            buttons[0].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[4].gameObject.SetActive(false);
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
        }
    }

    public void Expandir(){
        variable.Interaction.ignoreGrasping = true;
        anchorMetodo.gameObject.SetActive(true);
        objeto.Expandir();
    }

    public void Contraer(){
        if(variable){
            variable.Interaction.ignoreGrasping = false;
        }
        anchorMetodo.gameObject.SetActive(false);
        objeto.Contraer();
    }

    public void EliminarObjeto(){
        variable.objetoReferenciado = null;
        MenuGrid.Instance.RemoveOneObject(objeto);
        objeto = null;
        buttons[0].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);
        buttons[4].gameObject.SetActive(false);
    }

    public void EliminarVariable(){
        variable.objetoReferenciado = null;
        MenuGrid.Instance.RemoveOneVariable(variable);
        variable = null;
        End();
        MenuGrid.Instance.Open();
    }

    public void EliminarReferencia(){
        variable.objetoReferenciado = null;
        objeto.MainAnchor = objeto.SubAnchor;
        objeto.SubAnchor = null;
        objeto.Anchorable.anchorLerpCoeffPerSec = objeto.MainAnchor.LerpCoeficient;
        objeto.Anchorable.anchor = objeto.MainAnchor;
        objeto.Anchorable.isAttached = true;
        objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
        objeto = null;
        buttons[0].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);
        buttons[4].gameObject.SetActive(false);
    }
}
