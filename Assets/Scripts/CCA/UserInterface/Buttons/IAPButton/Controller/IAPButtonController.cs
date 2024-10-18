using AmuseEngine.Assets.Scripts.UserInterface.UnityButton.Controller;
using CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButton;
using UnityEngine;

namespace CCA.UserInterface.Buttons.IAPButton.Controller
{
    public class IAPButtonController : UnityButtonController
    {
        public IAPButtonDataArgsStruct IAPButtonData => iapButtonData;

        [SerializeField]
        private IAPButtonDataArgsStruct iapButtonData;

    }

   
}