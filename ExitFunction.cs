using UnityEngine;

namespace Samples {
    public class ExitFunction : MonoBehaviour {

        public void ExitGame() {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
