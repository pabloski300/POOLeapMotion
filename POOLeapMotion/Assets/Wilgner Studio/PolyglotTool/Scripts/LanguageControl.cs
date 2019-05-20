﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Polyglot;

public class LanguageControl : MonoBehaviour {

    public PolyglotSave polyglot;
    public UnityEvent LanguageChanged = new UnityEvent();
    public int selectedLanguage;
    public int previousSelectedLanguage = -1;
    FindTranslation[] findTranslation;

    // Use this for initialization
    void Awake () {
        if (polyglot == null)
            Debug.Log("Polyglot is null!");

        LanguageChanged.AddListener(UpdateTextTranslation);
        findTranslation = GameObject.FindObjectsOfType<FindTranslation>();
    }
	
	// Update is called once per frame
	void Update () {
        if (selectedLanguage != previousSelectedLanguage)
            LanguageChanged.Invoke();
    }

    public void UpdateTextTranslation()
    {
        for(int i=0; i < findTranslation.Length; i++)
        {
            findTranslation[i].SetText();
        }
        previousSelectedLanguage = selectedLanguage;
    }

    public string GetSaveLocalPath()
    {
        return "Assets/Resources/Polyglot.asset";
    }
}
