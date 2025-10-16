#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Samples {
    [InitializeOnLoad]
    public class SceneLoader : MonoBehaviour {
        static SceneLoader() {
            // always load first scene
            EditorSceneManager.playModeStartScene =
                AssetDatabase.LoadAssetAtPath<SceneAsset>(
                    SceneUtility.GetScenePathByBuildIndex(0));
        }
    }
}
#endif