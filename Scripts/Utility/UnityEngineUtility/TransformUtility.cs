using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lof.Utility.UnityEngineUtility
{
    public static class TransformUtility
    {
        /// <summary>
        /// 通过查找孩子
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            if (string.IsNullOrEmpty(childName))
                throw new Exception("childName is null!");

            Transform childTF = currentTF.Find(childName);
            if (childTF != null) return childTF;

            for (int i = 0; i < currentTF.childCount; i++)
            {
                childTF = FindChildByName(currentTF.GetChild(i), childName);
                if (childTF != null) return childTF;
            }
            return null;
        }

        /// <summary>
        /// 平滑旋转看向到目标方向
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="dir">方向</param>
        /// <param name="rotateSpeed"></param>
        public static void LookAtDirection(this Transform currentTF, Vector3 dir, float rotateSpeed)
        {
            if (dir == Vector3.zero) return;

            if (rotateSpeed == 0) return;

            Quaternion targetDir = Quaternion.LookRotation(dir);

            currentTF.rotation = Quaternion.Slerp(currentTF.rotation, targetDir, Time.deltaTime * rotateSpeed);
        }

        /// <summary>
        /// 平滑旋转看向到目标位置
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="pos"></param>
        /// <param name="rotateSpeed"></param>
        public static void LookAtPosition(this Transform currentTF, Vector3 pos, float rotateSpeed)
        {
            Vector3 dir = pos - currentTF.position;
            currentTF.LookAtDirection(dir, rotateSpeed);
        }

        /// <summary>
        /// 计算周边区域的对象
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="targetTags">目标Tag</param>
        /// <param name="distance">距离</param>
        /// <param name="angle">角度：360度是球形区域，180是自身方向的半球区域</param>
        /// <returns></returns>
        public static Transform[] CalculateAroundObject(this Transform currentTF, string[] targetTags, float distance, float angle = 360)
        {
            List<Transform> tempTfList = new List<Transform>();
            for (int i = 0; i < targetTags.Length; i++)
            {
                GameObject[] tempGoArr = GameObject.FindGameObjectsWithTag(targetTags[i]);
                Transform[] tempTfArr = tempGoArr.Select(go => go.transform);
                tempTfList.AddRange(tempTfArr);
            }

            tempTfList = tempTfList.FindAll(tf =>
                Vector3.Distance(currentTF.position, tf.position) <= distance &&
                Vector3.Angle(currentTF.forward, tf.position - currentTF.position) <= angle / 2
            );
            return tempTfList.ToArray();
        }
    }
}