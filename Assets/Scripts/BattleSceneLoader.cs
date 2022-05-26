using Assets.Scripts.Library;
using Assets.Scripts.Map;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using Assets.Scripts.Serialization.Map;

public class BattleSceneLoader : MonoBehaviour
{
    [SerializeField] private LibraryView _libraryView;
    [SerializeField] private SpaceMapView _mapView;

    private LibrarySerializer _librarySerializer;
    private Library _library;
    private LibraryPresenter _presenter;

    private string _mapFile;
    private SpaceMap _map;

    private void Awake()
    {
        Debug.Log("Init battle scene...");

        InitLibrary();

        Debug.Log("Load map...");
        Debug.Log($"Map will be saved in '{_mapFile}'");
        _mapFile = Path.Combine(Application.persistentDataPath, "map.json");
        _map = LoadMap(_mapFile);
        _mapView.Init(_map);
    }

    private void OnDestroy()
    {
        _presenter.Dispose();
        _librarySerializer.Save(_library);

        SaveMap(_mapFile, _map);
    }

    private void InitLibrary()
    {
        Debug.Log("Load library...");
        string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string folderWithBlueprints = Path.Combine(myDocumentsFolder, "My Games", "Cosmoteer", "Saved Ships");
        string saveFile = Path.Combine(Application.persistentDataPath, "library.json");
        Debug.Log($"Library will be saved in '{saveFile}'");

        _librarySerializer = new LibrarySerializer(saveFile, folderWithBlueprints, new DefaultRankMatcher());
        _library = _librarySerializer.Load();

        _libraryView.Init(new CachedImageSource());
        _libraryView.Comparer = new BlueprintComparer();

        _presenter = new LibraryPresenter(_library, _libraryView);
    }

    private SpaceMap GenerateMap()
    {
        var starDad = new Star("Папа", 50_000, new Vector2(50, 50));
        var starMum = new Star("Мама", 40_000, new Vector2(0, 0));
        var tunnel = new Tunnel(starDad, starMum);

        return new SpaceMap(new[] { tunnel });
    }

    private SpaceMap LoadMap(string file)
    {
        if (!File.Exists(file)) return new SpaceMap(Enumerable.Empty<Tunnel>());

        var json = File.ReadAllText(file);
        return new MapSerializer().Deserialize(json);
    }

    private void SaveMap(string file, SpaceMap map)
    {
        var json = new MapSerializer().Serialize(map);
        File.WriteAllText(file, json);
    }

    private class BlueprintComparer : IComparer<IImmutableBlueprint>
    {
        public int Compare(IImmutableBlueprint x, IImmutableBlueprint y)
        {
            if (x == null && y == null)
            {
                Debug.LogWarning("All is null!");
                return 0;
            }
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Name.CompareTo(y.Name);
        }
    }
}
