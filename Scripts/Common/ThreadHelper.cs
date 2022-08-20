using System;
using System.Collections.Generic;

namespace Lof.Common
{
    /// <summary>
    ///  线程助手：把其他线程执行不了的Unity API放到主线程执行
    /// </summary>
    public class ThreadHelper : MonoSingleton<ThreadHelper>
    {
        private class DelayedItem
        {
            public Action CustomAction { get; set; }

            public DateTime ExecuteTime { get; set; }
        }

        private List<DelayedItem> itemList;

        public override void Init()
        {
            base.Init();
            itemList = new List<DelayedItem>();
        }

        private void Update()
        {
            for (int i = itemList.Count - 1; i >= 0; i--)
            {               
                if (itemList[i].ExecuteTime <= DateTime.Now)
                {                    
                    lock (itemList)
                    {
                        itemList[i].CustomAction();
                        itemList.RemoveAt(i);
                    }
                }
            }
        }

        public void ExecuteOnMainThread(Action action, float delay = 0)
        {           
            lock (itemList)
            {
                DelayedItem item = new DelayedItem()
                {
                    CustomAction = action,
                    ExecuteTime = DateTime.Now.AddSeconds(delay)
                };
                itemList.Add(item);
            }
        }
    }
}