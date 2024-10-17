using CCA.AddressablesContent.Behaviour;
using CCA.ContentDownloader.Interface;
using CCA.ContentLoader.Interface;
using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CCA.AddressablesContent.Controller
{
    public class AddressableContentController : MonoBehaviour
    {
        private AddressableContentBehaviour addressableContentBehaviour;

        public void InitAddressables()
        {
            addressableContentBehaviour = new AddressableContentBehaviour();
            addressableContentBehaviour.InitAddressables().Forget();
        }

        public UniTask<string> DownloadAddressableContent(IContentDownloader contentDownloader, IAddressableDownloadableContent addressableContent, System.Action<string> callback = null)
        {
            return addressableContentBehaviour.DownloadAddressableContentAsync(contentDownloader, addressableContent, callback);
        }
        
        public string SaveDownloadedAddressableContent(string addressableDownloadedContent, string fileName, string folderName)
        {
            return addressableContentBehaviour.SaveDownloadedAddressableContent(addressableDownloadedContent, fileName, folderName);
        }
        
        public UniTask<IAddressableLoadableContent> LoadDownloadedAddressableCatalogContent(IContentLoader contentLoader, IAddressableDownloadableContent addressableDownloadableObject, System.Action<IAddressableLoadableContent> onCompleteCalback = null)
        {
            return addressableContentBehaviour.LoadDownloadedAddressableCatalogContent(contentLoader, addressableDownloadableObject, onCompleteCalback);
        }
        
        public UniTask<SceneInstance> LoadAddressableSceneInCatalogContent(IAddressableLoadableContent addressableLoadableContent, LoadSceneMode loadSceneMode)
        {
            return addressableContentBehaviour.LoadAddressableSceneInCatalogContent(addressableLoadableContent, loadSceneMode);
        }
    }
}