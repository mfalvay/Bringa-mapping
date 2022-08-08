using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCycle : MonoBehaviour
{
    public BicycleRack bicycleRack;
    public float maxRatePerSecond = 20;
    [Range(0, 1)]
    public float rate = 0;
    float lastRan = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rate > 0 && Time.time - lastRan > 1/(maxRatePerSecond*rate))
        {
            bicycleRack.Raise((1000.0 / (maxRatePerSecond * rate)).ToString());
            lastRan = Time.time;
        }
        
    }
}
