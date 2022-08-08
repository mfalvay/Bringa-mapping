using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class GUIParameterLink : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField inputField;
    public FloatEvent onValueChanged;
        
    // Start is called before the first frame update
    void Start()
    {
        slider ??= GetComponentInChildren<Slider>();
        inputField ??= GetComponentInChildren<TMP_InputField>();
        slider.onValueChanged.AddListener(OnSliderChanged);
        inputField.onSubmit.AddListener(OnFieldChanged);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSliderChanged(float val)
    {
        inputField.SetTextWithoutNotify(val.ToString("F"));
        onValueChanged.Invoke(val);
    }

    void OnFieldChanged(string val)
    {
        if (float.TryParse(val, out float result))
        {
            slider.SetValueWithoutNotify(result);
            onValueChanged.Invoke(result);
        }
    }
    public void SetParameterWithoutNotify(float val)
    {
        inputField.SetTextWithoutNotify(val.ToString("F"));
        slider.SetValueWithoutNotify(val);
    }

    public void SetParameter(float val)
    {
        OnSliderChanged(val);
    }
}
