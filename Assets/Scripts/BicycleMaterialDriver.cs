using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter
{
    
    public float value = 1;
    public float alpha = 0.5f;
    public float Sample(float newValue) {
        return Sample(newValue, alpha);
    }

    public float Sample(float newValue, float alpha) {
        this.alpha = alpha;
        value = newValue * alpha + (1 - alpha) * value;
        return value;

    }
}

public class BicycleMaterialDriver : MonoBehaviour
{
    public BicycleRack bicycleRack;

    [Space(20)]

    public float frequency = 0;
    private Filter filter = new Filter();
    float timePassed;
    public Vector2 mapFrom = new Vector2(0, 1);
    public Vector2 mapTo = new Vector2(0, 1);
    public bool clamp = false;

    [Space(20)]

    public string materialPropertyReference = "_testValue";
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        frequency = 1/filter.Sample(timePassed, 0.01f);
        float val = frequency.Map(mapFrom.x, mapFrom.y, mapTo.x, mapTo.y);

        val = clamp ? (val > Mathf.Max(mapTo.x, mapTo.y) ? Mathf.Max(mapTo.x, mapTo.y) : (val < Mathf.Min(mapTo.x, mapTo.y) ? Mathf.Min(mapTo.x, mapTo.y) : val)) : val;
        renderer.material.SetFloat(materialPropertyReference, val);
        Debug.Log(val);
    }

    void OnSensorDetect()
    {
        timePassed = 0;
    }

    private void OnEnable()
    {
        bicycleRack.Subscribe(OnSensorDetect);
    }
    private void OnDisable()
    {
        bicycleRack.Unsubscribe(OnSensorDetect);

    }
    private void OnDestroy()
    {
        bicycleRack.Unsubscribe(OnSensorDetect);
    }


}
public static class ExtensionMethods
{

    public static float Map(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}