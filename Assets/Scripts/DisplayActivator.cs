using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach(Display display in Display.displays){
            display.Activate();
        }
        
    }
}
