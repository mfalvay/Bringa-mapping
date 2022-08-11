using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessHelper : MonoBehaviour
{
    Volume volume;
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }
    public float Weight { get => volume.weight; set => volume.weight = value; }

    
}
