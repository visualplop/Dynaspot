using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI value;
    [SerializeField] private CrossCursor cursor;
    [SerializeField] private int target;


    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            value.text = v.ToString("0.00");

            switch (target)
            {
                case 1:
                    cursor.SetMinRadius(v);
                    break;
                case 2:
                    cursor.SetMaxRadius(v);
                    break;
                case 3:
                    cursor.SetSpeedUp(v);
                    break;
                case 4:
                    cursor.SetSpeedDown(v);
                    break;
                case 5:
                    cursor.SetLagBuffer(v);
                    break;
                case 6:
                    cursor.SetSpeedT(v);
                    break;
            }
        });
    }

    public void SetPreset(int preset)
    {
        switch (target)
        {
            case 1:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2: 
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        value.SetText("0.05");
                        slider.value = 0.05f;
                        break;
                }
                break;
            case 2:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2:
                    case 4:
                    case 6:
                        value.SetText("0.7");
                        slider.value = 0.7f;
                        break;
                    case 3:
                    case 5:
                    case 7:
                        value.SetText("1.40");
                        slider.value = 1.40f;
                        break;
                }
                break;
            case 3:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        value.SetText("1.5");
                        slider.value = 1.5f;
                        break;
                }
                break;
            case 4:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        value.SetText("0.8");
                        slider.value = 0.8f;
                        break;
                    case 6:
                    case 7:
                        value.SetText("0.9");
                        slider.value = 0.9f;
                        break;
                }
                break;
            case 5:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 7:
                        value.SetText("0.14");
                        slider.value = 0.14f;
                        break;
                    case 4:
                    case 5:
                        value.SetText("0.06");
                        slider.value = 0.06f;
                        break;
                }
                break;
            case 6:
                switch (preset)
                {
                    case 1:
                        value.SetText("0.00");
                        slider.value = 0.0f;
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        value.SetText("0.4");
                        slider.value = 0.4f;
                        break;
                }
                break;
        }
    }
}
