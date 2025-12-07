using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using nadena.dev.modular_avatar.core;

namespace nadena.dev.modular_avatar.editor
{
    public class BlendshapeSyncAutoSetup : EditorWindow
    {
        private GameObject sourceObject;
        private List<GameObject> targetObjects = new List<GameObject>();
        private Vector2 scrollPosition;
        private Vector2 targetScrollPosition;
        private Dictionary<GameObject, List<string>> commonBlendshapesPerTarget =
            new Dictionary<GameObject, List<string>>();
        private bool includeChildren = true;

        private enum Language
        {
            English,
            Korean,
            Japanese
        }

        // Default language: Korean
        private Language currentLanguage = Language.Korean;

        // Localization dictionary
        private static readonly Dictionary<Language, Dictionary<string, string>> Localization =
            new Dictionary<Language, Dictionary<string, string>>
        {
            {
                Language.English, new Dictionary<string, string>
                {
                    {"title", "Blendshape Sync Auto Setup"},
                    {"helpBox", "Source Object: Source mesh (reference blendshapes)\nTarget Objects: Target meshes (components will be added here)"},
                    {"sourceObject", "Source Object"},
                    {"targetObjects", "Target Objects (Drag & Drop Multiple Objects Here)"},
                    {"includeChildren", "Include Child Objects"},
                    {"dropHint", "Drag & Drop GameObjects Here\n(Multiple selection supported)"},
                    {"target", "Target"},
                    {"addTarget", "Add Target Object"},
                    {"clearAll", "Clear All"},
                    {"foundBlendshapes", "Found Common Blendshapes"},
                    {"blendshapes", "blendshapes"},
                    {"noCommon", "No common blendshapes found in any target objects."},
                    {"setupButton", "Setup Blendshape Sync"},
                    {"targets", "targets"},
                    {"language", "Language"},
                    {"errorNoSource", "Please select a source object."},
                    {"errorNoSMRSource", "Source object does not have a SkinnedMeshRenderer."},
                    {"complete", "Complete"},
                    {"completeMsg", "Successfully configured {0} target objects with {1} total blendshape bindings.\n\nSource: {2}"},
                    {"error", "Error"},
                    {"errorNoValid", "No valid targets were processed. Please check that all target objects have SkinnedMeshRenderer components."},
                    {"logComplete", "[Blendshape Sync Setup] Complete!\nSource: {0}\nProcessed Targets ({1}):\n{2}\nTotal Blendshapes Configured: {3}"}
                }
            },
            {
                Language.Korean, new Dictionary<string, string>
                {
                    {"title", "블렌드쉐이프 싱크 자동 설정"},
                    {"helpBox", "소스 오브젝트: 참조할 블렌드쉐이프를 가진 메시\n타겟 오브젝트: 컴포넌트가 추가될 메시"},
                    {"sourceObject", "소스 오브젝트"},
                    {"targetObjects", "타겟 오브젝트 (여러 개 드래그 앤 드롭 가능)"},
                    {"includeChildren", "하위 오브젝트 포함"},
                    {"dropHint", "여기에 GameObject를 드래그 앤 드롭하세요\n(다중 선택 지원)"},
                    {"target", "타겟"},
                    {"addTarget", "타겟 오브젝트 추가"},
                    {"clearAll", "전체 삭제"},
                    {"foundBlendshapes", "발견된 공통 블렌드쉐이프"},
                    {"blendshapes", "블렌드쉐이프"},
                    {"noCommon", "타겟 오브젝트에서 공통 블렌드쉐이프를 찾을 수 없습니다."},
                    {"setupButton", "블렌드쉐이프 싱크 설정"},
                    {"targets", "타겟"},
                    {"language", "언어"},
                    {"errorNoSource", "소스 오브젝트를 선택해주세요."},
                    {"errorNoSMRSource", "소스 오브젝트에 SkinnedMeshRenderer가 없습니다."},
                    {"complete", "완료"},
                    {"completeMsg", "{0}개의 타겟 오브젝트에 총 {1}개의 블렌드쉐이프 바인딩이 설정되었습니다.\n\n소스: {2}"},
                    {"error", "오류"},
                    {"errorNoValid", "처리된 타겟이 없습니다. 모든 타겟 오브젝트에 SkinnedMeshRenderer 컴포넌트가 있는지 확인해주세요."},
                    {"logComplete", "[블렌드쉐이프 싱크 설정] 완료!\n소스: {0}\n처리된 타겟 ({1}):\n{2}\n설정된 총 블렌드쉐이프: {3}"}
                }
            },
            {
                Language.Japanese, new Dictionary<string, string>
                {
                    {"title", "ブレンドシェイプ同期自動設定"},
                    {"helpBox", "ソースオブジェクト: 参照するブレンドシェイプを持つメッシュ\nターゲットオブジェクト: コンポーネントが追加されるメッシュ"},
                    {"sourceObject", "ソースオブジェクト"},
                    {"targetObjects", "ターゲットオブジェクト (複数ドラッグ&ドロップ可能)"},
                    {"includeChildren", "子オブジェクトを含む"},
                    {"dropHint", "GameObjectをここにドラッグ&ドロップ\n(複数選択対応)"},
                    {"target", "ターゲット"},
                    {"addTarget", "ターゲットを追加"},
                    {"clearAll", "すべてクリア"},
                    {"foundBlendshapes", "共通ブレンドシェイプが見つかりました"},
                    {"blendshapes", "ブレンドシェイプ"},
                    {"noCommon", "ターゲットオブジェクトに共通のブレンドシェイプが見つかりませんでした。"},
                    {"setupButton", "ブレンドシェイプ同期を設定"},
                    {"targets", "ターゲット"},
                    {"language", "言語"},
                    {"errorNoSource", "ソースオブジェクトを選択してください。"},
                    {"errorNoSMRSource", "ソースオブジェクトにSkinnedMeshRendererがありません。"},
                    {"complete", "完了"},
                    {"completeMsg", "{0}個のターゲットオブジェクトに合計{1}個のブレンドシェイプバインディングを設定しました。\n\nソース: {2}"},
                    {"error", "エラー"},
                    {"errorNoValid", "処理されたターゲットがありません。すべてのターゲットオブジェクトにSkinnedMeshRendererコンポーネントがあることを確認してください。"},
                    {"logComplete", "[ブレンドシェイプ同期設定] 完了！\nソース: {0}\n処理されたターゲット ({1}):\n{2}\n設定された総ブレンドシェイプ: {3}"}
                }
            }
        };

