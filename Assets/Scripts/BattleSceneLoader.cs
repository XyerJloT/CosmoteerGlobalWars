using Assets.Scripts.Library;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleSceneLoader : MonoBehaviour
{
    [SerializeField] private LibraryView _libraryView;

    private LibrarySerializer _librarySerializer;
    private Library _library;
    private LibraryPresenter _presenter;

    private void Start()
    {
        Debug.Log("Init battle scene...");

        Debug.Log("Load library...");
        string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string libraryFodler = Path.Combine(myDocumentsFolder, "My Games", "Cosmoteer", "Saved Ships");
        string librarySaveFile = Path.Combine(Application.persistentDataPath, "library.json");
        Debug.Log($"Library will be saved in '{librarySaveFile}'");

        _librarySerializer = new LibrarySerializer(librarySaveFile, libraryFodler, new DefaultRankMatcher());
        _library = _librarySerializer.Load();

        _libraryView.Init(new CachedImageSource());
        _libraryView.Comparer = new BlueprintComparer();

        _presenter = new LibraryPresenter(_library, _libraryView);
    }

    private void OnDestroy()
    {
        _presenter.Dispose();
        _librarySerializer.Save(_library);
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
