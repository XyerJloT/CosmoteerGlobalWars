using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BlueprintView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private InputField _name;
    [SerializeField] private NumberField _cost;

    public Blueprint Blueprint { get; set; }
    public string Name { get => _name.text; set => _name.text = value; }
    public int Cost { get => _cost.Number; set => _cost.Number = value; }

    public event Action<Blueprint> OnBlueprintChanged;

    private List<ShipTypeData> _matchersCopy;
    private Config _config;

    private void Start()
    {
        _config = FindObjectOfType<Config>();
        _matchersCopy = new List<ShipTypeData>(_config.SortedMatchers);

        Blueprint = new Blueprint(_name.text, default(Ship.RankType), 0);
        SetBlueprintCost(_cost.Number);

        _name.onValueChanged.AddListener(SetBlueprintName);
        _cost.OnChanged.AddListener(SetBlueprintCost);

        Debug.Log(_config);
        Debug.Log(_matchersCopy);
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            _matchersCopy = new List<ShipTypeData>(_config.SortedMatchers);
        }
    }

    private void OnDestroy()
    {
        _name.onValueChanged.RemoveListener(SetBlueprintName);
        _cost.OnChanged.RemoveListener(SetBlueprintCost);
    }

    private void SetBlueprintName(string name)
    {
        Blueprint = new Blueprint(name, Blueprint.Type, Blueprint.Cost);
        
        OnBlueprintChanged?.Invoke(Blueprint);
    }

    private void SetBlueprintCost(int cost)
    {
        Debug.Log("Hi");

        var typeData = FindTypeData(cost);

        Blueprint = new Blueprint(Blueprint.Name, typeData.Type, cost);
        _icon.sprite = typeData.Sprite;

        OnBlueprintChanged?.Invoke(Blueprint);
    }

    private ShipTypeData FindTypeData(int cost)
    {
        return _matchersCopy.Find(m => m.Cost > cost);
    }
}
