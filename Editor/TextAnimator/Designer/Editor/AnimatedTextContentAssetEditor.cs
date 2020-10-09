using Com.Jschiff.UnityExtensions.TextAnimator.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Scripts.Terminal.Designer {

    [CustomEditor(typeof(AnimatedTextContentAsset))]
    public class AnimatedTextContentAssetEditor : Editor {
        AnimatedTextContentAsset asset;
        ReorderableList itemsList;

        SerializedProperty itemsProperty;
        SerializedProperty optionsProperty;

        List<Type> itemTypes = new List<Type>();
        Dictionary<Type, Editor> cachedEditors = new Dictionary<Type, Editor>();
        Dictionary<AnimatedItemAsset, int> depths = new Dictionary<AnimatedItemAsset, int>();

        private void OnEnable() {
            asset = (AnimatedTextContentAsset)target;
            itemTypes = GetTypesExtendingFrom(typeof(AnimatedItemAsset)).OrderBy(t => t.Name).ToList();


            itemsProperty = serializedObject.FindProperty("Items");
            optionsProperty = serializedObject.FindProperty("Options");


            itemsList = new ReorderableList(serializedObject,
                itemsProperty, true, true, true, true);

            int rowSize = 1;
            itemsList.elementHeight = EditorGUIUtility.singleLineHeight * rowSize;

            itemsList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Items");
            };

            itemsList.onAddDropdownCallback += (rct, list) => {
                var menu = new GenericMenu();
                foreach (var assetType in itemTypes) {
                    var itemType = assetType;
                    while (typeof(AnimatedItemAsset) != itemType.BaseType) {
                        itemType = itemType.BaseType;
                    }
                    itemType = itemType.GetGenericArguments()[0];

                    menu.AddItem(new GUIContent(itemType.Name), false, () => {
                        var instance = CreateInstance(assetType);
                        instance.name = $"zzz-{itemType.Name}-{Guid.NewGuid()}";
                        var path = AssetDatabase.GetAssetPath(asset);
                        AssetDatabase.AddObjectToAsset(instance, path);
                        AssetDatabase.SaveAssets();

                        asset.Items.Add((AnimatedItemAsset)instance);
                        serializedObject.Update();

                        //var index = list.serializedProperty.arraySize;
                        //itemsProperty.InsertArrayElementAtIndex(index);
                        //var element = list.serializedProperty.GetArrayElementAtIndex(index);
                        //element.objectReferenceValue = instance;
                    });
                }

                menu.ShowAsContext();
            };

            itemsList.onRemoveCallback += (list) => {
                AssetDatabase.RemoveObjectFromAsset(asset.Items[list.index]);
                AssetDatabase.SaveAssets();
                asset.Items.RemoveAt(list.index);
            };

            itemsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = itemsList.serializedProperty.GetArrayElementAtIndex(index);

                rect.height = EditorGUIUtility.singleLineHeight;

                var t = element.objectReferenceValue.GetType();
                string previewString;
                AnimatedItemAsset elementAsset = (AnimatedItemAsset)element.objectReferenceValue;
                string depthPrefix = new string('-', depths[elementAsset] * 2);
                var listPreview = elementAsset.ListPreview.Replace("\n", " ").Replace("\r", " ");
                var listLabel = elementAsset.ListLabel;

                GUIContent label = new GUIContent($"{depthPrefix}{listLabel}");
                Vector2 labelSize = GUI.skin.label.CalcSize(label);
                var labelRect = rect;
                labelRect.width = labelSize.x;
                EditorGUI.LabelField(labelRect, label);

                var previewRect = rect;
                previewRect.x = labelRect.x + labelSize.x + 2;
                var labelStyle = EditorStyles.label;
                var oldColor = labelStyle.normal.textColor;

                labelStyle.normal.textColor = Color.cyan;
                EditorGUI.LabelField(previewRect, listPreview, labelStyle);
                labelStyle.normal.textColor = oldColor;

                rect.y += EditorGUIUtility.singleLineHeight;
                rect.height = EditorGUIUtility.singleLineHeight * (rowSize - 1);

                if (itemsList.index == index) {
                    Editor editor = null; 
                    cachedEditors.TryGetValue(t, out editor);
                    CreateCachedEditor(element.objectReferenceValue, null, ref editor);
                    cachedEditors[t] = editor;

                    EditorGUILayout.LabelField("Element " + index, EditorStyles.boldLabel);
                    editor.OnInspectorGUI();
                    serializedObject.ApplyModifiedProperties();
                }
            };
        }

        IEnumerable<Type> GetTypesExtendingFrom(Type inputType) {
            return Assembly.GetAssembly(inputType).GetTypes().Where(theType =>
              theType.IsClass && !theType.IsAbstract && theType.IsSubclassOf(inputType));
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            serializedObject.Update();

            GUI.enabled = false;
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
            GUI.enabled = true;

            EditorGUILayout.PropertyField(optionsProperty);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Select an element from the list to edit", EditorStyles.boldLabel);

            UpdateDepths();
            itemsList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void UpdateDepths() {
            int depth = 0;
            depths.Clear();

            for (int i = 0; i < asset.Items.Count; i++) {
                var item = asset.Items[i];
                depths[item] = depth;
                depth += item.ScopeDepthChange;
            }
        }
    }
}
