using CCA.AddressablesContent.Manager;
using CCA.CustomArgsClassObjects.AddressableDownloadableObjectArgs;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs;
using CCA.MainMenuScripts.PanningCamera.Manager;
using CCA.MainMenuScripts.WorldButtonInteraction.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Manager
{
    public class CarCityAdventureWorldButtonInteractionManager : MonoBehaviour
    {
        [SerializeField]
        private PanningCameraManager panningCameraManager;
        
        [SerializeField]
        private AddressableContentManager addressableContentManager;
        
        [SerializeField]
        private CarCityAdventureWorldButtonInteractionController carCityAdventureWorldButtonInteractionController;

        private void Awake()
        {
            carCityAdventureWorldButtonInteractionController.Init(panningCameraManager);
        }

        public void OnWorldButtonClicked(CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs)
        {
            carCityAdventureWorldButtonInteractionController.MoveCameraTowardsWorldButton(worldButtonClickedArgs);

            CarCityAdventureCharacterWorldButtonDataArgsStruct worldButtonData = worldButtonClickedArgs.CharacterWorldButtonClicked.GetCharacterWorldButtonDataArgs();
            AddressableDownloadableObjectArgsClass addressableDownloadableObject =
                new AddressableDownloadableObjectArgsClass(worldButtonData.URL, worldButtonData.TitleFileName, worldButtonData.StorageFolderName, worldButtonData.AddressableContentName);
            addressableContentManager.DownloadAddressableContentAsync(addressableDownloadableObject).Forget();
        }
    }
}