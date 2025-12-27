using UnityEngine;


namespace YourMaidTools
{
    /// <summary>
    /// Safe Area Utility
    /// セーフエリアをセッティングできるユーティリティクラスCanvas内のエンプティオブジェクトにアタッチして使用します。
    /// Editorでも実行できるように`[ExecuteAlways]`を付け、エディタ用の切替を追加しています。
    /// </summary>
    [ExecuteAlways]
    public class YMSafeArea : MonoBehaviour
    {
        [SerializeField]
        private bool applyInEditMode = true;

        private RectTransform rectTransform;
        private Rect lastSafeArea = new Rect(0, 0, 0, 0);
        private Vector2 lastScreenSize = Vector2.zero;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void OnEnable()
        {
            rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void Update()
        {
            if (!Application.isPlaying && !applyInEditMode) return;

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Rect deviceSafeArea = Screen.safeArea;

            if (lastScreenSize != screenSize || lastSafeArea != deviceSafeArea)
            {
                ApplySafeArea();
            }
        }

        [ContextMenu("Apply Safe Area")]
        private void ApplySafeArea()
        {
            if (!Application.isPlaying && !applyInEditMode) return;
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null) return;

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Rect deviceSafeArea = Screen.safeArea;

            if (screenSize.x <= 0 || screenSize.y <= 0) return;

            Vector2 anchorMin = deviceSafeArea.position;
            Vector2 anchorMax = deviceSafeArea.position + deviceSafeArea.size;

            anchorMin.x /= screenSize.x;
            anchorMax.x /= screenSize.x;
            anchorMin.y /= screenSize.y;
            anchorMax.y /= screenSize.y;

            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            lastScreenSize = screenSize;
            lastSafeArea = deviceSafeArea;
        }
    }
}