        private string GetText(string key)
        {
            return Localization[currentLanguage][key];
        }

        private void AddGameObjectWithChildren(GameObject go)
        {
            if (go == null) return;

            // Add self
            if (!targetObjects.Contains(go))
            {
                var smr = go.GetComponent<SkinnedMeshRenderer>();
                if (smr != null || !includeChildren)
                {
                    targetObjects.Add(go);
                }
            }

            // Children
            if (includeChildren)
            {
                foreach (Transform child in go.transform)
                {
                    AddGameObjectWithChildren(child.gameObject);
                }
            }
        }

        [MenuItem("Iyan-Kim/Tools/Modular Avatar Blendshape Sync Auto Setup")]
        public static void ShowWindow()
        {
            var window = GetWindow<BlendshapeSyncAutoSetup>("Blendshape Sync Setup");
            window.minSize = new Vector2(500, 600);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(GetText("title"), EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(GetText("helpBox"), MessageType.Info);

            EditorGUILayout.Space(10);

            // Source Object
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(GetText("sourceObject"), GUILayout.Width(150));
            sourceObject = (GameObject)EditorGUILayout.ObjectField(
                sourceObject,
                typeof(GameObject),
                true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            // Target Objects
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(GetText("targetObjects"), EditorStyles.boldLabel);
            includeChildren = EditorGUILayout.Toggle(GetText("includeChildren"), includeChildren, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();

            // Drop area
            Rect dropArea = GUILayoutUtility.GetRect(0f, 80f, GUILayout.ExpandWidth(true));
            GUI.Box(dropArea, GetText("dropHint"), EditorStyles.helpBox);

            Event evt = Event.current;
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (Object draggedObject in DragAndDrop.objectReferences)
                        {
                            GameObject go = draggedObject as GameObject;
                            if (go != null)
                            {
                                AddGameObjectWithChildren(go);
                            }
                        }
                    }
                    evt.Use();
                    break;
            }

            EditorGUILayout.BeginVertical("box");
            targetScrollPosition = EditorGUILayout.BeginScrollView(targetScrollPosition, GUILayout.Height(120));

            for (int i = 0; i < targetObjects.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                targetObjects[i] = (GameObject)EditorGUILayout.ObjectField(
                    $"{GetText("target")} {i + 1}",
                    targetObjects[i],
                    typeof(GameObject),
                    true);

                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    targetObjects.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(GetText("addTarget")))
            {
                targetObjects.Add(null);
            }
            if (GUILayout.Button(GetText("clearAll")) && targetObjects.Count > 0)
            {
                targetObjects.Clear();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            // Common Blendshapes Preview
            if (sourceObject != null && targetObjects.Count > 0)
            {
                UpdateCommonBlendshapes();

                int totalBlendshapes = commonBlendshapesPerTarget.Values.Sum(list => list.Count);

                if (totalBlendshapes > 0)
                {
                    EditorGUILayout.LabelField(GetText("foundBlendshapes"), EditorStyles.boldLabel);

                    EditorGUILayout.BeginVertical("box");
                    scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));

                    foreach (var kvp in commonBlendshapesPerTarget)
                    {
                        if (kvp.Key != null && kvp.Value.Count > 0)
                        {
                            EditorGUILayout.LabelField(
                                $"• {kvp.Key.name}: {kvp.Value.Count} {GetText("blendshapes")}",
                                EditorStyles.boldLabel);
                            foreach (var blendshape in kvp.Value)
                            {
                                EditorGUILayout.LabelField($"    - {blendshape}");
                            }
                            EditorGUILayout.Space(3);
                        }
                    }

                    EditorGUILayout.EndScrollView();
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.HelpBox(GetText("noCommon"), MessageType.Warning);
                }
            }

            EditorGUILayout.Space(10);

            // Setup Button
            bool hasValidTargets = sourceObject != null && targetObjects.Any(t => t != null);
            int validTargetCount = commonBlendshapesPerTarget.Count(kvp => kvp.Key != null && kvp.Value.Count > 0);

            GUI.enabled = hasValidTargets && validTargetCount > 0;

            if (GUILayout.Button($"{GetText("setupButton")} ({validTargetCount} {GetText("targets")})", GUILayout.Height(40)))
            {
                SetupBlendshapeSync();
            }

            GUI.enabled = true;

            EditorGUILayout.Space(10);

            // Language Selection – label on its own line, dropdown below (like PlaneFit tool)
            EditorGUILayout.LabelField("Language / 언어 / 言語", EditorStyles.boldLabel);
            Language newLang = (Language)EditorGUILayout.EnumPopup(currentLanguage);
            if (newLang != currentLanguage)
            {
                currentLanguage = newLang;
                Repaint();
            }

            EditorGUILayout.Space(10);
        }

