using UnityEngine;
using UnityEngine.UI;

public class StarMenu : MonoBehaviour
{
    [SerializeField] GameObject _shipList, _buildingList, _starStatList;
    [SerializeField] Image _shipIcon, _buildIcon, _starIcon;
    public Color NewColor;

    public void ShipListEnable()
    {
        _buildingList.SetActive(false);
        _starStatList.SetActive(false);
        _shipList.SetActive(true);
        _shipIcon.color = NewColor;
        _buildIcon.color = Color.clear;
        _starIcon.color = Color.clear;
    }
    public void BuildingListEnable()
    {
        _starStatList.SetActive(false);
        _shipList.SetActive(false);
        _buildingList.SetActive(true);
        _buildIcon.color = NewColor;
        _shipIcon.color = Color.clear;
        _starIcon.color = Color.clear;
    }
    public void StarStatListEnable()
    {
        _shipList.SetActive(false);
        _buildingList.SetActive(false);
        _starStatList.SetActive(true);
        _starIcon.color = NewColor;
        _buildIcon.color = Color.clear;
        _shipIcon.color = Color.clear;
    }
}
