using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace YourMaidTools
{
    [CustomEditor(typeof(YMAnimationBase), true)]
    /// <summary>
    /// YMAnimationBase Editor
    /// YMAnimationBaseのインスペクターをカスタマイズします。
    /// </summary>
    public class YMAnimationEditor : Editor
    {
        SerializedProperty durationProp;
        SerializedProperty animationTypeProp;
        SerializedProperty animationCurveProp;
        SerializedProperty easeTypeProp;
        string[] excludeProps;

        void OnEnable()
        {
            durationProp = serializedObject.FindProperty("Duration");
            animationTypeProp = serializedObject.FindProperty("AnimationType");
            animationCurveProp = serializedObject.FindProperty("AnimationCurve");
            easeTypeProp = serializedObject.FindProperty("EaseType");
            excludeProps = OnVirtualEnable();
        }
        public virtual string[] OnVirtualEnable()
        {
            return null;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            OnVirtualInspectorGUI();
            EditorGUILayout.PropertyField(durationProp, new GUIContent("アニメーション時間"));
            EditorGUILayout.PropertyField(animationTypeProp, new GUIContent("アニメーションの設定方式"));
            if (animationTypeProp != null)
            {
                YMAnimationType type = (YMAnimationType)animationTypeProp.enumValueIndex;
                EditorGUILayout.Space();
                switch (type)
                {
                    case YMAnimationType.Ease:
                        EditorGUILayout.PropertyField(easeTypeProp, new GUIContent("Ease"));
                        break;
                    case YMAnimationType.AnimationCurve:
                        EditorGUILayout.PropertyField(animationCurveProp, new GUIContent("Animation Curve"));
                        break;
                }
            }
            var excludes = new List<string>()
            {
                "m_Script",
                "Duration",
                "AnimationType",
                "AnimationCurve",
                "EaseType",
            };
            if (excludeProps != null)
                excludes.AddRange(excludeProps);

            DrawPropertiesExcluding(serializedObject, excludes.ToArray());
            serializedObject.ApplyModifiedProperties();
        }
        public virtual void OnVirtualInspectorGUI()
        {
        }
    }

    [CustomEditor(typeof(YMScaleAnimation))]
    public class YMScaleAnimationEditor : YMAnimationEditor
    {
        List<SerializedProperty> excludePropList = new List<SerializedProperty>();
        string[] childExcludeProps = new string[] { "changeScale" };
        string[] excludeContent = new string[] { "変化後のスケール" };
        public override string[] OnVirtualEnable()
        {
            foreach (var propName in childExcludeProps)
            {
                excludePropList.Add(serializedObject.FindProperty(propName));
            }
            return childExcludeProps;
        }
        public override void OnVirtualInspectorGUI()
        {
            for (int i = 0; i < excludePropList.Count; i++)
            {
                EditorGUILayout.PropertyField(excludePropList[i], new GUIContent(excludeContent[i]));
            }
        }
    }
    [CustomEditor(typeof(YMTextScaleAnimation))]
    public class YMTextScaleAnimationEditor : YMAnimationEditor
    {
        List<SerializedProperty> excludePropList = new List<SerializedProperty>();
        string[] childExcludeProps = new string[] { "changeScale" };
        string[] excludeContent = new string[] { "変化後のスケール" };
        public override string[] OnVirtualEnable()
        {
            foreach (var propName in childExcludeProps)
            {
                excludePropList.Add(serializedObject.FindProperty(propName));
            }
            return childExcludeProps;
        }
        public override void OnVirtualInspectorGUI()
        {
            for (int i = 0; i < excludePropList.Count; i++)
            {
                EditorGUILayout.PropertyField(excludePropList[i], new GUIContent(excludeContent[i]));
            }
        }
    }
    [CustomEditor(typeof(YMColorChangeAnimation))]
    public class YMColorChangeAnimationEditor : YMAnimationEditor
    {
        List<SerializedProperty> excludePropList = new List<SerializedProperty>();
        string[] childExcludeProps = new string[] { "changeColor" };
        string[] excludeContent = new string[] { "変化後のカラー" };
        public override string[] OnVirtualEnable()
        {
            foreach (var propName in childExcludeProps)
            {
                excludePropList.Add(serializedObject.FindProperty(propName));
            }
            return childExcludeProps;
        }
        public override void OnVirtualInspectorGUI()
        {
            for (int i = 0; i < excludePropList.Count; i++)
            {
                EditorGUILayout.PropertyField(excludePropList[i], new GUIContent(excludeContent[i]));
            }
        }
    }
    [CustomEditor(typeof(YMPictureChangeAnimation))]
    public class YMPictureChangeAnimationEditor : YMAnimationEditor
    {
        List<SerializedProperty> excludePropList = new List<SerializedProperty>();
        string[] childExcludeProps = new string[] { "changeAlpha", "changeSprite" };
        string[] excludeContent = new string[] { "変化後のアルファ値", "変化後の画像" };
        public override string[] OnVirtualEnable()
        {
            foreach (var propName in childExcludeProps)
            {
                excludePropList.Add(serializedObject.FindProperty(propName));
            }
            return childExcludeProps;
        }
        public override void OnVirtualInspectorGUI()
        {
            for (int i = 0; i < excludePropList.Count; i++)
            {
                EditorGUILayout.PropertyField(excludePropList[i], new GUIContent(excludeContent[i]));
            }
        }
    }
}