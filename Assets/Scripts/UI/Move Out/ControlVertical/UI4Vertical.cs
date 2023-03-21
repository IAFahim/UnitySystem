using System;
using UI.Move_Out.Util;
using UnityEngine.UI;

namespace UI.Move_Out.ControlVertical
{
    [Serializable]
    public class UI4Vertical
    {
        public Image BackgroundImage;
        public ButtonWithTransform topBT;
        public ButtonWithTransform middleBT;
        public ButtonWithTransform bottomBT;

        public void StoreTransform()
        {
            topBT.StoreTransform();
            middleBT.StoreTransform();
            bottomBT.StoreTransform();
        }

        public void SetActive(bool active)
        {
            BackgroundImage.gameObject.SetActive(true);
            //then move out other object after a delay
        }
        
    }
}