using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InflateAnimation), typeof(Transform))]
public class StarMenu : MonoBehaviour
{
    [SerializeField] private Text _starName;

    [Header("Build Tab")]
    [SerializeField] private Transform _blueprintContainer;
    [SerializeField] private BlueprintRow _blueprintRowPrefab;

    public StarView OpenedStar { get; private set; }

    public bool IsOpened => OpenedStar != null;

    private List<BlueprintRow> _blueprints = new List<BlueprintRow>();
    private Vector3 _menuPositionOffset = new Vector3(100, -50);
    private InflateAnimation _animation;
    private Transform _myTransform;

    public void Reopen(StarView target)
    {
        if (target == OpenedStar) return;

        StopAllCoroutines();
        StartCoroutine(ReopenCoroutine());

        IEnumerator ReopenCoroutine()
        {
            if (IsOpened) yield return CloseCoroutine();
            yield return OpenCoroutine(target);
        }
    }

    public void Open(StarView target)
    {
        if (IsOpened) return;

        StopAllCoroutines();
        StartCoroutine(OpenCoroutine(target));
    }

    public void Close()
    {
        if (!IsOpened) return;

        StopAllCoroutines();
        StartCoroutine(CloseCoroutine());
    }

    private void Start()
    {
        _animation = GetComponent<InflateAnimation>();
        _myTransform = GetComponent<Transform>();
    }

    private IEnumerator OpenCoroutine(StarView target)
    {
        _starName.text = target.Model.Name;
        //SetBlueptintCollection(target.Model.Blueprints);

        MoveToStar(target.transform);
        yield return _animation.OpenCoroutine();
        
        OpenedStar = target;

        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private IEnumerator CloseCoroutine()
    {
        yield return _animation.CloseCoroutine();
        
        _starName.text = "Empty";

        //RemoveBlueprintCollection(OpenedStar.Model.Blueprints);
        OpenedStar = null;
    }

    private void HandleChangeBlueprintList(object sender, NotifyCollectionChangedEventArgs e)
    {
        // TODO: Изменение списка чертежей вместо ReloadBlueprints
        //ReloadBlueprints(OpenedStar.Model.Blueprints);
    }

    /*private void ReloadBlueprints(IEnumerable<Blueprint> blueprints)
    {
        ClearBlueprints();

        foreach (var blueprint in blueprints)
        {
            var row = InstantiateBlueprintRow(blueprint);
            _blueprints.Add(row);
        }
    }*/

    private void ClearBlueprints()
    {
        _blueprints.ForEach(Destroy);
    }

    /*private BlueprintRow InstantiateBlueprintRow(Blueprint blueprint)
    {
        var row = Instantiate(_blueprintRowPrefab, _blueprintContainer);

        // TODO: Инициализировать BlueprintRow
        
        return row;
    }*/

    private void MoveToStar(Transform targetTransform)
    {
        _myTransform.localPosition = targetTransform.localPosition + _menuPositionOffset;
    }

    /*private void SetBlueptintCollection(ObservableCollection<Blueprint> blueprints)
    {
        blueprints.CollectionChanged += HandleChangeBlueprintList;
        //ReloadBlueprints(blueprints);
    }

    private void RemoveBlueprintCollection(ObservableCollection<Blueprint> blueprints)
    {
        blueprints.CollectionChanged -= HandleChangeBlueprintList;
        ClearBlueprints();
    }*/
}
