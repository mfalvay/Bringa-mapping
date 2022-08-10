using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibilityOnKey : MonoBehaviour
{
    public KeyCode actionKey = KeyCode.Space;
    public bool toggleState = false;
    public List<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
     foreach(GameObject obj in objects)
        {
            obj.SetActive(toggleState);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            toggleState = !toggleState;
            foreach (GameObject obj in objects)
            {
                obj.SetActive(toggleState);
            }
        }
        
    }
}
