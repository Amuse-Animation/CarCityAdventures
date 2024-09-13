using AmuseEngine.Assets.Scripts.HelplerStaticClasses.AddressablesLoader;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PrototypeAddressables
{
    public class TestPrototypeAddressables : MonoBehaviour
    {

        private async UniTaskVoid Start()
        {
            // string catalogPath = @"C:\Users\o.gomez\Documents\UnityProjects\CarCityHealer\CarCityHealer\AddressablesShit\Catalog\catalog_1.1.json";
            string catalogPath = "https://cdn.mini-mango.com/AddressablesShit/Android/catalog_1.1.json";

            try
            {
                await AddressablesLoaderStaticClass.InitAddressablesAsyncWithAwait();
                //await AddressablesLoaderStaticClass.LoadContentCatalogAsync(catalogPath, (catalogLoaded) =>
                //{
                //    print("Finished");
                //    catalogLoaded.Locate("Assets/Scenes/LoadingScene/LoadingScene.unity", typeof(SceneInstance), out IList<IResourceLocation> locations);
                //    if (locations != null && locations.Count > 0)
                //    {
                //        Addressables.LoadSceneAsync(locations[0], LoadSceneMode.Additive);
                //    }
                //});

                // Addressables.LoadContentCatalog()
                // string 

                var webRequest = (await UnityWebRequest.Get(catalogPath).SendWebRequest()).downloadHandler.text;
                print(webRequest);

            }
            catch (Exception exception)
            {

                Debug.LogError($"{exception.Message} - Could not load content catalog async! - Path: {catalogPath}");
            }

            //var webRequest = UnityWebRequest.Get("");
            //await webRequest.SendWebRequest();


        }
    }
}
