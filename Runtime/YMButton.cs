using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace YourMaidTools
{
    public class YMButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("ボタンが上下どちらの種類かを指定します。")]
        public YMButtonType ButtonType;
        [Tooltip("クリックされたときに呼ばれるイベントを指定します。")]
        public UnityEvent OnClick;
        [Tooltip("マウスカーソルがボタンの上にあるときに呼ばれるアニメーションを指定します。")]
        public Animation[] PointerEnterAnimation;
        [Tooltip("クリックされたときに呼ばれるアニメーションを指定します。")]
        public Animation[] OnClickAnimations;
        public Action OnClickAction;
        private bool isPointerInside = false;
        private CancellationTokenSource cancellationToken;
        public void OnPointerDown(PointerEventData eventData)
        {
            cancellationToken?.Cancel();
            cancellationToken?.Dispose();
            cancellationToken = new CancellationTokenSource();
            if (ButtonType == YMButtonType.Donw)
            {
                OnClick?.Invoke();
                OnClickAction?.Invoke();
            }
            PlayAnimations();
        }
        private async void PlayAnimations()
        {
            if (OnClickAnimations == null) return;
            try
            {
                foreach (var animation in OnClickAnimations)
                {
                    if (animation == null || animation.AnimationBase == null) continue;
                    try
                    {
                        if(animation.IsPlayAwait)
                        await animation.AnimationBase.PlayAnimation(animation.IsPlayAwait, cancellationToken?.Token ?? CancellationToken.None);
                        else
                        animation.AnimationBase.PlayAnimation(animation.IsPlayAwait, cancellationToken?.Token ?? CancellationToken.None).Forget();
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected when cancelled; stop playing further animations
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        private async void ReverseAnimations()
        {
            if (OnClickAnimations == null) return;
            try
            {
                foreach (var animation in OnClickAnimations)
                {
                    if (animation == null || animation.AnimationBase == null) continue;
                    try
                    {
                        await animation.AnimationBase.ReverseAnimation(animation.IsPlayAwait, cancellationToken?.Token ?? CancellationToken.None);
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected when cancelled; stop reversing further animations
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerInside = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerInside = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            cancellationToken?.Cancel();
            cancellationToken?.Dispose();
            cancellationToken = new CancellationTokenSource();
            if (ButtonType == YMButtonType.Up && isPointerInside)
            {
                OnClick?.Invoke();
                OnClickAction?.Invoke();
            }
            ReverseAnimations();
        }
        void OnDisable()
        {
            cancellationToken?.Cancel();
            cancellationToken?.Dispose();
            cancellationToken = null;
            foreach (var animation in OnClickAnimations)
            {
                animation.AnimationBase.Init();
            }
        }
    }
    public enum YMButtonType
    {
        [InspectorName("ボタン押下時")]
        Donw,
        [InspectorName("ボタン離上時")]
        Up
    }
    [System.Serializable]
    public class Animation
    {
        public YMAnimationBase AnimationBase;
        public bool IsPlayAwait;
    }
}
