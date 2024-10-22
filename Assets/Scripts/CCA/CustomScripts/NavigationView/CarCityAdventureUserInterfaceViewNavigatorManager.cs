using AmuseEngine.Assets.Scripts.UserInterfaceScreen.Navigator.Base;
using AmuseEngine.Assets.Scripts.UserInterfaceScreen.Views.IAPScreen.Manager;
using AmuseEngine.Assets.Scripts.UserInterfaceScreen.Views.LoadingScreen.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace CCA.CustomScripts.NavigationView
{
    public class CarCityAdventureUserInterfaceViewNavigatorManager : UserInterfaceViewNavigatorBaseManager
    {
        [SerializeField]
        private UserInterfaceLoadingScreenViewManager loadingScreenViewManager;
        
        [SerializeField]
        private UserInterfaceIAPScreenViewManager iapScreenViewManager;
        protected override void AddNavigationViewsToDictionary()
        {
            userInterfaceViewNavigatorController.AddViewToDictionary(loadingScreenViewManager);
            userInterfaceViewNavigatorController.AddViewToDictionary(iapScreenViewManager);
        }
    }
}