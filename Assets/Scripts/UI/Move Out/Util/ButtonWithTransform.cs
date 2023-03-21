using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Move_Out.Util
{
    [Serializable]
    public class ButtonWithTransform
    {
        public Button topButton;
        private Vector2 _transform;

        public void StoreTransform()
        {
            _transform = topButton.transform.position;
        }

        
    }
}