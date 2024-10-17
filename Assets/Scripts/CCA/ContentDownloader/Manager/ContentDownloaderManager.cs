using CCA.ContentDownloader.Controller;
using CCA.ContentDownloader.Interface;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.ContentDownloader.Manager
{
    public class ContentDownloaderManager : MonoBehaviour, IContentDownloader
    {
        [SerializeField]
        private ContentDownloaderController contentDownloaderController;
        
        public UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent, System.Action<string> onDownloadComplete = null)
        {
            return contentDownloaderController.GetDownloadableContentAsString(downloadableContent, onDownloadComplete);
        }
    }
}