        private void UpdateCommonBlendshapes()
        {
            commonBlendshapesPerTarget.Clear();

            if (sourceObject == null)
                return;

            var sourceSMR = sourceObject.GetComponent<SkinnedMeshRenderer>();
            if (sourceSMR == null || sourceSMR.sharedMesh == null)
                return;

            var sourceMesh = sourceSMR.sharedMesh;

            var sourceBlendshapes = new HashSet<string>();
            for (int i = 0; i < sourceMesh.blendShapeCount; i++)
            {
                sourceBlendshapes.Add(sourceMesh.GetBlendShapeName(i));
            }

            foreach (var targetObject in targetObjects)
            {
                if (targetObject == null)
                    continue;

                var targetSMR = targetObject.GetComponent<SkinnedMeshRenderer>();
                if (targetSMR == null)
                    continue;

                var targetMesh = targetSMR.sharedMesh;
                if (targetMesh == null)
                    continue;

                var commonList = new List<string>();

                for (int i = 0; i < targetMesh.blendShapeCount; i++)
                {
                    string blendshapeName = targetMesh.GetBlendShapeName(i);
                    if (sourceBlendshapes.Contains(blendshapeName))
                    {
                        commonList.Add(blendshapeName);
                    }
                }

                commonList.Sort();

                if (commonList.Count > 0)
                {
                    commonBlendshapesPerTarget[targetObject] = commonList;
                }
            }
        }

        private void SetupBlendshapeSync()
        {
            if (sourceObject == null)
            {
                EditorUtility.DisplayDialog(GetText("error"), GetText("errorNoSource"), "OK");
                return;
            }

            var sourceSMR = sourceObject.GetComponent<SkinnedMeshRenderer>();
            if (sourceSMR == null)
            {
                EditorUtility.DisplayDialog(GetText("error"), GetText("errorNoSMRSource"), "OK");
                return;
            }

            int successCount = 0;
            int totalBlendshapes = 0;
            List<string> processedTargets = new List<string>();

            foreach (var kvp in commonBlendshapesPerTarget)
            {
                var targetObject = kvp.Key;
                var commonBlendshapes = kvp.Value;

                if (targetObject == null || commonBlendshapes.Count == 0)
                    continue;

                var targetSMR = targetObject.GetComponent<SkinnedMeshRenderer>();
                if (targetSMR == null)
                    continue;

                Undo.RecordObject(targetObject, "Setup Blendshape Sync");

                var syncComponent = targetObject.GetComponent<ModularAvatarBlendshapeSync>();
                if (syncComponent == null)
                {
                    syncComponent = Undo.AddComponent<ModularAvatarBlendshapeSync>(targetObject);
                }

                Undo.RecordObject(syncComponent, "Setup Blendshape Sync");

                syncComponent.Bindings.Clear();

                foreach (var blendshapeName in commonBlendshapes)
                {
                    var avatarRef = new AvatarObjectReference();
                    avatarRef.Set(sourceObject);

                    var binding = new BlendshapeBinding
                    {
                        ReferenceMesh = avatarRef,
                        Blendshape = blendshapeName,
                        LocalBlendshape = blendshapeName
                    };

                    syncComponent.Bindings.Add(binding);
                }

                EditorUtility.SetDirty(syncComponent);

                successCount++;
                totalBlendshapes += commonBlendshapes.Count;
                processedTargets.Add($"  • {targetObject.name}: {commonBlendshapes.Count} {GetText("blendshapes")}");
            }

            if (successCount > 0)
            {
                string targetsList = string.Join("\n", processedTargets);

                Debug.Log(string.Format(GetText("logComplete"),
                    sourceObject.name, successCount, targetsList, totalBlendshapes));

                EditorUtility.DisplayDialog(
                    GetText("complete"),
                    string.Format(GetText("completeMsg"), successCount, totalBlendshapes, sourceObject.name),
                    "OK");
            }
            else
            {
                EditorUtility.DisplayDialog(
                    GetText("error"),
                    GetText("errorNoValid"),
                    "OK");
            }
        }
    }
}
