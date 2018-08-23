using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Application
{
    public class QuitApplication : MonoBehaviour
    {
        #region Public
        /// <summary>
        /// Quit the application.
        /// </summary>
        public void Quit()
        {

            //If we are running in a standalone build of the game
            #if UNITY_STANDALONE
		    //Quit the application
		    UnityEngine.Application.Quit();
            #endif

            //If we are running in the editor
            #if UNITY_EDITOR
		    //Stop playing the scene
		    UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        #endregion
    }
}
