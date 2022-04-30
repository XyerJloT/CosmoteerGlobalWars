using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class CoroutineExecuter : IExecuter
    {
        private readonly MonoBehaviour _component;

        public CoroutineExecuter()
        {
            var obj = new GameObject("LibraryHelperDontDestroy");
            UnityEngine.Object.DontDestroyOnLoad(obj);
            _component = obj.AddComponent<Helper>();
        }

        public void Execute(Action callback)
        {
            _component.StartCoroutine(WrapInCoroutine(callback));
        }

        private IEnumerator WrapInCoroutine(Action callback)
        {
            yield return null;
            callback();
        }

        private class Helper : MonoBehaviour { }
    }
}
