using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using NUnit.Framework;

namespace YourMaidTools
{
    /// <summary>
    /// YMAnimationBase
    /// アニメーションの基本設定を行うベースクラスです。
    /// </summary>
    public abstract class YMAnimationBase : MonoBehaviour
    {
        public float Duration = 1f;
        public YMAnimationType AnimationType = YMAnimationType.Ease;
        public AnimationCurve AnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public Ease EaseType = Ease.Linear;
        public bool isLoop = false;
        private float progress = 0f;
        private Tween tween;
        public async UniTask PlayAnimation(bool waitForCompletion = false, CancellationToken cancellationToken = default)
        {
            if (isLoop)
            {
                while(cancellationToken.IsCancellationRequested == false)
                {
                    tween?.Kill();
                    switch (AnimationType)
                    {
                        case YMAnimationType.Ease:
                            // getter, setter, endValue, duration
                            tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 1f, Duration).SetEase(EaseType);
                            break;
                        case YMAnimationType.AnimationCurve:
                            tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 1f, Duration).SetEase(AnimationCurve);
                            break;
                    }
                    await WaitEndOfTween(cancellationToken);
                    tween?.Kill();
                    switch (AnimationType)
                    {
                        case YMAnimationType.Ease:
                            tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 0f, Duration).SetEase(EaseType);
                            break;
                        case YMAnimationType.AnimationCurve:
                            tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 0f, Duration).SetEase(AnimationCurve);
                            break;
                    }
                    await WaitEndOfTween(cancellationToken);
                }
                return;
            }
            tween?.Kill();
            switch (AnimationType)
            {
                case YMAnimationType.Ease:
                    // getter, setter, endValue, duration
                    tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 1f, Duration).SetEase(EaseType);
                    break;
                case YMAnimationType.AnimationCurve:
                    tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 1f, Duration).SetEase(AnimationCurve);
                    break;
            }
            if (waitForCompletion)
            {
                await WaitEndOfTween(cancellationToken);
            }
        }
        public async UniTask ReverseAnimation(bool waitForCompletion = false, CancellationToken cancellationToken = default)
        {
            tween?.Kill();
            switch (AnimationType)
            {
                case YMAnimationType.Ease:
                    tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 0f, Duration).SetEase(EaseType);
                    break;
                case YMAnimationType.AnimationCurve:
                    tween = DOTween.To(() => progress, x => { progress = x; PlayingAnimation(x); }, 0f, Duration).SetEase(AnimationCurve);
                    break;
            }
            if (waitForCompletion)
            {
                await WaitEndOfTween(cancellationToken);
            }
        }
        public void Init()
        {
            tween?.Kill();
            PlayingAnimation(0f);
        }
        private async UniTask WaitEndOfTween(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => tween == null || !tween.IsActive() || !tween.IsPlaying(), cancellationToken: cancellationToken);
        }
        public abstract void PlayingAnimation(float progress);
    }
    public enum YMAnimationType
    {
        Ease,
        AnimationCurve,
    }

}
