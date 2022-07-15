using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBicycleEventEmitter : MonoBehaviour
{
    public BicycleRack bicycleRack;
    public KeyCode DummyActionKey;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(DummyActionKey))
        {
            bicycleRack.Raise();
        }
    }
}
