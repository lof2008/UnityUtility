using System;
using System.Collections.Generic;

namespace Lof.Utility
{   
    public static class ArrayUtility
    {
        /// <summary>
        /// 查找满足条件的所有对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 查找满足条件的首个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取满足条件的最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMax<T, TKey>(this T[] array, Func<T, TKey> condition) where TKey : IComparable
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(max).CompareTo(condition(array[i])) < 0)
                {
                    max = array[i];
                }
            }
            return max;
        }

        /// <summary>
        /// 获取满足条件的最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMin<T, TKey>(this T[] array, Func<T, TKey> condition) where TKey : IComparable
        {
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(min).CompareTo(condition(array[i])) > 0)
                {
                    min = array[i];
                }
            }
            return min;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition">第一个参数大于第二个参数返回true：升序，第一个参数小于第二个参数返回true：降序</param>
        public static void Sort<T>(this T[] array, Func<T, T, bool> condition)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (condition(array[j], array[j + 1]))
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void OrderBy<T>(this T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) == 1)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void OrderByDescending<T>(this T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) == -1)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="arrayTF"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Q[] Select<T, Q>(this T[] arrayTF, Func<T, Q> handler)
        {
            Q[] arrayGO = new Q[arrayTF.Length];
            for (int i = 0; i < arrayGO.Length; i++)
            {
                arrayGO[i] = handler(arrayTF[i]);
            }
            return arrayGO;
        }
    }
}
