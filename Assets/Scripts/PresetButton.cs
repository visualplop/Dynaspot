using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetButton : MonoBehaviour
{
    [SerializeField] private int Preset = 1;
    [SerializeField] private List<SliderScript> slidersList = new List<SliderScript>();

    public void OnClick()
    {
        foreach (SliderScript slider in slidersList)
        {
            slider.SetPreset(Preset);
        }
    }
}
