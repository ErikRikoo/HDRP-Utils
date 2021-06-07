using System.Linq;
using com.rikoo.HDRPUtilities.Runtime.CustomPass.RenderingLayerPass;
using UnityEditor;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

namespace com.rikoo.HDRPUtilities.Editor.CustomPass.RenderingLayerPass
{
    [CustomPassDrawer(typeof(RenderingLayerCustomPass))]
    public class RenderingLayerCustomPassEditor : CustomPassDrawer
    {
        private class Styles
        {
            public static readonly float LineHeight =
                EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            public static readonly GUIContent MaterialStyle = 
                new GUIContent("Material Override", "The material which is used when rerendering the objects");
            
            public static readonly GUIContent RenderingLayerStyle = 
                new GUIContent("Rendering Layer", "The rendering layer used to filter out which renderer to draw");
            
            public static readonly GUIContent PassIndexStyle = 
                new GUIContent("Pass used", "The material pass used while rendering the objects");

            public static readonly int IndentSpace = 16;
        }

        private SerializedProperty m_OverrideMaterialProperty;
        private SerializedProperty m_RenderingLayerProperty;
        private SerializedProperty m_PassIndexProperty;
        
        protected override void Initialize(SerializedProperty customPass)
        {
            base.Initialize(customPass);
            m_OverrideMaterialProperty = customPass.FindPropertyRelative("m_OverrideMaterial");
            m_RenderingLayerProperty = customPass.FindPropertyRelative("m_RenderingLayer");
            m_PassIndexProperty = customPass.FindPropertyRelative("m_PassIndex");
        }

        protected override void DoPassGUI(SerializedProperty customPass, Rect rect)
        {
            EditorGUI.PropertyField(rect, m_RenderingLayerProperty, Styles.RenderingLayerStyle);
            rect.y += Styles.LineHeight;
            
            EditorGUI.PropertyField(rect, m_OverrideMaterialProperty, Styles.MaterialStyle);
            rect.y += Styles.LineHeight;
            
            // Passes
            ++EditorGUI.indentLevel;

            var material = m_OverrideMaterialProperty.objectReferenceValue as Material;
            if (material == null)
            {
                EditorGUI.HelpBox(rect, "You should pick a material", MessageType.Warning);
            }
            else
            {
                EditorGUI.BeginProperty(rect, Styles.PassIndexStyle, m_PassIndexProperty);
                EditorGUI.IntPopup(
                    rect, m_PassIndexProperty, GetMaterialPassName(material), 
                    Enumerable.Range(0, material.passCount).ToArray());
                EditorGUI.EndProperty();
            }
            --EditorGUI.indentLevel;
            rect.y += Styles.LineHeight;
        }

        private GUIContent[] GetMaterialPassName(Material material)
        {
            GUIContent[] ret = new GUIContent[material.passCount];
            for (int i = 0; i < material.passCount; ++i)
            {
                ret[i] = new GUIContent(material.GetPassName(i));                
            }

            return ret;
        }
    }
}