using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Examples;
using System.IO;

public class Manager : MonoBehaviour {

    [HideInInspector]
    public static Manager Instance;

    [SerializeField]
    public List<GameObject> anchorablePrefs;

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
        //objects = FindObjectsOfType<CustomAnchorable>().ToList();
        objects = new List<CustomAnchorable>();
        gridParent = GameObject.FindGameObjectWithTag("GridParent");
        grid = gridParent.GetComponentsInChildren<CustomAnchor>().ToList();
        centralAnchor = GameObject.FindGameObjectWithTag("CentralAnchor").GetComponent<CustomAnchor>();
        grid.Sort();

        for(int i=0; i<grid.Count; i++)
        {
            grid[i].gameObject.SetActive(false);
        }
        activeAnchors = 0;
        Debug.Log(Application.streamingAssetsPath);
        /* AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objetos"));
        anchorablePrefs = bundle.LoadAllAssets<GameObject>();*/
        Debug.Log(anchorablePrefs.Count);
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
            
            CustomAnchor anchor = grid.FirstOrDefault(x=>!x.gameObject.activeSelf);
            GameObject newObject = Instantiate(anchorablePrefs[0], anchor.gameObject.transform.position, anchor.gameObject.transform.rotation,objectsPivot);
            CustomAnchorable newAnchorable = newObject.GetComponent<CustomAnchorable>();
            anchor.gameObject.SetActive(true);
            anchor.objectAnchored = newAnchorable;
            newAnchorable.Init();
            newAnchorable.Anchorable.anchor = anchor;
            newAnchorable.GridAnchor = anchor;
            newAnchorable.CentralAnchor = centralAnchor;
            objects.Add(newAnchorable);
            activeAnchors++;
            newAnchorable.ReturnToStart();
            Debug.Log(activeAnchors);
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
            CustomAnchor anchor = grid.LastOrDefault(x=>x.gameObject.activeSelf);
            CustomAnchorable oldObject = anchor.objectAnchored;
            objects.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeAnchors);
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }

    public void RemoveOne(CustomAnchorable c)
    {
        if (activeAnchors > 0)
        {
            CustomAnchor anchor = grid.FirstOrDefault(x=>x.objectAnchored == c);
            CustomAnchorable oldObject = anchor.objectAnchored;
            objects.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeAnchors);
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }
}
