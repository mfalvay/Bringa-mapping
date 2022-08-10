using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHelper : MonoBehaviour
{
    public float XPosition { get => transform.position.x; set => transform.position = new Vector3(value, transform.position.y, transform.position.z); }
    public float YPosition { get => transform.position.y; set => transform.position = new Vector3(transform.position.x, value, transform.position.z); }
    public float ZPosition { get => transform.position.z; set => transform.position = new Vector3(transform.position.x, transform.position.y, value); }

}
