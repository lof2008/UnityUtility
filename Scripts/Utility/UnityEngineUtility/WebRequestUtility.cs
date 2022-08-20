using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Lof.Utility.UnityEngineUtility
{
    public partial class WebRequestUtility
    {
        private static MonoHelper mono = default;

        static WebRequestUtility()
        {
            mono = MonoHelper.I;
        }

        public static void GetString(string url, Action<string> callback)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("GetString request url is null!");

            mono.StartCoroutine(GetRequestString(url, callback));
        }

        public static void GetBytes(string url, Action<byte[]> callback)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("GetBytes url is null!");
            mono.StartCoroutine(GetRequestBytes(url, callback));
        }

        public static void PostString(string url, string data, Action<string> callback)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("PostString url is null!");

            if (string.IsNullOrEmpty(data))
                throw new Exception("PostString data is null!");

            mono.StartCoroutine(PostRequestString(url, data, callback));
        }

        private static IEnumerator GetRequestString(string url, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            //2020�°�ͨ��result�ж�
            if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.isDone)
            {
                callback?.Invoke(request.downloadHandler.text);
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new Exception("GetRequestString have error!");
            }
        }

        private static IEnumerator GetRequestBytes(string url, Action<byte[]> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            //2020�°�ͨ��result�ж�
            if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.isDone)
            {
                callback?.Invoke(request.downloadHandler.data);
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new Exception("GetRequestBytes have error!");
            }
        }

        private static IEnumerator PostRequestString(string url, string data, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Post(url, data);
            yield return request.SendWebRequest();

            //2020�°�ͨ��result�ж�
            if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.isDone)
            {
                callback?.Invoke(request.downloadHandler.text);
            }
            else if (request.result == UnityWebRequest.Result.ConnectionError ||
                 request.result == UnityWebRequest.Result.ProtocolError ||
                 request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new Exception("PostRequestString have error!");
            }
        }
    }
}

