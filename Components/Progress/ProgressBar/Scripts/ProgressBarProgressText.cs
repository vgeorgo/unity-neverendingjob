using UnityEngine;
using System.Collections;

using NeverEndingJob.Util;

namespace NeverEndingJob.Components
{
    public class ProgressBarProgressText : ProgressBar
    {
        #region Protected
        protected override void UpdateLabel()
        {
            base.UpdateLabel();

            bool isSmaller;
            if (Direction == Type.LeftToRight || Direction == Type.RightToLeft)
                isSmaller = Label.rectTransform.rect.width < TextUtil.GetUiTextWidth(Label, Label.text);
            else
                isSmaller = Label.rectTransform.rect.height < TextUtil.GetUiTextHeight(Label, Label.text);

            switch (Direction)
            {
                case Type.LeftToRight:
                    Label.alignment = isSmaller ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;
                    break;

                case Type.RightToLeft:
                    Label.alignment = isSmaller ? TextAnchor.MiddleRight : TextAnchor.MiddleCenter;
                    break;

                case Type.TopToBottom:
                    Label.alignment = isSmaller ? TextAnchor.UpperCenter : TextAnchor.MiddleCenter;
                    break;

                case Type.BottomToTop:
                    Label.alignment = isSmaller ? TextAnchor.LowerCenter : TextAnchor.MiddleCenter;
                    break;
            }
        }
        #endregion
    }
}