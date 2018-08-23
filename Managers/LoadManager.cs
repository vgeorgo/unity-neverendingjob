using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

using NeverEndingJob.Application;
using NeverEndingJob.Components;

namespace NeverEndingJob.Managers
{
    public class LoadManager : Singleton<LoadManager>
    {
        #region Variables
        // Public
        public GameObject Loading;
        public ProgressBar ProgressBar;
        #endregion

        #region Public
        /// <summary>
        /// Load the scene synchronously.
        /// </summary>
        /// <param name="SceneName">Scene to be loaded</param>
        public void LoadScene(string SceneName)
        {
            Loading.SetActive(true);
            SceneManager.LoadScene(SceneName);
        }

        /// <summary>
        /// Adds the scene to the current scene.
        /// </summary>
        /// <param name="SceneName">Scene to be loaded</param>
        public void LoadSceneAdditive(string SceneName)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Load a scene asynchronously.
        /// </summary>
        /// <param name="SceneName">Scene to be loaded</param>
        /// <param name="useProgress">If use progress is set to true it automatically starts the animation of the loading scene progress</param>
        public void LoadSceneAsync(string SceneName, bool useProgress)
        {
            Loading.SetActive(true);
            var _AsyncOperation = SceneManager.LoadSceneAsync(SceneName);
            if (useProgress)
                StartCoroutine(LoadSceneWithProgress(_AsyncOperation));
        }

        /// <summary>
        /// Opens the specified URL on the device browser.
        /// </summary>
        /// <param name="Url">URL to be opened</param>
        public void LoadUrl(string Url)
        {
            UnityEngine.Application.OpenURL(Url);
        }

        public Sprite LoadUIImage(string path)
        {
            return Resources.Load<Sprite>(path);
        }
        #endregion

        #region Protected
        IEnumerator LoadSceneWithProgress(AsyncOperation AsyncOperation)
        {
            while (!AsyncOperation.isDone)
            {
                if (ProgressBar != null)
                    ProgressBar.SetProgress(AsyncOperation.progress);
                yield return null;
            }
        }
        #endregion
    }
}
