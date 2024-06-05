using System;
using MIG.API;
using UnityEditor;
using UnityEngine;

namespace MIG.Editor
{
    [CustomPropertyDrawer(typeof(CheckObjectAttribute))]
    public sealed class CheckObjectPropertyDrawer : PropertyDrawer
    {
        private static CheckObjectPropertyDrawerSettings Settings =>
            GameConfigLocator.Get<CheckObjectPropertyDrawerSettings>();

        private CheckObjectAttribute CheckAttr => attribute as CheckObjectAttribute;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (!IsPropertySupported(property)) throw new Exception($"{property.propertyType} is not supported");

            using (GetScope(property))
            {
                EditorGUI.PropertyField(rect, property, label, property.hasVisibleChildren);
            }
        }

        private IDisposable GetScope(SerializedProperty property)
        {
            var color = GetPropertyColor(property);
            return CheckAttr.Highlight switch
            {
                CheckHighlight.Background => new GUIBackgroundColorScope(color),
                CheckHighlight.Content => new GUIContentColorScope(color),
                _ => new GUIColorScope(color)
            };
        }

        private Color GetPropertyColor(SerializedProperty property)
        {
            return IsPropertyValid(property)
                ? Settings.ValidObjectColor
                : Settings.InvalidObjectColor;
        }

        private bool IsPropertySupported(SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.ObjectReference
                    or SerializedPropertyType.ExposedReference
                    or SerializedPropertyType.ManagedReference => true,
                _ => false
            };
        }

        private bool IsPropertyValid(SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.ObjectReference => property.objectReferenceValue != null,
                SerializedPropertyType.ExposedReference => property.exposedReferenceValue != null,
                SerializedPropertyType.ManagedReference => property.managedReferenceValue != null,
                _ => false
            };
        }
    }
}