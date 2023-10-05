using Cysharp.Threading.Tasks;
using AGames.AssetManagment;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AGames.SpaceInvaders
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] GameLoaderConfig m_Config;
        [SerializeField] GameObject m_SplashScreen;
        [Header("Progress Bar")]
        [SerializeField] Image m_Progress;
        [SerializeField] float m_ProgressSmooth;

        private float m_TargetProgress;

        private void Start()
        {
            m_TargetProgress = 0;
            m_Progress.fillAmount = 0;

            // load assets
            AssetManager.LoadAssets(m_Config.PreloadKey, 
                // on complete
                () => UniTask.Run(OnScenesLoadCompleted),
                // update progress
                (x) => m_TargetProgress = x);
        }

        private void Update()
        {
            m_Progress.fillAmount = Mathf.Lerp(m_Progress.fillAmount, m_TargetProgress, Time.deltaTime * m_ProgressSmooth);
        }

        async UniTaskVoid OnScenesLoadCompleted()
        {
            // wait
            await UniTask.Delay(TimeSpan.FromSeconds(m_Config.WaitAfterLoadingEverything));
            // initialization completed
            GameMessage.StateCommand.Post(GameStateCommand.InitializationCompleted);
            // hide splash screen
            m_SplashScreen.SetActive(false);
            // unload init scene
            await SceneManager.UnloadSceneAsync(0);
        }
    }
}