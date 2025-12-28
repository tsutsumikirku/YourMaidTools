using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace YourMaidTools
{
    public class YMAnimationPlayer : MonoBehaviour
    {
        [SerializeField] Animation[] animations;
        [SerializeField] private bool playOnStart = true;
        CancellationTokenSource cancellationTokenSource;
        private async UniTask Start()
        {
            if(!playOnStart)return;
            foreach (var anim in animations)
            {
                if(anim.IsPlayAwait)
                await anim.AnimationBase.PlayAnimation(anim.IsPlayAwait);
                else
                anim.AnimationBase.PlayAnimation(true);
            }
        }
        public async UniTask PlayAnimation()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            foreach (var anim in animations)
            {
                if(anim.IsPlayAwait)
                await anim.AnimationBase.PlayAnimation(anim.IsPlayAwait, cancellationTokenSource.Token);
                else
                anim.AnimationBase.PlayAnimation(anim.IsPlayAwait, cancellationTokenSource.Token).Forget();
            }
            
        }
        public async UniTask StopAnimation()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
