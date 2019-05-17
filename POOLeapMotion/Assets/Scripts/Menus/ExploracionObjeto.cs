using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExploracionObjeto : CustomMenu
{
    string nombreVariable;

    public VariableObjeto variable;

    public ObjetoBase objeto;

    public CustomAnchor anchorObjeto;
    public CustomAnchor anchorVariable;
    public CustomAnchor anchorMetodo;

    public TextMeshPro title;
    public TextMeshPro info;

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
        anchorMetodo.gameObject.SetActive(false);
        info.text = "Para poder ejecutar metodos asigna un objeto a la variable";
        if (variable.objetoReferenciado != null)
        {
            objeto = variable.objetoReferenciado;
            objeto.SubAnchor = objeto.MainAnchor;
            objeto.MainAnchor = anchorObjeto;
            objeto.Anchorable.anchorLerpCoeffPerSec = anchorObjeto.LerpCoeficient;
            objeto.Anchorable.anchor = anchorObjeto;
            objeto.Anchorable.isAttached = true;
            objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
            anchorMetodo.gameObject.SetActive(true);
            info.text = "";
            title.text = "Inspeccionando Variable " + variable.nombre;
            Expandir();
        }
    }

    public void Open(ObjetoBase _objeto)
    {
        Open();
        objeto = _objeto;
        objeto.SubAnchor = objeto.MainAnchor;
        objeto.MainAnchor = anchorObjeto;
        objeto.Anchorable.anchorLerpCoeffPerSec = anchorObjeto.LerpCoeficient;
        objeto.Anchorable.anchor = anchorObjeto;
        objeto.Anchorable.isAttached = true;
        objeto.Anchorable.anchor.NotifyAttached(objeto.Anchorable);
        anchorMetodo.gameObject.SetActive(false);
        info.text = "Para poder ejecutar metodos inspeccione una variable con un objeto asignado";
        title.text = "Inspeccionando Objeto " + objeto.nombre;
        Expandir();
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
            variable.Interaction.ignoreGrasping = false;
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
            objeto.Interaction.ignoreGrasping = false;
            objeto = null;
        }
    }

    public void Expandir()
    {
        if (variable != null)
        {
            variable.Interaction.ignoreGrasping = true;
        }
        objeto.Expandir();
        objeto.Interaction.ignoreGrasping = true;
    }

    public void Contraer()
    {
        if (variable != null)
        {
            variable.Interaction.ignoreGrasping = false;
        }
        objeto.Contraer();
        objeto.Interaction.ignoreGrasping = false;
    }
}
