using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BicycleSensorTickCallback();

[CreateAssetMenu(fileName = "New Bicycle Rack", menuName = "Bicycle Rack/New Bycicle Rack")]
public class BicycleRack : ScriptableObject
{
    public string websocketChannel = "test";
    private event BicycleSensorTickCallback OnTick;

    public void Subscribe(BicycleSensorTickCallback callback)
    {
        OnTick += callback;
    }
    public void Unsubscribe(BicycleSensorTickCallback callback)
    {
        OnTick -= callback;
    }

    public void Raise()
    {
        OnTick?.Invoke();
    }

}
