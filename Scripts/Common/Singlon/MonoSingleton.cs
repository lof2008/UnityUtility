using UnityEngine;

namespace Lof.Common
{
    /// <summary>
    ///  脚本单例类
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static object locker = new object();
       
        private static T instance;
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
                            new GameObject("MonoSinglton_" + typeof(T)).AddComponent<T>();//立即执行Awake
                        }
                        else
                        {
                            instance.Init();
                        }
                    }
                }
                return instance;
            }
        }

        [Tooltip("跨场景不销毁")]
        public bool isDontDestroy;

        //如果自行在场景中附加了T类型对象，则在Awake中为其赋值
        protected void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }

        //需求：实现类需要在Instance属性赋值时，进行初始化。
        //情景1：自行附加到游戏对象,通过Awake进行初始化。
        //如果Instance属性先执行
        //   情景2：附加到游戏对象，代码28行进行初始化
        //   情景3：没有附加到游戏对象，代码24行会执行Awake进行初始化
        public virtual void Init()
        {
            if (isDontDestroy)
                DontDestroyOnLoad(gameObject);
        }
    }
}