using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Lof.Utility
{
    public static class FileUtility
    {
        public static string ReadAllTextWithoutCreate(string fileName, string defaultValue = null, DirectioryPathEnum root = DirectioryPathEnum.StreamingAssets)
        {
            if (string.IsNullOrEmpty(fileName.Trim())) return null;

            string path = PathUtility.Combine(fileName, root);
            return ReadAllText(path, defaultValue);
        }

        public static IEnumerable<string> ReadAllLinesWithoutCreate(string fileName, IEnumerable<string> defaultValue = null, DirectioryPathEnum root = DirectioryPathEnum.StreamingAssets)
        {
            if (string.IsNullOrEmpty(fileName.Trim())) return null;

            string path = PathUtility.Combine(fileName, root);
            return ReadAllLines(path, defaultValue);
        }

        public static byte[] ReadAllBytesWithoutCreate(string fileName, byte[] defaultValue = null, DirectioryPathEnum root = DirectioryPathEnum.StreamingAssets)
        {
            if (string.IsNullOrEmpty(fileName.Trim())) return null;

            string path = PathUtility.Combine(fileName, root);
            return ReadAllBytes(path, defaultValue);
        }

        public static string ReadAllText(string path, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(path.Trim())) return null;

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                try
                {
                    File.WriteAllText(path, defaultValue);
                }
                catch (Exception e)
                {
                    Debug.Log("创建文件异常：" + e.Message + "File Path:" + path);
                }
                return defaultValue;
            }
        }

        public static IEnumerable<string> ReadAllLines(string path, IEnumerable<string> defaultValue = null)
        {
            if (string.IsNullOrEmpty(path.Trim())) return null;

            if (File.Exists(path))
            {
                return File.ReadAllLines(path);
            }
            else
            {
                try
                {
                    File.WriteAllLines(path, defaultValue);
                }
                catch (Exception e)
                {
                    Debug.Log("创建文件异常：" + e.Message + "File Path:" + path);
                }
                return defaultValue;
            }
        }

        public static byte[] ReadAllBytes(string path, byte[] defaultValue = null)
        {
            if (string.IsNullOrEmpty(path.Trim())) return null;

            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                try
                {
                    File.WriteAllBytes(path, defaultValue);
                }
                catch (Exception e)
                {
                    Debug.Log("创建文件异常：" + e.Message + "File Path:" + path);
                }

                return defaultValue;
            }
        }
    }
}