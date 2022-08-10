using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using YamlDotNet.Serialization;
using System.IO;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class GUIParameterLink : MonoBehaviour
{
    public class PersistentData
    {
        public Dictionary<string, string> values = new Dictionary<string, string>();
    }

    public static PersistentData persistentData = new PersistentData();
    static bool initialized = false;

    public Slider slider;
    public TMP_InputField inputField;
    public FloatEvent onValueChanged = new FloatEvent();

    public float value = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        slider ??= GetComponentInChildren<Slider>();
        inputField ??= GetComponentInChildren<TMP_InputField>();
        slider.onValueChanged.AddListener(OnSliderChanged);
        inputField.onSubmit.AddListener(OnFieldChanged);
    }

    void Start()
    {


        if (!initialized)
        {
            initialized = true;
            LoadAll();
        }
    }

    void OnSliderChanged(float val)
    {
        inputField.SetTextWithoutNotify(val.ToString("F"));
        onValueChanged.Invoke(val);
        value = val;
    }

    void OnFieldChanged(string val)
    {
        if (float.TryParse(val, out float result))
        {
            slider.SetValueWithoutNotify(result);
            onValueChanged.Invoke(result);
            value = result;
        }
    }
    public void SetParameterWithoutNotify(float val)
    {
        inputField.SetTextWithoutNotify(val.ToString("F"));
        slider.SetValueWithoutNotify(val);
        value = val;
    }

    public void SetParameter(float val)
    {
        OnSliderChanged(val);
        OnFieldChanged(val.ToString());
        value = val;
    }
    static public void LoadAll()
    {
        Debug.Log("Loading camera settings");

        StreamReader reader = new StreamReader($"{Application.streamingAssetsPath}/camera_settings.yaml");
        string yamlFile = reader.ReadToEnd();
        reader.Close();

        IDeserializer deserializer = new DeserializerBuilder().Build();
        persistentData = deserializer.Deserialize<PersistentData>(yamlFile);

        foreach (GUIParameterLink link in GameObject.FindObjectsOfType<GUIParameterLink>(true))
        {
            string key = $"{link.transform.parent.name}/{link.transform.name}";
            if (persistentData.values.TryGetValue(key, out string data))
            {
                link.SetParameter(float.Parse(data));
            }
            else
            {
                Debug.LogWarning($"Key does not exist: {key}");
            }
        }
    }
    static public void DefaultAll()
    {
        Debug.Log("Loading default camera settings");

        StreamReader reader = new StreamReader($"{Application.streamingAssetsPath}/camera_settings_default.yaml");
        string yamlFile = reader.ReadToEnd();
        reader.Close();

        IDeserializer deserializer = new DeserializerBuilder().Build();
        persistentData = deserializer.Deserialize<PersistentData>(yamlFile);

        foreach (GUIParameterLink link in GameObject.FindObjectsOfType<GUIParameterLink>(true))
        {
            string key = $"{link.transform.parent.name}/{link.transform.name}";
            if (persistentData.values.TryGetValue(key, out string data))
            {
                link.SetParameter(float.Parse(data));
            }
            else
            {
                Debug.LogWarning($"Key does not exist: {key}");
            }
        }
    }

    static public void SaveAll()
    {
        Debug.Log("Saving camera settings");

        foreach (GUIParameterLink link in GameObject.FindObjectsOfType<GUIParameterLink>(true))
        {
            string key = $"{link.transform.parent.name}/{link.transform.name}";
            string value = link.value.ToString("F4");
            persistentData.values[key] = value;
        }

        ISerializer serializer = new SerializerBuilder().Build();
        string serializedYaml = serializer.Serialize(persistentData);
        StreamWriter writer = new StreamWriter($"{Application.streamingAssetsPath}/camera_settings.yaml", false);
        writer.Write(serializedYaml);
        writer.Close();
    }
}
