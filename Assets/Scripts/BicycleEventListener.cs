using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BicycleEventListener : MonoBehaviour
{
    public BicycleRack bicycleRack;
    public UnityEvent onEventTrigged;
    // Start is called before the first frame update
    void Start()
    {
        bicycleRack.Subscribe(OnEventTriggered);
    }

    private void OnEventTriggered()
    {
        onEventTrigged.Invoke();
    }
}
