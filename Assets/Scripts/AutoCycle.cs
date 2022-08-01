using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCycle : MonoBehaviour
{
    public BicycleRack bicycleRack;
    [Range(0, 1)]
    public float speed = 0;
    float lastRan = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0 && Time.time -lastRan > (1/Mathf.Lerp(0,10,speed))) {
            lastRan = Time.time;
            bicycleRack.Raise();
        }
        
    }
}
