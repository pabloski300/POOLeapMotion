using Leap.Unity.Animation;
using Leap.Unity.Examples;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomAnchorable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    InteractionBehaviour interaction;
    public InteractionBehaviour Interaction { get { return interaction; } }
    AnchorableBehaviour anchorable;
    public AnchorableBehaviour Anchorable { get { return anchorable; } }
    CustomAnchor mainAnchor;
    public CustomAnchor MainAnchor { get { return mainAnchor; } set { mainAnchor = value; } }
    CustomAnchor subAnchor;
    public CustomAnchor SubAnchor { get { return subAnchor; } set { subAnchor = value; } }

    public GameObject panelSuperior;
    public TextMeshPro textoPanelSuperior;
    public TransformTweenBehaviour tweenPanelSuperior;

    protected bool selected = false;
    protected bool isOver = false;

    Camera cam;

    // Use this for initialization
    public void Init(CustomAnchor main)
    {
        interaction = GetComponent<InteractionBehaviour>();
        anchorable = GetComponent<AnchorableBehaviour>();
        MainAnchor = main;
        cam = FindObjectOfType<Camera>();
        GraspEnd();
    }

    public void Update()
    {
        if (selected)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f,LayerMask.GetMask("Escenario")))
            {
                //suppose i have two objects here named obj1 and obj2.. how do i select obj1 to be transformed 
                if (hit.transform != null)
                {
                    transform.position = new Vector3(hit.point.x ,hit.point.y, hit.point.z);
                }
            }
        }


        if (isOver && Input.GetMouseButtonDown(0) && !interaction.ignoreGrasping)
        {
            selected = true;
            Interaction.OnHoverEnd();
            anchorable.isAttached = false;
            anchorable.anchor.NotifyDetached(anchorable);
            anchorable.anchor = null;
        }

        if (selected && isOver && Input.GetMouseButtonUp(0))
        {
            selected = false;
            anchorable.anchor = mainAnchor as Anchor;
            anchorable.anchorLerpCoeffPerSec = mainAnchor.LerpCoeficient;
            anchorable.isAttached = true;
            anchorable.anchor.NotifyAttached(anchorable);
            Debug.Log("click end");
        }
    }

    public void GraspEnd()
    {
        anchorable.anchorLerpCoeffPerSec = mainAnchor.LerpCoeficient;
        anchorable.anchor = mainAnchor;
        anchorable.isAttached = true;
        anchorable.anchor.NotifyAttached(anchorable);

    }

    public void ShowVariables()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected && !interaction.ignoreGrasping)
        {
            Interaction.OnHoverBegin();
            isOver = true;
            //Debug.Log(isOver);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            Interaction.OnHoverEnd();
            isOver = false;
        }
    }

    public void LookAtCamera()
    {
        if (!Interaction.ignoreGrasping && panelSuperior)
        {
            tweenPanelSuperior.PlayForward();
            //panelSuperior.transform.forward = -(cam.transform.position - panelSuperior.transform.position);
        }
    }
}
