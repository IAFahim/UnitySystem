using System;
using UI.Move_Out.ControlHorizontal;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Move_Out
{
    public class MoveDir : MonoBehaviour
    {
        [FormerlySerializedAs("ui3")] public UI3LeftCenterRight horizontal;

        private void OnEnable()
        {
            horizontal.StoreTransform();
            horizontal.center.rootButton.onClick.AddListener(SetActive);
        }
        
        private void SetActive()
        {
            horizontal.SetActive();
        }
    }
}