using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    Camera camera;

    public float HorizontalShift { get => camera.lensShift.x; set => camera.lensShift = new Vector2(value, camera.lensShift.y); }
    public float VerticalShift { get => camera.lensShift.y; set => camera.lensShift = new Vector2(camera.lensShift.x, value); }
    public float FOV { get => camera.fieldOfView; set => camera.fieldOfView = value; }
    // Start is called before the first frame update
    void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
