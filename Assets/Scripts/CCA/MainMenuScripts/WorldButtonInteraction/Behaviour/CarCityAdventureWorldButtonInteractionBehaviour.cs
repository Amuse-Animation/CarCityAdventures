using System;
using AmuseEngine.Assets.Scripts.CameraNavigation.PanningCamera.Manager;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.LoadDataSerializer;
using AmuseEngine.Assets.Scripts.InternetContent.AddressablesContent.Manager;
using AmuseEngine.Assets.Scripts.InternetContent.DownloadableContent.AddressableDownloadableObject.AddressableDownloadableContent;
using AmuseEngine.Assets.Scripts.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using AmuseEngine.Assets.Scripts.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.CharacterWorldButton.Manager;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

        public async UniTaskVoid InitiateCharacterWorldButtonGame(AddressableContentManager addressableContentManager, CarCityAdventureCharacterWorldButtonManager characterWorldButtonManagerClicked, string sceneNameToUnload = "" ,System.Action<float> onProgress = null, System.Action onCompleted = null)
        {
            float currentProgress = 0f;

            CarCityAdventureCharacterWorldButtonDataArgsStruct characterWorldButtonDataArgs = characterWorldButtonManagerClicked.GetCharacterWorldButtonDataArgs();
            
            IAddressableDownloadableContent addressableDownloadableObject = new AddressableDownloadableContentClass(characterWorldButtonDataArgs.URL, characterWorldButtonDataArgs.TitleFileName, characterWorldButtonDataArgs.StorageFolderName, characterWorldButtonDataArgs.AddressableContentName);
            
            if (!characterWorldButtonManagerClicked.HasWorldContentBeenPreviouslyDownloaded())
            {
                string downloadedContent = await addressableContentManager.DoDownloadAddressableContentAsync(addressableDownloadableObject);
                addressableContentManager.DoSaveDownloadedAddressableContent(downloadedContent,addressableDownloadableObject);

                await DOTween.To(() => currentProgress, x => currentProgress = x, 30f, 1f).OnUpdate(() =>
                {
                    onProgress?.Invoke(currentProgress);
                }).AsyncWaitForCompletion().AsUniTask();
            }
            
            IAddressableLoadableContent addressableLoadableContent = await addressableContentManager.LoadDownloadedAddressableCatalogContent(addressableDownloadableObject);
            
            await DOTween.To(() => currentProgress, x => currentProgress = x, 50f, 1f).OnUpdate(() =>
            {
                onProgress?.Invoke(currentProgress);
            }).AsyncWaitForCompletion().AsUniTask();
            
            SceneInstance sceneInstance = await addressableContentManager.LoadAddressableSceneInCatalogContent(addressableLoadableContent, LoadSceneMode.Additive, false);
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            
            await DOTween.To(() => currentProgress, x => currentProgress = x, 100f, 1f).OnUpdate(() =>
            {
                onProgress?.Invoke(currentProgress);
            }).AsyncWaitForCompletion().AsUniTask();
            
            sceneInstance.ActivateAsync();
            onCompleted?.Invoke();
            
            if(!string.IsNullOrEmpty(sceneNameToUnload)) 
                SceneManager.UnloadSceneAsync(sceneNameToUnload);
        }
    }
}