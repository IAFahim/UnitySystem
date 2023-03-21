using System;
using UI.Move_Out.ControlRoot;
using UI.Move_Out.ControlVertical;

namespace UI.Move_Out.ControlHorizontal
{
    [Serializable]
    public class UI3LeftCenterRight
    {
        public UI4Vertical left;
        public UI2ButtonIcon center;
        public UI4Vertical right;

        public void StoreTransform()
        {
            left.StoreTransform();
            right.StoreTransform();
        }

        public void SetActive()
        {
            left.SetActive(!left.BackgroundImage.IsActive());
            right.SetActive(!right.BackgroundImage.IsActive());
            
        }

        
    }
}