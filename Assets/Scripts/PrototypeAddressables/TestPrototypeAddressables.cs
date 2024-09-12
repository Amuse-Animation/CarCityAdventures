using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PrototypeAddressables
{
    public class TestPrototypeAddressables : MonoBehaviour
    {
        private void Start()
        {
            Addressables.InitializeAsync().Completed += AddressablesReady;
        }

        private void AddressablesReady(AsyncOperationHandle<IResourceLocator> obj)
        {
            // string wea = "Assets/Scenes/Testing/Code.unity";
            //
            // Addressables.LoadSceneAsync(wea, LoadSceneMode.Single).Completed += (x) =>
            // {
            //     Debug.Log("Todo Ok");
            // };

            AsyncOperationHandle<IResourceLocator> loadContentCatlogAsync =
                Addressables.LoadContentCatalogAsync(
                    @"C:\Users\OlafGomez\Documents\AmuseProjects\CarCityHealer\AddressablesShit\Catalog\catalog_1.1.json");

            loadContentCatlogAsync.Completed += CatlogLoaded;
        }

        private void CatlogLoaded(AsyncOperationHandle<IResourceLocator> obj)
        {
            // string wea = "Assets/Scenes/Testing/Code.unity";
            //
            // Addressables.LoadSceneAsync(wea, LoadSceneMode.Single).Completed += (x) =>
            // {
            //     Debug.Log("Todo Ok");
            // };
            IResourceLocator resourceLocator = obj.Result;
            resourceLocator.Locate("Assets/Scenes/LoadingScene/LoadingScene.unity", typeof(SceneInstance), out IList<IResourceLocation> locations);
            if (locations != null && locations.Count > 0)
            {
                Addressables.LoadSceneAsync(locations[0], LoadSceneMode.Additive);
            }
        }
    }
}