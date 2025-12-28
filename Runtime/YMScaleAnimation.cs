using UnityEngine;

namespace YourMaidTools
{
    public class YMScaleAnimation : YMAnimationBase
    {
        [SerializeField] private float changeScale = 1.2f;
        RectTransform rectTransform;
        Vector2 defaultScale;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if(rectTransform != null)
            {
                defaultScale = rectTransform.sizeDelta;
                return;
            }
            defaultScale = transform.localScale;
        }
        public override void PlayingAnimation(float progress)
        {
            float scale;
            if(rectTransform != null)
            {
                scale = Mathf.Lerp(1f, changeScale, progress);
                rectTransform.sizeDelta = defaultScale * scale;
                return;
            }
            scale = Mathf.Lerp(1f, changeScale, progress);
            transform.localScale = defaultScale * scale;
        }
    }
}
