using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadOnCommand : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Default();
        }
    }

    public void Save()
    {
        GUIParameterLink.SaveAll();

    }
    public void Load()
    {
        GUIParameterLink.LoadAll();

    }
    public void Default()
    {
        GUIParameterLink.DefaultAll();

    }
}
