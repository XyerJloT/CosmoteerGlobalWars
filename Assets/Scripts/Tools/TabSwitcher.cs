using System;
using UnityEngine;

[ExecuteInEditMode]
public class TabSwitcher : MonoBehaviour
{
    [SerializeField] private int _defaultActiveTab;
    [SerializeField] private GameObject[] _tabContainers;

    public GameObject ActiveTab => _tabContainers[ActiveTabIndex];

    public int ActiveTabIndex
    {
        get => _activeTab;
        set
        {
            if (!(0 <= value && value <= _tabContainers.Length))
            {
                throw new ArgumentOutOfRangeException($"Всего {_tabContainers.Length} вкладок, а запросили вкладку {value}");
            }

            _tabContainers[_activeTab].SetActive(false); // Закрываем текущую вкладку
            _tabContainers[value].SetActive(true); // Открываем новую

            _activeTab = value;
        }
    }

    private int _activeTab = 0;

    private void Start()
    {
        foreach (var tab in _tabContainers)
        {
            tab.SetActive(false);
        }

        ActiveTabIndex = _defaultActiveTab;
    }

    private void Update()
    {
        // Обновление в редакторе
        if (!Application.isPlaying)
        {
            ActiveTabIndex = _defaultActiveTab;
        }
    }
}
