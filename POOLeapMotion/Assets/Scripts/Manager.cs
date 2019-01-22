using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour {

    public GameObject anchorablePref;

    public GameObject[] grid;

    List<AnchorableBehaviour> objects;

    int activeAnchors;

	// Use this for initialization
	void Start () {
        objects = FindObjectsOfType<AnchorableBehaviour>().ToList();
        for(int i=0; i<10; i++)
        {
            grid[i].SetActive(false);
        }
        activeAnchors = 0;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnObject();
        }
    }

    public void ReturnToAnchor(AnchorableBehaviour emmisor)
    {
        foreach(AnchorableBehaviour target in objects)
        {
            if(target != emmisor)
            {
                target.isAttached = true;
                if (target.anchor != null)
                {
                    target.anchor.NotifyAttached(target);
                }
            }
        }
    }

    public void SpawnObject()
    {
        if (activeAnchors < 10) {
            Anchor anchor = grid[activeAnchors].GetComponent<Anchor>();
            GameObject newObject = Instantiate(anchorablePref, anchor.gameObject.transform.position, anchor.gameObject.transform.rotation);
            AnchorableBehaviour newAnchorable = newObject.GetComponent<AnchorableBehaviour>();
            anchor.gameObject.SetActive(true);
            newAnchorable.anchor = anchor;
            objects.Add(newAnchorable);
            activeAnchors++;
            ReturnToAnchor(null);
        }
        else
        {
            Debug.Log("Maximos objetos creados");
        }
    }

    public void Remove()
    {
        if (activeAnchors > 0)
        {
            Anchor anchor = grid[activeAnchors-1].GetComponent<Anchor>();
            AnchorableBehaviour anchorable = objects[activeAnchors-1];
            objects.RemoveAt(activeAnchors-1);
            anchor.NotifyDetached(anchorable);
            Destroy(anchorable.gameObject);
            anchor.gameObject.SetActive(false);
            activeAnchors--;
            ReturnToAnchor(null);
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }
}
