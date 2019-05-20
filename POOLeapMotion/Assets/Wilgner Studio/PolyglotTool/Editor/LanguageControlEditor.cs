﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Polyglot;

[CustomEditor(typeof(LanguageControl))]
public class LanguageControlEditor : Editor
{
    private LanguageControl languageControl;
    private SerializedObject script;

    public SerializedProperty LanguageChanged;

    public void OnEnable()
    {
        languageControl = (LanguageControl)target;
        script = new SerializedObject(target);

        LanguageChanged = script.FindProperty("LanguageChanged");
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector ();
        script.Update();
        EditorGUI.BeginChangeCheck();
        this.languageControl.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(languageControl.GetSaveLocalPath());

        languageControl.selectedLanguage = EditorGUILayout.Popup("Selected Languages: ", languageControl.selectedLanguage, languageControl.polyglot.languages.ToArray());
        EditorGUILayout.PropertyField(LanguageChanged);

        if (EditorGUI.EndChangeCheck())
        {
            script.ApplyModifiedProperties();
        }
    }
}
