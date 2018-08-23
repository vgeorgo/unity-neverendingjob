using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using NeverEndingJob.Application;
using NeverEndingJob.Components;

namespace NeverEndingJob.Managers
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        #region Variables
        // Structs
        [Serializable]
        public struct DefaultValue
        {
            public string key;
            public string value;
        }

        // Public
        public DefaultValue[] DefaultValues;

        // Protected
        protected const string CONFIG_EXISTS = "config-exists";
        #endregion

        #region Public
        /// <summary>
        /// Resets all the values on the PlayerPrefs with the DefaultValues array.
        /// </summary>
        public void Reset()
        {
            PlayerPrefs.DeleteAll();
            SetInitialValues();
        }

        /// <summary>
        /// Resets only one key of the PlayerPrefs and set its DefaultValue if exists.
        /// </summary>
        /// <param name="key">Key of the PlayerPrefs</param>
        protected void ResetKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            SetInitialValues(key);
        }

        /// <summary>
        /// Set the int PlayerPrefs and saves if chosen.
        /// </summary>
        /// <param name="key">Key of the PlayerPrefs</param>
        /// <param name="value">value of the PlayerPrefs</param>
        /// <param name="autoSave">Saves the PlayerPrefs if it is true</param>
        public void SetInt(string key, int value, bool autoSave = true)
        {
            PlayerPrefs.SetInt(key, value);
            OnChangeSettings(autoSave);
        }

        /// <summary>
        /// Set the float PlayerPrefs and saves if chosen.
        /// </summary>
        /// <param name="key">Key of the PlayerPrefs</param>
        /// <param name="value">value of the PlayerPrefs</param>
        /// <param name="autoSave">Saves the PlayerPrefs if it is true</param>
        public void SetFloat(string key, float value, bool autoSave = true)
        {
            PlayerPrefs.SetFloat(key, value);
            OnChangeSettings(autoSave);
        }

        /// <summary>
        /// Set the string PlayerPrefs and saves if chosen.
        /// </summary>
        /// <param name="key">Key of the PlayerPrefs</param>
        /// <param name="value">value of the PlayerPrefs</param>
        /// <param name="autoSave">Saves the PlayerPrefs if it is true</param>
        public void SetString(string key, string value, bool autoSave = true)
        {
            PlayerPrefs.SetString(key, value);
            OnChangeSettings(autoSave);
        }

        /// <summary>
        /// Set the value PlayerPrefs and saves if chosen. It converts the value to int, float or mantain string.
        /// </summary>
        /// <param name="key">Key of the PlayerPrefs</param>
        /// <param name="value">value of the PlayerPrefs</param>
        /// <param name="autoSave">Saves the PlayerPrefs if it is true</param>
        public void SetPref(string key, string value, bool autoSave = true)
        {
            int valueInt;
            if (int.TryParse(value, out valueInt))
                PlayerPrefs.SetInt(key, valueInt);

            float valueFloat;
            if (float.TryParse(value, out valueFloat))
                PlayerPrefs.SetFloat(key, valueFloat);

            PlayerPrefs.SetString(key, value);
            OnChangeSettings(autoSave);
        }
        #endregion

        #region Protected
        protected override void Init()
        {
            if (!PlayerPrefs.HasKey(CONFIG_EXISTS) || PlayerPrefs.GetInt(CONFIG_EXISTS) == 0)
                SetInitialValues();
        }

        protected void SetInitialValues(string key = null)
        {
            foreach (DefaultValue dv in DefaultValues)
            {
                if (key != null && dv.key != key)
                    continue;

                SetPref(dv.key, dv.value);
            }

            PlayerPrefs.SetInt(CONFIG_EXISTS, 1);
            OnChangeSettings(true);
        }

        protected void OnChangeSettings(bool save = true)
        {
            if (save)
                PlayerPrefs.Save();
        }
        #endregion
    }
}