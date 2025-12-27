using UnityEditor;
using UnityEngine;

namespace YourMaidTools
{
    [CustomEditor(typeof(YMButton))]
    public class YMButtonEditor : Editor
    {
        SerializedProperty buttonTypeProp;
        SerializedProperty onClickProp;
        SerializedProperty buttonAnimationsProp;
        void OnEnable()
        {
            buttonTypeProp = serializedObject.FindProperty("ButtonType");
            onClickProp = serializedObject.FindProperty("OnClick");
            buttonAnimationsProp = serializedObject.FindProperty("Animations");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(buttonTypeProp, new GUIContent("ボタンの種類"));
            EditorGUILayout.PropertyField(onClickProp, new GUIContent("クリックイベント"));
            EditorGUILayout.PropertyField(buttonAnimationsProp, new GUIContent("アニメーション"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
