using System;
using AmuseEngine.Assets.Scripts.CameraNavigation.PanningCamera.Manager;
using AmuseEngine.Assets.Scripts.ScriptableVariables.UnityServicesData;
using AmuseEngine.Assets.Scripts.UserInterfaceScreen.Activators.SingleActivator.ActivatorObject;
using AmuseEngine.Assets.Scripts.UserInterfaceScreen.Commander.Interface;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.WorldButtonInteraction.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Manager
{
    public class CarCityAdventureWorldButtonInteractionManager : MonoBehaviour, IUserInterfaceViewCommander
    {
        public string DesiredUserInterfaceView => desiredUserInterfaceView;

        public UnityEvent<UserInterfaceViewSingleActivatorObjectClass> OnUserInterfaceViewCommanded => onUserInterfaceViewCommanded;
        
        [SerializeField]
        private PanningCameraManager panningCameraManager;
        
        [SerializeField]
        private ScriptableVariableUnityServicesData scriptableVariableUnityServicesData;
        
        [SerializeField]
        private CarCityAdventureWorldButtonInteractionController carCityAdventureWorldButtonInteractionController;
        
        [SerializeField]
        private string desiredUserInterfaceView;

        [SerializeField] 
        private string sceneNameToUnload;
        
        [SerializeField] 
        private UnityEvent<UserInterfaceViewSingleActivatorObjectClass> onUserInterfaceViewCommanded;
        
        private CharacterWorldButtonClickedArgsStruct currentWorldButtonClickedArgs;

        private void Awake()
        {
            carCityAdventureWorldButtonInteractionController.Init();
        }

        public void OnWorldButtonClicked(CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs)
        {
            currentWorldButtonClickedArgs = worldButtonClickedArgs;
            
            carCityAdventureWorldButtonInteractionController.MoveCameraTowardsWorldButton(panningCameraManager, worldButtonClickedArgs, null,
                () =>
                {
                    onUserInterfaceViewCommanded.Invoke(new UserInterfaceViewSingleActivatorObjectClass(transform, this, desiredUserInterfaceView));
                });

        }

        public UniTaskVoid ExecuteViewOrder(Action<float> onProgress = null, Action onCompleted = null)
        {
           return carCityAdventureWorldButtonInteractionController.InitiateCharacterWorldButtonGame(
                                                                   scriptableVariableUnityServicesData.Value.AddressableContentManager,
                                                                   currentWorldButtonClickedArgs.CharacterWorldButtonClicked,
                                                                   sceneNameToUnload,
                                                                   onProgress,
                                                                   onCompleted);
            
        }
    }
}