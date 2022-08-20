
using System.IO;
using UnityEngine;

namespace Lof.Utility
{
    public static class PathUtility
    {
        public static string Combine(string dircetoryName, DirectioryPathEnum root = DirectioryPathEnum.StreamingAssets)
        {
            string path = string.Empty;
            switch (root)
            {
                case DirectioryPathEnum.DataPath:
                    path = Path.Combine(Application.dataPath, dircetoryName);
                    break;
                case DirectioryPathEnum.StreamingAssets:
                    path = Path.Combine(Application.streamingAssetsPath, dircetoryName);
                    break;
                case DirectioryPathEnum.PersistentDataPath:
                    path = Path.Combine(Application.persistentDataPath, dircetoryName);
                    break;
                case DirectioryPathEnum.TemporaryCachePath:
                    path = Path.Combine(Application.temporaryCachePath, dircetoryName);
                    break;
                case DirectioryPathEnum.ConsoleLogPath:
                    path = Path.Combine(Application.consoleLogPath, dircetoryName);
                    break;
            }
            return path;
        }
    }
}

