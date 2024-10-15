using CCA.ContentDownloader.Behaviour;
using CCA.DownloadableContent.Interface;
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

        public async UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent)
        {
            return await contentDownloaderBehaviour.GetDownloadableContentAsString(downloadableContent);
        }
    }
}