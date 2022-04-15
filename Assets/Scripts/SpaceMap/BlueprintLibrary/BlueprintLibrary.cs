using System;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintLibrary : MonoBehaviour
{
    [SerializeField] private Transform _blueprintContainer;
    [SerializeField] private BlueprintView _blueprintPrefab;
    [SerializeField] private GameObject[] _displayedContent;

    public readonly List<BlueprintView> Blueprints = new List<BlueprintView>();

    public bool IsActiveContent { get; private set; }

    public event Action<Blueprint> OnAddBlueprint;
    public event Action<Blueprint> OnRemoveBlueprint;

    public void ToggleDisplay()
    {
        SetActiveContent(!IsActiveContent);
    }

    public void SetActiveContent(bool isActive)
    {
        foreach (var obj in _displayedContent)
        {
            obj.SetActive(isActive);
        }
        IsActiveContent = isActive;
    }

    public void AddEmptyBlueprint()
    {
        var view = Instantiate(_blueprintPrefab, _blueprintContainer);

        Blueprints.Add(view);

        OnAddBlueprint?.Invoke(view.Blueprint);
    }

    public void RemoveBlueprint(BlueprintView obj)
    {
        if (!Blueprints.Contains(obj))
        {
            throw new ArgumentException("ѕопытались удалить не пренадлежащий чертеж");
        }

        Blueprints.Remove(obj);
        
        OnRemoveBlueprint?.Invoke(obj.Blueprint);

        Destroy(obj.gameObject);
    }

    private void Start()
    {
        SetActiveContent(false);
    }
}
