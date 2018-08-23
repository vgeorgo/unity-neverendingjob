using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NeverEndingJob.Components;
using NeverEndingJob.File;

namespace NeverEndingJob.Managers
{
    public class DataManager : MonoBehaviour
    {
        #region Variables
        // Protected
        //protected JSONObject _GameDataObject;
        //protected GameDataStructure _GameDataStructure;
        //protected Game _Game;
        #endregion

        #region Gets
        //public JSONObject GameDataObject { get { return _GameDataObject; } }
        #endregion

        #region Public
        /// <summary>
        /// Check if the .dat file that store gameplay custom data exists.
        /// </summary>
        /// <returns>True or False</returns>
        public bool HasGameData()
        {
            return UnityFile.Exists("GameData.dat", FolderPaths.Persistent);
        }

        /// <summary>
        /// Save the content of the .
        /// </summary>
        public void SaveFile()
        {
            //UnityFile.RewriteFileContent("GameData.dat", _GameDataStructure, FolderPaths.Persistent);
        }
        #endregion

        #region Protected
        protected void Init()
        {
            //var gameData = Resources.Load<TextAsset>("Data/game");
            //_GameDataObject = new JSONObject(gameData.text);

            if (!HasGameData())
                CreateGameDataObject();
            else
                LoadFile();
        }

        protected void CreateGameDataObject()
        {
            //var data = new GameDataStructure();
            //_GameDataStructure = data;

            SaveFile();
        }

        protected void LoadFile()
        {
            //_GameDataStructure = (GameDataStructure)UnityFile.GetFileContent("GameData.dat", FolderPaths.Persistent);
        }
        #endregion
    }
}