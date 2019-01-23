using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Examples;

public class Manager : MonoBehaviour {

    public static Manager Instance;

    public GameObject anchorablePref;

    public GameObject[] grid;

    List<CustomAnchorable> objects;

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

        objects = FindObjectsOfType<CustomAnchorable>().ToList();
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

    public void ReturnToAnchor(CustomAnchorable emmisor)
    {
        foreach(CustomAnchorable target in objects)
        {
            if(target != emmisor)
            {
                if (!target.Anchorable.isAttached && target.Anchorable.anchor != null)
                {
                    target.Anchorable.isAttached = true;
                    target.WorkStation.DeactivateWorkstation();
                    target.Anchorable.anchor.NotifyAttached(target.Anchorable);

                }
            }
        }
    }

    public void SpawnObject()
    {
        if (activeAnchors < 10) {
            Anchor anchor = grid[activeAnchors].GetComponent<Anchor>();
            GameObject newObject = Instantiate(anchorablePref, anchor.gameObject.transform.position, anchor.gameObject.transform.rotation);
            CustomAnchorable newAnchorable = newObject.GetComponent<CustomAnchorable>();
            anchor.gameObject.SetActive(true);
            newAnchorable.Init();
            newAnchorable.Anchorable.anchor = anchor;
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
