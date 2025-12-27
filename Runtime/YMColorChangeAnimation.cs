using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YourMaidTools
{
    public class YMColorChangeAnimation : YMAnimationBase
    {
        [SerializeField] private Color changeColor = Color.white;
        private Color defaultColor;
        private Image image;
        private TextMeshProUGUI textMeshProUGUI;
        private void Awake()
        {
            image = GetComponent<Image>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            if (image != null)
            {
                defaultColor = image.color;
            }
            else if (textMeshProUGUI != null)
            {
                defaultColor = textMeshProUGUI.color;
            }
        }
        public override void PlayingAnimation(float progress)
        {
            Debug.Log($"progress={progress} image={(image!=null)} tmp={(textMeshProUGUI!=null)}");
            Color color = Color.Lerp(defaultColor, changeColor, progress);
            if (image != null)
            {
                image.color = color;
            }
            else if (textMeshProUGUI != null)
            {
                textMeshProUGUI.color = color;
            }
        }
    }
}