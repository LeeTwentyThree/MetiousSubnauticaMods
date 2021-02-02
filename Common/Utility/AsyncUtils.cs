using System.Collections;
using Logger = QModManager.Utility.Logger;
using UnityEngine;
using UWE;
namespace Common.Utility
{
    public static class AsyncUtils
    {
        private static GameObject _prefab;
        private static GameObject _obj;
        public static GameObject GetPrefabForTechTypeAsync(TechType techType)
        {
            CoroutineHost.StartCoroutine(GetPrefabForTechType(techType));

            var obj = _prefab;

            if (obj == null)
                Logger.Log(Logger.Level.Error, "GameObject is null");

            return obj;
        }
        public static GameObject GetPrefabForFileNameAsync(string fileName)
        {
            CoroutineHost.StartCoroutine(GetPrefabForFileName(fileName));

            var obj = _obj;

            if (obj == null)
                Logger.Log(Logger.Level.Error, "GameObject is null");

            return obj;
        }
        private static IEnumerator GetPrefabForTechType(TechType techType)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(techType);
            yield return task;

            GameObject prefab = task.GetResult();

            _prefab = prefab;
            yield break;
        }
        private static IEnumerator GetPrefabForFileName(string path)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabForFilenameAsync(path);
            yield return request;

            request.TryGetPrefab(out GameObject prefab);

            _obj = prefab;

            yield break;
        }
    }
}