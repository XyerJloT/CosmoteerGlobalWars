using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour
{
    [SerializeField] private Text _starName;
    [SerializeField] private Transform _blueprintContainer;
    [SerializeField] private BlueprintRow _blueprintRowPrefab;

    private List<BlueprintRow> _blueprints = new List<BlueprintRow>();

    public Star OpenedStar { get; private set; }

    public bool IsOpened => OpenedStar != null;
    
    public void Reopen(Star target)
    {
        if (IsOpened) Close();

        OpenedStar = target;
        OpenedStar.Blueprints.CollectionChanged += HandleChangeBlueprintList;

        _starName.text = OpenedStar.Name;

        ReloadBlueprints(OpenedStar.Blueprints);
    }

    public void Close()
    {
        if (!IsOpened) return;

        OpenedStar.Blueprints.CollectionChanged -= HandleChangeBlueprintList;
        OpenedStar = null;

        _starName.text = "Empty";

        ClearBlueprints();
    }

    private void HandleChangeBlueprintList(object sender, NotifyCollectionChangedEventArgs e)
    {
        // TODO: Изменение списка чертежей вместо ReloadBlueprints
        ReloadBlueprints(OpenedStar.Blueprints);
    }

    private void ReloadBlueprints(IEnumerable<Blueprint> blueprints)
    {
        ClearBlueprints();

        foreach (var blueprint in blueprints)
        {
            var row = InstantiateBlueprintRow(blueprint);
            _blueprints.Add(row);
        }
    }

    private void ClearBlueprints()
    {
        _blueprints.ForEach(Destroy);
    }

    private BlueprintRow InstantiateBlueprintRow(Blueprint blueprint)
    {
        var row = Instantiate(_blueprintRowPrefab, _blueprintContainer);

        // TODO: Инициализировать BlueprintRow
        
        return row;
    }
}
