using UnityEngine;
using UnityEngine.UI;

public class ShipIcon : MonoBehaviour
{
    [SerializeField] Toggle[] icons;
    bool _alreadyClicked;

    public void OnClicked()    
    {
      // foreach (var item in icons)
     //  {
     //     item.isOn = false;
     //  }
      // gameObject.GetComponent<Toggle>().isOn = true;   
    }
}
