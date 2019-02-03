using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Examples;

public class Manager : MonoBehaviour {

    [HideInInspector]
    public static Manager Instance;

    [SerializeField]
    GameObject anchorablePref;

    GameObject gridParent;

    List<CustomAnchor> grid;

    List<CustomAnchorable> objects;

    CustomAnchor centralAnchor;

    Transform objectsPivot;

    int activeAnchors;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        objectsPivot = GameObject.FindGameObjectWithTag("ObjectsPivot").transform;
        objects = FindObjectsOfType<CustomAnchorable>().ToList();
        gridParent = GameObject.FindGameObjectWithTag("GridParent");
        grid = gridParent.GetComponentsInChildren<CustomAnchor>().ToList();
        centralAnchor = GameObject.FindGameObjectWithTag("CentralAnchor").GetComponent<CustomAnchor>();
        grid.Sort();

        Debug.Log(grid.Count);

        for(int i=0; i<grid.Count; i++)
        {
            grid[i].gameObject.SetActive(false);
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

    public void ReturnToAnchor(CustomAnchorable emmisor)
    {
        if (emmisor != null)
        {
            emmisor.Anchorable.anchor = null;
        }
        foreach(CustomAnchorable target in objects)
        {
            if(target != emmisor)
            {
                if (target.Anchorable.anchor == target.CentralAnchor)
                {
                    target.ReturnToStart();
                }
            }
        }
    }

    public void SpawnObject()
    {
        if (activeAnchors < grid.Count) {
            CustomAnchor anchor = grid[activeAnchors].GetComponent<CustomAnchor>();
            GameObject newObject = Instantiate(anchorablePref, anchor.gameObject.transform.position, anchor.gameObject.transform.rotation,objectsPivot);
            CustomAnchorable newAnchorable = newObject.GetComponent<CustomAnchorable>();
            anchor.gameObject.SetActive(true);
            newAnchorable.Init();
            newAnchorable.Anchorable.anchor = anchor;
            newAnchorable.GridAnchor = anchor;
            newAnchorable.CentralAnchor = centralAnchor;
            objects.Add(newAnchorable);
            activeAnchors++;
            newAnchorable.ReturnToStart();
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
            CustomAnchorable oldObject = objects[activeAnchors-1];
            objects.RemoveAt(activeAnchors-1);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
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
