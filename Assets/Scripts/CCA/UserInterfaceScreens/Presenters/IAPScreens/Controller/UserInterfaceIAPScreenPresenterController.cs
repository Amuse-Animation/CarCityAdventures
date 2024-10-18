using CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButton;
using CCA.IAP.Manager;
using CCA.UserInterface.Buttons.IAPButton.Manager;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CCA.UserInterfaceScreens.Presenters.IAPScreens.Controller
{
    public class UserInterfaceIAPScreenPresenterController : MonoBehaviour
    {
        public void InjectIAPButtonDependencies(IAPManager iapManager ,IAPButtonManager iapButtonManager)
        {
            if(iapButtonManager.IsIAPButtonInitialized) return;
            IAPButtonDataArgsStruct iapButtonDataArgsStruct = iapButtonManager.GetIAPButtonData();
            Product designedIAPProduct = iapManager.DoGetDesiredProduct(iapButtonDataArgsStruct.IAPId);
            iapButtonManager.InitIAPButton(designedIAPProduct);
        }
    }
}