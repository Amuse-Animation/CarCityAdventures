using CCA.ContentDownloader.Controller;
using CCA.ContentDownloader.Interface;
using CCA.DownloadableContent.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.ContentDownloader.Manager
{
    public class ContentDownloaderManager : MonoBehaviour, IContentDownloader
    {
        [SerializeField]
        private ContentDownloaderController contentDownloaderController;
        
        public async UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent)
        {
            return  await contentDownloaderController.GetDownloadableContentAsString(downloadableContent);
        }
    }
}