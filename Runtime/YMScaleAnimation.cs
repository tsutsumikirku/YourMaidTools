using UnityEngine;

namespace YourMaidTools
{
    public class YMScaleAnimation : YMAnimationBase
    {
        [SerializeField] private float changeScale = 1.2f;
        Vector2 defaultScale;
        private void Awake()
        {
            defaultScale = transform.localScale;
        }
        public override void PlayingAnimation(float progress)
        {
            float scale = Mathf.Lerp(1f, changeScale, progress);
            transform.localScale = defaultScale * scale;
        }
    }
}
