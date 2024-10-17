using System;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.LoadDataSerializer;
using CCA.AddressablesContent.Manager;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs;
using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.AddressableDownloadableContent;
using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using CCA.MainMenuScripts.PanningCamera.Manager;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Behaviour
{
    public class CarCityAdventureWorldButtonInteractionBehaviour
    {
        public void MoveCameraTowardsWorldButton(PanningCameraManager panningCameraManager ,CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs, float movementDuration, System.Action onMovementStart = null, System.Action onMovementEnd = null)
        {
            Vector3 finalPoint = worldButtonClickedArgs.CharacterWorldButtonRoot.position;
            finalPoint.z = -10f;
            panningCameraManager.CameraToMove.DOLocalMove(finalPoint, movementDuration)
                .OnStart(
                    () =>
                    {
                        panningCameraManager.DoActivateAutomaticMovement();
                        onMovementStart?.Invoke();
                    }).OnComplete(() =>
                    {
                        panningCameraManager.DoDeactivateAutomaticMovement();
                        onMovementEnd?.Invoke();
                    });
        }

        public async UniTaskVoid InitiateCharacterWorldButtonGame(AddressableContentManager addressableContentManager, CarCityAdventureCharacterWorldButtonDataArgsStruct worldButtonClickedArgs)
        {
            IAddressableDownloadableContent addressableDownloadableObject = new AddressableDownloadableContentClass(worldButtonClickedArgs.URL, worldButtonClickedArgs.TitleFileName, worldButtonClickedArgs.StorageFolderName, worldButtonClickedArgs.AddressableContentName);

            if (!LoadDataSerializerStaticClass.HasSaveFileBeenCreated(addressableDownloadableObject.TitleFileName, addressableDownloadableObject.StorageFolderName))
            {
                string downloadedContent = await addressableContentManager.DoDownloadAddressableContentAsync(addressableDownloadableObject);
                addressableContentManager.DoSaveDownloadedAddressableContent(downloadedContent,addressableDownloadableObject);
            }
            
            IAddressableLoadableContent addressableLoadableContent = await addressableContentManager.LoadDownloadedAddressableCatalogContent(addressableDownloadableObject);
            SceneInstance sceneInstance = await addressableContentManager.LoadAddressableSceneInCatalogContent(addressableLoadableContent, LoadSceneMode.Single);
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            sceneInstance.ActivateAsync();
        }
    }
}