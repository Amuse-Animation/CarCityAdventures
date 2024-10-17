using System;
using CCA.ContentLoader.Controller;
using CCA.ContentLoader.Interface;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.ContentLoader.Manager
{
    public class ContentLoaderManager : MonoBehaviour, IContentLoader
    {
        [SerializeField]
        private ContentLoaderController contentLoaderController;
        
        public UniTask<ILoadableContent> DoGetLoadableContent(IDownloadableContent downloadableContent, Action<ILoadableContent> onDownloadComplete = null)
        {
            return contentLoaderController.DoGetLoadableContent(downloadableContent, onDownloadComplete);
        }
    }
}