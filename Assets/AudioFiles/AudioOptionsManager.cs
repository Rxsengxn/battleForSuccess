using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public void OnSliderValueChange(float value)
    {
        AudioManager.Instance.UpdateMixerVolume(value);
    }

    private void Awake()
    {
        SetSliderInitialValue(AudioManager.Instance.MasterVolume);
    }

    public void SetSliderInitialValue(float value)
    {
        Slider slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider not found!");
            return;
        }
        slider.value = value;
    }
}