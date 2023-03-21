using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Move_Out.ControlRoot
{
    [Serializable]
    public class UI2ButtonIcon
    {
        public Button rootButton;
        public Image icon;
        
        public void SetActive(bool active)
        {
            rootButton.gameObject.SetActive(true);
            icon.gameObject.SetActive(active);
        }
    }
}