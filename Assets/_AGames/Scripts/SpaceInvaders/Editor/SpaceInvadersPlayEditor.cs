using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AGames.SpaceInvaders.Editor
{
    public class SpaceInvadersPlayEditor
    {
        const string INIT_SCENE_PATH = "Assets/_AGames/_Scenes/init.unity";
        const string SETTINGS_ASSET_PATH = "Assets/_AGames/Data/SpaceInvadersGameSettings.asset";
        const string GAME_LOADER_CONFIG_ASSET_PATH = "Assets/_AGames/Data/SpaceInvadersGameLoaderConfig.asset";


        [MenuItem("Space Invaders/Play", priority = 0)]
        static void Play()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // load init scene
                EditorSceneManager.OpenScene(INIT_SCENE_PATH, OpenSceneMode.Single);
                // play
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Space Invaders/Game Settings", priority = 20)]
        static void SelectSettings() => SelectAsset(SETTINGS_ASSET_PATH);

        [MenuItem("Space Invaders/Game Loader Config", priority = 20)]
        static void SelectLoaderConfig() => SelectAsset(GAME_LOADER_CONFIG_ASSET_PATH);

        static void SelectAsset(string path)
        {
            Object configAsset = AssetDatabase.LoadMainAssetAtPath(path);

            if (configAsset == null)
            {
                Debug.LogWarningFormat("Asset is missing: {0}", path);
                return;
            }

            Selection.activeObject = configAsset;
            EditorGUIUtility.PingObject(configAsset);
        }

    }
}
