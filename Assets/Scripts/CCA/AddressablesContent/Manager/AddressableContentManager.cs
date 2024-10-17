using System;
using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.AddressablesLoader;
using CCA.AddressablesContent.Controller;
using CCA.ContentDownloader.Manager;
using CCA.ContentLoader.Manager;
using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CCA.AddressablesContent.Manager
{
    public class AddressableContentManager : MonoBehaviour
    {
        [SerializeField]
        private ContentDownloaderManager contentDownloaderManager;
        
        [SerializeField]
        private ContentLoaderManager contentLoaderManager;

        [SerializeField]
        private AddressableContentController addressableContentController;

        private void Awake()
        {
            addressableContentController.InitAddressables();
        }

        public UniTask<string> DoDownloadAddressableContentAsync(IAddressableDownloadableContent addressableDownloadableObject, System.Action<string> onCompleteCalback = null)
        {
            return addressableContentController.DownloadAddressableContent(contentDownloaderManager, addressableDownloadableObject, onCompleteCalback);
        }

        public string DoSaveDownloadedAddressableContent(string addressableDownloadedContent, IAddressableDownloadableContent addressableDownloadableObject)
        {
            return addressableContentController.SaveDownloadedAddressableContent(addressableDownloadedContent, addressableDownloadableObject.TitleFileName, addressableDownloadableObject.StorageFolderName);
        }

        public UniTask<IAddressableLoadableContent> LoadDownloadedAddressableCatalogContent(IAddressableDownloadableContent addressableDownloadableObject, System.Action<IAddressableLoadableContent> onCompleteCalback = null)
        {
            return addressableContentController.LoadDownloadedAddressableCatalogContent(contentLoaderManager,addressableDownloadableObject, onCompleteCalback);
        }

        public UniTask<SceneInstance> LoadAddressableSceneInCatalogContent(IAddressableLoadableContent addressableLoadableContent, LoadSceneMode loadSceneMode)
        {
            return addressableContentController.LoadAddressableSceneInCatalogContent(addressableLoadableContent, loadSceneMode);
        }
    }
}