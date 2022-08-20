using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Lof.Utility.UnityEngineUtility
{
    /// <summary>
    ///  配置文件读取器
    /// </summary>
    public class ConfigurationReader
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetConfigFile(string fileName)
        {
            string path;
#if UNITY_EDITOR || UNITY_STANDALONE
            path = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
            if (File.Exists(path))            
                return File.ReadAllText(path);           
            else            
                throw new Exception("filename is null!");
#elif UNITY_IPHONE
            path = "file://" + Application.dataPath + "/Raw/" + fileName;
#elif UNITY_ANDROID
           path = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#endif 
            //安卓平台不能使用File进行读取，因为File读取要求明确的路径，在安卓平台下，
            //文件在压缩包里，所以用UnityWebRequest或WWW，IOS不清楚，稳妥起见还是不用File
            UnityWebRequest request = UnityWebRequest.Get(path);
            request.SendWebRequest();
            while (true)
            {
                if (request.downloadHandler.isDone)
                    return request.downloadHandler.text;
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="handle">解析逻辑</param>
        public static void LoadConfigFile(string configFile, Action<string> handle)
        {
            using (StringReader reader = new StringReader(configFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    handle(line);
                }
            }
        }
    }
}