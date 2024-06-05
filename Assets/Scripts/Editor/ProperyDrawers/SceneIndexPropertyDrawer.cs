using System;
using System.Linq;
using MIG.API;
using UnityEditor;
using UnityEngine;

namespace MIG.Editor
{
    [CustomPropertyDrawer(typeof(SceneIndexAttribute))]
    public sealed class SceneIndexPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                throw new ArgumentException($"Invalid property type {property.propertyType}");
            }

            var sceneNames = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, sceneNames);
        }
    }
}