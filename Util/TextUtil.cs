using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeverEndingJob.Util
{
    public class TextUtil
    {
        /// <summary>
        /// Get the width of the UI Text string.
        /// </summary>
        /// <param name="uiText">Unity text component</param>
        /// <param name="text">Text to be calculated</param>
        /// <returns></returns>
        public static float GetUiTextWidth(Text uiText, string text)
        {
            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = uiText.GetGenerationSettings(uiText.rectTransform.rect.size);
            return textGen.GetPreferredWidth(text, generationSettings);
        }

        /// <summary>
        /// Get the height of the UI Text string.
        /// </summary>
        /// <param name="uiText">Unity text component</param>
        /// <param name="text">Text to be calculated</param>
        /// <returns></returns>
        public static float GetUiTextHeight(Text uiText, string text)
        {
            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = uiText.GetGenerationSettings(uiText.rectTransform.rect.size);
            return textGen.GetPreferredHeight(text, generationSettings);
        }
    }
}