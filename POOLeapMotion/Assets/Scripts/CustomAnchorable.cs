using Leap.Unity.Animation;
using Leap.Unity.Examples;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomAnchorable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
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
        ReturnToStart();
    }

    public void Update()
    {
        if (selected)
        {
            Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z * 1.25f));
            transform.position = new Vector3(pos.x * -1, (pos.y * -1) + (cam.transform.position.y * 2), transform.position.z);
            //transform.position += new
        }


        if (isOver && Input.GetMouseButtonDown(0) && !interaction.ignoreGrasping)
        {
            selected = true;
            Interaction.OnHoverEnd();
            anchorable.isAttached = false;
            anchorable.anchor.NotifyDetached(anchorable);
            anchorable.anchor = null;
            //Debug.Log("clicked");
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
        if (anchorable.anchor != mainAnchor)
        {
            anchorable.anchorLerpCoeffPerSec = subAnchor.LerpCoeficient;
            anchorable.anchor = subAnchor;
            anchorable.isAttached = true;
            anchorable.anchor.NotifyAttached(anchorable);
        }
    }

    public void ReturnToStart()
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

    /*public void OnPointerClick(PointerEventData eventData)
    {
            Interaction.OnHoverEnd();
            anchorable.isAttached = false;
            anchorable.anchor.NotifyDetached(anchorable);
            selected = true;
            Debug.Log("click begin");  
    }*/

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

    public void OnPointerUp(PointerEventData eventData)
    {
        //selected = false;
        //anchorable.anchor = gridAnchor as Anchor;
        //anchorable.anchorLerpCoeffPerSec = gridAnchor.LerpCoeficient;
        //anchorable.isAttached = true;
        //anchorable.anchor.NotifyAttached(anchorable);
        //Debug.Log("click end");
        //MenuGrid.Instance.ReturnToAnchor(this as ObjetoBase ,centralAnchor);
    }

    public void LookAtCamera()
    {
        panelSuperior.transform.forward = -(cam.transform.position - panelSuperior.transform.position);
    }
}
