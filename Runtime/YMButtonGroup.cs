using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEditor.EditorTools;

namespace YourMaidTools
{   
    public class YMButtonGroup : MonoBehaviour
    {
        [Tooltip("初期選択されているボタンのインデックスを指定します。")]
        public int InitialIndex = 0;
        private int currentIndex = -1;
        [Tooltip("ボタンの配列を指定します。")]
        public YMButton[] buttons;
        [Tooltip("Keyで操作するか、UIButtonで操作するかを指定します。")]
        public YMKeyType KeyType = YMKeyType.Key;
        [Tooltip("次のボタンに移動するキーを指定します。")]
        public KeyCode[] nextKey = new KeyCode[] { KeyCode.DownArrow };
        [Tooltip("前のボタンに移動するキーを指定します。")]
        public KeyCode[] previousKey = new KeyCode[] { KeyCode.UpArrow };
        [Tooltip("決定ボタンのキーを指定します。")]
        public KeyCode[] submitKey = new KeyCode[]{KeyCode.Return};
        public int CurrentIndex
        {
            get { return currentIndex; }
            // セットにてアニメーションなどの処理を行いたい
            set
            {
                if (buttons == null || buttons.Length == 0)
                {
                    currentIndex = -1;
                    return;
                }

                int previous = currentIndex;

                // wrap around
                int setCurrentIndex = value;
                if (setCurrentIndex < 0)
                {
                    setCurrentIndex = buttons.Length - 1;
                }
                else if (setCurrentIndex >= buttons.Length)
                {
                    setCurrentIndex = 0;
                }

                // If previous is valid, play exit animation
                if (previous >= 0 && previous < buttons.Length && previous != setCurrentIndex && buttons[previous] != null)
                {
                    buttons[previous].OnPointerExitAnimation().Forget();
                }

                currentIndex = setCurrentIndex;

                if (currentIndex >= 0 && currentIndex < buttons.Length && buttons[currentIndex] != null)
                {
                    buttons[currentIndex].OnPointerEnterAnimation().Forget();
                }
            }
        }
        private void Awake()
        {
            if (buttons == null || buttons.Length == 0)
            {
                Debug.LogWarning("YMButtonGroup: buttons が未設定です。");
                return;
            }

            if (InitialIndex < 0 || InitialIndex >= buttons.Length)
            {
                Debug.LogWarning("InitialIndex が buttons の範囲外です。0 に丸めます。");
                InitialIndex = 0;
            }

            currentIndex = InitialIndex;
            if (buttons[currentIndex] != null)
            {
                Debug.Log("YMButtonGroup: 初期インデックス " + InitialIndex + " のボタンを選択します。");
                buttons[currentIndex].OnPointerEnterAnimation().Forget();
            }
        }
        private void Update()
        {
            if (buttons == null || buttons.Length == 0) return;

            foreach (var nextKeyCode in nextKey)
            {
                if (Input.GetKeyDown(nextKeyCode))
                {
                    CurrentIndex++;
                    break;
                }
            }
            foreach (var previousKeyCode in previousKey)
            {
                if (Input.GetKeyDown(previousKeyCode))
                {
                    CurrentIndex--;
                    break;
                }
            }
            foreach (var submitKeyCode in submitKey)
            {
                if (Input.GetKeyDown(submitKeyCode))
                {
                    if (currentIndex >= 0 && currentIndex < buttons.Length && buttons[currentIndex] != null)
                    {
                        buttons[currentIndex].OnSelect();
                    }
                    break;
                } 
            }
        }
    }
    public enum YMKeyType
    {
        Key,
        UIButton
    }
}
