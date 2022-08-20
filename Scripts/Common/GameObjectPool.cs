using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Lof.Common
{
    /// <summary>
    ///  游戏对象池
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        /// <summary>
        /// 对象初始化接口
        /// </summary>
        public interface IInit
        {
            void Init();
        }

        /// <summary>
        /// 对象回收接口
        /// </summary>
        public interface ICollect
        {
            void Collect();
        }

        /// <summary>
        /// 对象是否可以使用
        /// </summary>
        public interface IUsable
        {
            bool Usable();
        }

        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 通过对象池创建对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefab"></param>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion dir, Vector3 localScale = default)
        {
            GameObject targetGo = FindUsableObject(key);
            if (targetGo == null)
            {
                targetGo = AddObject(key, prefab);
            }
            UseObject(targetGo, pos, dir, localScale);
            return targetGo;
        }

        private void UseObject(GameObject targetGo, Vector3 pos, Quaternion rot, Vector3 localScale = default)
        {
            targetGo.transform.position = pos;
            targetGo.transform.rotation = rot;
            if (localScale != default)
            {
                targetGo.transform.localScale = localScale;
            }

            targetGo.SetActive(true);

            foreach (var item in targetGo.GetComponents<IInit>())
            {
                item.Init();
            }
        }

        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject targetGo;
            if (!cache.ContainsKey(key))
            {
                cache.Add(key, new List<GameObject>());
            }
            targetGo = Instantiate(prefab);
            cache[key].Add(targetGo);
            return targetGo;
        }

        private GameObject FindUsableObject(string key)
        {
            GameObject targetGo = null;
            if (cache.ContainsKey(key))
            {
                targetGo = cache[key].Find(go =>
                {
                    var usable = go.GetComponent<IUsable>();
                    if (usable != null)
                        return usable.Usable();
                    else//未指定自定义可用逻辑，默认使用是否隐藏来判断是否可用
                        return go.activeInHierarchy == false;

                });
            }
            return targetGo;
        }

        private void CollectObject(GameObject go)
        {
            bool isCollected = false;
            foreach (var item in go.GetComponents<ICollect>())
            {
                item.Collect();
                isCollected = true;
            }

            //没有自定义回收，默认执行隐藏
            if (isCollected) return;
            go.SetActive(false);
        }

        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            CollectObject(go);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delay"></param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            if (delay == 0)
                CollectObject(go);
            else
                StartCoroutine(CollectObjectDelay(go, delay));
        }

        public void Clear(string key)
        {
            foreach (var item in cache[key])
            {
                Destroy(item);
            }
            cache.Remove(key);
        }

        public void ClearAll()
        {
            List<string> keys = new List<string>(cache.Keys);
            foreach (var type in keys)
            {
                Clear(type);
            }
        }

        public void DisposeUnuseGameObject()
        {
            List<string> keys = new List<string>(cache.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                var temp = cache[keys[i]];

                for (int j = temp.Count - 1; j >= 0; j--)
                {
                    var go = temp[j];

                    if (go.GetComponent<IUsable>().Usable())
                    {
                        Destroy(go);
                        temp.RemoveAt(j);
                    }
                }
            }
        }
    }
}