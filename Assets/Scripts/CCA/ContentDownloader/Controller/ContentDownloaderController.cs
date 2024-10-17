using CCA.ContentDownloader.Behaviour;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.ContentDownloader.Controller
{
    public class ContentDownloaderController : MonoBehaviour
    {
        private ContentDownloaderBehaviour contentDownloaderBehaviour;

        private void Awake()
        {
            contentDownloaderBehaviour = new ContentDownloaderBehaviour();
        }

        public UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent, System.Action<string> onDownloadComplete = null)
        {
            return contentDownloaderBehaviour.GetDownloadableContentAsString(downloadableContent, onDownloadComplete);
        }
    }
}