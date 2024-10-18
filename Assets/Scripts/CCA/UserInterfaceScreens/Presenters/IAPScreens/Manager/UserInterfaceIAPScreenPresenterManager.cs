using System.Collections.Generic;
using CCA.CustomArgsStructObjects.IAPButtonStruct.IAPButtonClickedArgs;
using CCA.IAP.Manager;
using CCA.UserInterface.Buttons.IAPButton.Manager;
using CCA.UserInterfaceScreens.Presenters.IAPScreens.Controller;
using UnityEngine;

namespace CCA.UserInterfaceScreens.Presenters.IAPScreens.Manager
{
    public class UserInterfaceIAPScreenPresenterManager : MonoBehaviour
    {
        [SerializeField]
        private Transform iapScreenRoot;
        
        [SerializeField] 
        private List<IAPButtonManager> iapButtonManagerList;
        
        [SerializeField]
        private IAPManager iapManager;
        
        [SerializeField]
        private UserInterfaceIAPScreenPresenterController userInterfaceIAPScreenPresenterController;

        private void OnEnable()
        {
            int iapButtonManagerListSize = iapButtonManagerList.Count;
            for (int i = 0; i < iapButtonManagerListSize; i++)
            {
                iapButtonManagerList[i].OnIAPButtonClicked.AddListener(OnIAPButtonClicked);
                if(iapButtonManagerList[i].IsIAPButtonInitialized) continue;
                userInterfaceIAPScreenPresenterController.InjectIAPButtonDependencies(iapManager, iapButtonManagerList[i]);
            }
        }

        private void OnDisable()
        {
            int iapButtonManagerListSize = iapButtonManagerList.Count;
            for (int i = 0; i < iapButtonManagerListSize; i++)
            {
                iapButtonManagerList[i].OnIAPButtonClicked.RemoveListener(OnIAPButtonClicked);
            }
        }

        private void OnIAPButtonClicked(IAPButtonClickedArgsStruct iapButtonClickedArgs)
        {
            
        }


    }
}