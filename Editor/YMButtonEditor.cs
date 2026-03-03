using UnityEditor;
using UnityEngine;

namespace YourMaidTools
{
    [CustomEditor(typeof(YMButton))]
    public class YMButtonEditor : Editor
    {
        SerializedProperty buttonTypeProp;
        SerializedProperty selectTypeProp;
        SerializedProperty onClickProp;
        SerializedProperty selectAnimationsProp;
        SerializedProperty onClickAnimationsProp;
        void OnEnable()
        {
            buttonTypeProp = serializedObject.FindProperty("ButtonType");
            selectTypeProp = serializedObject.FindProperty("SelectType");
            onClickProp = serializedObject.FindProperty("OnClick");
            // try multiple possible property names to be compatible with field renames
            string[] selectCandidates = new string[] { "SelectAnimation", "SelectAnimations", "OnSelectAnimations", "SelectedAnimation", "SelectedAnimations", "PointerEnterAnimation" };
            foreach (var name in selectCandidates)
            {
                var p = serializedObject.FindProperty(name);
                if (p != null)
                {
                    selectAnimationsProp = p;
                    break;
                }
            }
            onClickAnimationsProp = serializedObject.FindProperty("OnClickAnimations");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(buttonTypeProp, new GUIContent("ボタンの種類"));
            EditorGUILayout.PropertyField(selectTypeProp, new GUIContent("選択方法"));
            EditorGUILayout.PropertyField(onClickProp, new GUIContent("クリックイベント"));
            // 選択時／非選択時アニメーション表示。フィールド名が変わっていても互換性を保つ。
            if (selectAnimationsProp != null)
            {
                EditorGUILayout.PropertyField(selectAnimationsProp, new GUIContent("選択時アニメーション"), true);
            }
            else
            {
                EditorGUILayout.HelpBox("選択時アニメーションのプロパティが見つかりません。PointerEnterAnimation を使用してください。", MessageType.Info);
            }
            EditorGUILayout.PropertyField(onClickAnimationsProp, new GUIContent("クリック時アニメーション"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
    [CustomEditor(typeof(YMButtonGroup))]
    public class YMButtonGroupEditor : Editor
    {
        SerializedProperty initialIndexProp;
        SerializedProperty buttonsProp;
        SerializedProperty keyTypeProp;
        SerializedProperty nextKeyProp;
        SerializedProperty previousKeyProp;

        void OnEnable()
        {
            initialIndexProp = serializedObject.FindProperty("InitialIndex");
            buttonsProp = serializedObject.FindProperty("buttons");
            keyTypeProp = serializedObject.FindProperty("KeyType");
            nextKeyProp = serializedObject.FindProperty("nextKey");
            previousKeyProp = serializedObject.FindProperty("previousKey");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(initialIndexProp, new GUIContent("初期インデックス"));
            EditorGUILayout.PropertyField(buttonsProp, new GUIContent("ボタン配列"), true);
            EditorGUILayout.PropertyField(keyTypeProp, new GUIContent("操作方法"));
            EditorGUILayout.PropertyField(nextKeyProp, new GUIContent("次へ移動キー"), true);
            EditorGUILayout.PropertyField(previousKeyProp, new GUIContent("前へ移動キー"), true);

            EditorGUILayout.Space();
            if (GUILayout.Button("子からボタンを埋める"))
            {
                foreach (var obj in targets)
                {
                    var group = obj as YMButtonGroup;
                    if (group == null) continue;
                    Undo.RecordObject(group, "Fill Buttons From Children");
                    var found = group.GetComponentsInChildren<YMButton>(true);
                    group.buttons = found;
                    EditorUtility.SetDirty(group);
                }
                // Ensure inspector reflects the change
                serializedObject.Update();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
