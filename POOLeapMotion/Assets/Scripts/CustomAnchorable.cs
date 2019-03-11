using Leap.Unity.Examples;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomAnchorable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler {

    InteractionBehaviour interaction;
    public InteractionBehaviour Interaction { get { return interaction; } }
    AnchorableBehaviour anchorable;
    public AnchorableBehaviour Anchorable { get { return anchorable; } }
    WorkstationBehaviourExample workStation;
    public WorkstationBehaviourExample WorkStation { get { return workStation; } }
    CustomAnchor gridAnchor;
    public CustomAnchor GridAnchor { get { return gridAnchor; } set { gridAnchor = value; } }
    CustomAnchor centralAnchor;
    public CustomAnchor CentralAnchor { get { return centralAnchor; } set { centralAnchor = value; } }

    // Use this for initialization
    public void Init () {
        interaction = GetComponent<InteractionBehaviour>();
        anchorable = GetComponent<AnchorableBehaviour>();
        workStation = GetComponent<WorkstationBehaviourExample>();
        interaction.OnGraspBegin += (()=> Manager.Instance.ReturnToAnchor(this));
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
            workStation.ActivateWorkstation();
        }
    }

    public void ReturnToStart()
    {
        Debug.Log(gridAnchor);
        anchorable.anchorLerpCoeffPerSec = gridAnchor.LerpCoeficient;
        anchorable.anchor = gridAnchor;
        anchorable.isAttached = true;
        workStation.DeactivateWorkstation();
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
            Manager.Instance.ReturnToAnchor(this as ObjetoBase ,centralAnchor);
            anchorable.anchorLerpCoeffPerSec = centralAnchor.LerpCoeficient;
            anchorable.isAttached = true;
            anchorable.anchor.NotifyAttached(anchorable);
            workStation.ActivateWorkstation();
            Manager.Instance.ReturnToAnchor(this as ObjetoBase ,centralAnchor);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Interaction.OnHoverBegin();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Interaction.OnHoverEnd();
    }
}
