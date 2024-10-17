using System;
using CCA.ContentLoader.Behaviour;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.LoadableObjectContent;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCA.ContentLoader.Controller
{
    public class ContentLoaderController : MonoBehaviour
    {
        private ContentLoaderBehaviour contentLoaderBehaviour;

        private void Awake()
        {
            contentLoaderBehaviour = new ContentLoaderBehaviour();
        }

        public UniTask<ILoadableContent> DoGetLoadableContent(IDownloadableContent downloadableContent, Action<ILoadableContent> onDownloadComplete = null)
        {
            string loadableContentData = contentLoaderBehaviour.DoGetLoadableContentData(downloadableContent);
            ILoadableContent loadableContent = new LoadableObjectContentClass(downloadableContent.URL, downloadableContent.TitleFileName, downloadableContent.StorageFolderName, loadableContentData);
            onDownloadComplete?.Invoke(loadableContent);
            return UniTask.FromResult(loadableContent);
        }
    }
}