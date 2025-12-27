using UnityEngine;
using TMPro;

namespace YourMaidTools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class YMTextScaleAnimation : YMAnimationBase
    {
        [SerializeField] private float changeScale = 1.2f;
        private float defaultFontSize;
        private TextMeshProUGUI textMeshProUGUI;
        
        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            defaultFontSize = textMeshProUGUI.fontSize;
        }
        public override void PlayingAnimation(float progress)
        {
            float scale = Mathf.Lerp(1f, changeScale, progress);
            textMeshProUGUI.fontSize = defaultFontSize * scale;
        }
    }
}
