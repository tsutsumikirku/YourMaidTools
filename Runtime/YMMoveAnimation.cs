using UnityEngine;
using YourMaidTools;

namespace YourMaidTools
{
    public class YMMoveAnimation : YMAnimationBase
    {
        [SerializeField] private Vector3 moveOffset = Vector3.zero;
        private Vector3 defaultPosition;

        private void Awake()
        {
            defaultPosition = transform.localPosition;
        }

        public override void PlayingAnimation(float progress)
        {
            Vector3 newPosition = Vector3.Lerp(defaultPosition, defaultPosition + moveOffset, progress);
            transform.localPosition = newPosition;
        }
    }
}