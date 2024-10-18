using CCA.UserInterface.Buttons.IAPButton.Manager;
using UnityEngine;

namespace CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButtonClickedArgs
{
    [System.Serializable]
    public struct IAPButtonClickedArgsStruct
    {
        public IAPButtonManager IAPButtonManagerClicked;
        public Transform ButtonRoot;
    }
}