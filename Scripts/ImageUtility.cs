using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Lof.Utility.Utility
{
    public class ImageUtility
    {
        class ImageLoadMono : MonoBehaviour { }

        private static ImageLoadMono mono;

        static ImageUtility()
        {
            mono = new GameObject("ImageLoad").AddComponent<ImageLoadMono>();
            UnityEngine.Object.DontDestroyOnLoad(mono.gameObject);
        }

        public static void Load(string path, Action<string, Texture2D> callback)
        {
            if (!File.Exists(path))
            {
                Debug.Log("文件路径不存在：" + path);
            }
            mono.StartCoroutine(LoadImage(path, callback));
        }

        private static IEnumerator LoadImage(string path, Action<string, Texture2D> callback)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);
            yield return request.SendWebRequest();
            if (request.downloadHandler.isDone)
            {
                var tex = DownloadHandlerTexture.GetContent(request);
                var fileName = Path.GetFileNameWithoutExtension(path);
                callback?.Invoke(fileName, tex);
            }
        }
    }
}
