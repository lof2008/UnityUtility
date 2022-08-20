using System;
using System.IO;
using UnityEngine;

namespace Lof.Utility
{
    public static class DirectoryUtility
    {
        public static string GetDirectoryPath(string dircetoryName, DirectioryPathEnum root = DirectioryPathEnum.StreamingAssets)
        {
            if (string.IsNullOrEmpty(dircetoryName.Trim())) return null;

            string path = PathUtility.Combine(dircetoryName, root);
            CreateDircetory(path);
            return path;
        }

        private static void CreateDircetory(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Debug.Log("�����ļ����쳣��" + e.Message + "Directory Path:" + path);
                }
            }
        }
    }
}