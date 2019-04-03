using Leap.Unity.Animation;
using Leap.Unity.Examples;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomAnchorable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler, IPointerUpHandler {

    InteractionBehaviour interaction;
    public InteractionBehaviour Interaction { get { return interaction; } }
    AnchorableBehaviour anchorable;
    public AnchorableBehaviour Anchorable { get { return anchorable; } }
    CustomAnchor gridAnchor;
    public CustomAnchor GridAnchor { get { return gridAnchor; } set { gridAnchor = value; } }
    CustomAnchor centralAnchor;
    public CustomAnchor CentralAnchor { get { return centralAnchor; } set { centralAnchor = value; } }

    public GameObject panelSuperior;
    public TextMeshPro textoPanelSuperior;

    // Use this for initialization
    public void Init () {
        interaction = GetComponent<InteractionBehaviour>();
        anchorable = GetComponent<AnchorableBehaviour>();
        interaction.OnGraspBegin += (()=> MenuGrid.Instance.ReturnToAnchor(this));
        interaction.OnGraspEnd += (() => GraspEnd());
	}

    void GraspEnd()
    {
        if(anchorable.anchor != gridAnchor)
        {
            anchorable.anchorLerpCoeffPerSec = centralAnchor.LerpCoeficient;
            anchorable.anchor = centralAnchor;
            anchorable.isAttached = true;
            anchorable.anchor.NotifyAttached(anchorable);
        }
    }

    public void ReturnToStart()
    {
        anchorable.anchorLerpCoeffPerSec = gridAnchor.LerpCoeficient;
        anchorable.anchor = gridAnchor;
        anchorable.isAttached = true;
        anchorable.anchor.NotifyAttached(anchorable);
    }

    public void ShowVariables()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(anchorable.anchor == gridAnchor)
        {
            MenuGrid.Instance.ReturnToAnchor(this as ObjetoBase ,centralAnchor);
            anchorable.anchorLerpCoeffPerSec = centralAnchor.LerpCoeficient;
            anchorable.isAttached = true;
            anchorable.anchor.NotifyAttached(anchorable);
            MenuGrid.Instance.ReturnToAnchor(this as ObjetoBase ,centralAnchor);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Interaction.OnHoverBegin();
        Debug.Log("hover begin");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Interaction.OnHoverEnd();
        Debug.Log("hover end");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void LookAtCamera(){
        panelSuperior.transform.forward = -(Camera.current.transform.position - panelSuperior.transform.position);
    }
}
