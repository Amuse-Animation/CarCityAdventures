using AmuseEngine.Assets.Scripts.UserInterface.UnityButton.Manager;
using CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButton;
using CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButtonClickedArgs;
using CCA.UserInterface.Buttons.IAPButton.Controller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace CCA.UserInterface.Buttons.IAPButton.Manager
{
    public class IAPButtonManager : UnityButtonManager
    {
        public bool IsIAPButtonInitialized => isIAPButtonInitialized;

        public UnityEvent<IAPButtonClickedArgsStruct> OnIAPButtonClicked => onIAPButtonClicked;

        [SerializeField] 
        private Transform iapButtonRoot;
        
        [SerializeReference]
        private IAPButtonController iapButtonController;

        [SerializeField] 
        private UnityEvent<IAPButtonClickedArgsStruct> onIAPButtonClicked;

        private bool isIAPButtonInitialized;
        private Product assignedIAPProduct;

        public void InitIAPButton(Product assignedProduct)
        {
            if(isIAPButtonInitialized) return;
            this.assignedIAPProduct = assignedProduct;
            iapButtonController.ChangeButtonText(assignedIAPProduct.metadata.localizedPriceString);
        }
        
        protected override void OnButtonClicked()
        {
            IAPButtonClickedArgsStruct iapButtonClickedArgs = new IAPButtonClickedArgsStruct
            {
                IAPButtonManagerClicked = this,
                ButtonRoot =  iapButtonRoot
            };
            
            onIAPButtonClicked.Invoke(iapButtonClickedArgs);
            
            base.OnButtonClicked();
        }

        public IAPButtonDataArgsStruct GetIAPButtonData() => iapButtonController.IAPButtonData;
        
        
    }
}