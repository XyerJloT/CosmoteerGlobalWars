using UnityEngine;
using UnityEngine.UI;

public class GetSliderText : MonoBehaviour
{
    [SerializeField] Slider slider;
    public string message;

    private void Update()
    {
        gameObject.GetComponent<Text>().text = $"{message}{slider.value.ToString()}";
    }
}
