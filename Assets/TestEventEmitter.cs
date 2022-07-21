using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEventEmitter : MonoBehaviour
{
    public UnityEvent OnTestTick;
    public KeyCode actionKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            OnTestTick.Invoke();
            Debug.Log("Tick event emitted");
        }
    }
}
