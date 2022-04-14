using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public string message;
    public Slider slider;

    public void OnValueChanging()
    {
        gameObject.GetComponent<Text>().text = $"{message} {slider.value}";
    }
}
