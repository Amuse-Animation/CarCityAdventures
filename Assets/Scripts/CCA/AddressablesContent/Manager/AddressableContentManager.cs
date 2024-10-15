using CCA.ContentDownloader.Manager;
using CCA.CustomArgsClassObjects.AddressableDownloadableObjectArgs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.AddressablesContent.Manager
{
    public class AddressableContentManager : MonoBehaviour
    {
        [SerializeField]
        private ContentDownloaderManager contentDownloaderManager;
        
        public async UniTask DownloadAddressableContentAsync(AddressableDownloadableObjectArgsClass addressableDownloadableObject)
        {
            string downloadedContent = await contentDownloaderManager.GetDownloadableContentAsString(addressableDownloadableObject);
            print(downloadedContent);
        }
    }
}