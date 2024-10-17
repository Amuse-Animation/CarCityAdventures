using System;
using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.AddressablesLoader;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.LoadDataSerializer;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.SaveDataSerializer;
using CCA.ContentDownloader.Interface;
using CCA.ContentLoader.Interface;
using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.AddressableLoadableObject.AddressableLoadableContent;
using CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CCA.AddressablesContent.Behaviour
{
    public class AddressableContentBehaviour
    {
        public async UniTaskVoid InitAddressables()
        {
            try
            {
                await AddressablesLoaderStaticClass.InitAddressablesAsyncWithAwaitUniTask();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                throw;
            }
        }
        
        public UniTask<string> DownloadAddressableContentAsync(IContentDownloader contentDownloader,IAddressableDownloadableContent addressableDownloadableObject, System.Action<string> onDownloadComplete = null)
        {
            try
            {
                return contentDownloader.GetDownloadableContentAsString(addressableDownloadableObject, onDownloadComplete);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        public string SaveDownloadedAddressableContent(string addressableDownloadedContent, string fileName, string folderName)
        {
            return SaveDataSerializerStaticClass.WriteDownloadedJsonFileToDisk(addressableDownloadedContent, fileName, folderName);
        }

        public async UniTask<IAddressableLoadableContent> LoadDownloadedAddressableCatalogContent(IContentLoader contentLoader, IAddressableDownloadableContent addressableDownloadableObject, System.Action<IAddressableLoadableContent> onCompleteCalback = null)
        {
            try
            {
                ILoadableContent loadableContent = await contentLoader.DoGetLoadableContent(addressableDownloadableObject);
                string filePath = LoadDataSerializerStaticClass.GetDownloadedFilePathFromDisk(addressableDownloadableObject.TitleFileName, addressableDownloadableObject.StorageFolderName);
                IResourceLocator resourceLocator = await AddressablesLoaderStaticClass.LoadContentCatalogAsyncUniTask(filePath);
                IAddressableLoadableContent addressableLoadableContent = new AddressableLoadableContentClass(loadableContent, resourceLocator, addressableDownloadableObject.AddressableContentName);
                
                onCompleteCalback?.Invoke(addressableLoadableContent);
                return addressableLoadableContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public UniTask<SceneInstance> LoadAddressableSceneInCatalogContent(IAddressableLoadableContent addressableLoadableContent, LoadSceneMode loadSceneMode)
        {
            try
            {
                addressableLoadableContent.ResourceLocator.Locate(addressableLoadableContent.AddressableContentName, typeof(SceneInstance), out IList<IResourceLocation> locations);
                if (locations != null && locations.Count > 0)
                {
                    AsyncOperationHandle<SceneInstance> asyncOp = Addressables.LoadSceneAsync(locations[0], loadSceneMode, false); 
                    return asyncOp.ToUniTask(Progress.Create<float>(x => Debug.Log(asyncOp.PercentComplete)));
                }

                return default;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
    }
}