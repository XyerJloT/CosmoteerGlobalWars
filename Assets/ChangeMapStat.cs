using UnityEngine;
using UnityEngine.UI;

public class ChangeMapStat : MonoBehaviour
{
    public Slider _height, _weight;
    public Toggle _isIslandMod;

    private void Start()
    {
        _height.value = PlayerPrefs.GetFloat("Height");
        _weight.value = PlayerPrefs.GetFloat("Weight");
    }
    private void Update()
    {
       // MapSpawner.Height = (int)_height.value;
       // MapSpawner.Weight = (int)_weight.value;
        ManageMap._isIslandMode = _isIslandMod.isOn;

        PlayerPrefs.SetFloat("Height", _height.value);
        PlayerPrefs.SetFloat("Weight", _weight.value);
    }
}
