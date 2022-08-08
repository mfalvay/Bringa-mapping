using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

[System.Serializable]
public class StringEvent : UnityEvent<string> { };

public class BicycleEventListener : MonoBehaviour
{
    public BicycleRack bicycleRack;
    public float interval = 10000;
    // Start is called before the first frame update
    void Start()
    {
        bicycleRack.Subscribe(OnEventTriggered);
    }

    private void OnEventTriggered(string data)
    {
        interval = float.Parse(data);
    }
}
