using UnityEngine;
using UnityEngine.UI;
namespace YourMaidTools
{
    public class YMPictureChangeAnimation : YMAnimationBase
    {
        [SerializeField] private float changeAlpha = 0.5f;
        [SerializeField] private Sprite changeSprite;
        private Image selfImage;
        private Image image;
        private void Awake()
        {
            selfImage = GetComponent<Image>();
            var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            ((RectTransform)go.transform).sizeDelta = ((RectTransform)transform).sizeDelta;
            ((RectTransform)go.transform).anchoredPosition = Vector2.zero;
            go.transform.SetParent(transform, false);
            image = go.GetComponent<Image>();
            var color = Color.white;
            color.a = 0f;
            image.color = color;
            image.sprite = changeSprite;
        }
        public override void PlayingAnimation(float progress)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(0f, changeAlpha, progress);
            image.color = color;
            Color selfColor = selfImage.color;
            selfColor.a = Mathf.Lerp(1f, 1f - changeAlpha, progress);
            selfImage.color = selfColor;
        }
    }
}
