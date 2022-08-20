using UnityEngine;
using System;

namespace Lof.Common
{
    /// <summary>
    /// 动画事件行为类:根据需要添加自己的需求
    /// </summary>
    public class AnimationEvent : MonoBehaviour
    {
        /// <summary>
        /// 开始播放动画的特定时间
        /// </summary>
        public event Action OnStartPlayEvent;
        /// <summary>
        /// 通用事件
        /// </summary>
        public event Action OnCommonEvent;
        /// <summary>
        /// 停止播放动画事件
        /// </summary>
        public event Action OnStopPlayEvent;
        /// <summary>
        /// 动画播放完成的事件
        /// </summary>
        public event Action OnEndPlayEvent;

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void StartPlay()
        {
            OnStartPlayEvent?.Invoke();
        }

        private void StopPlay(string aniParam)
        {
            anim.SetBool(aniParam, false);
            OnStopPlayEvent?.Invoke();
        }

        //通用事件：比如播放到某一帧，播放声音、攻击等，就可以把Common改下名字
        private void Common()
        {
            if (OnCommonEvent != null)
            {
                OnCommonEvent();
            }
        }

        private void EndPlay()
        {
            OnEndPlayEvent?.Invoke();
        }
    }
}
