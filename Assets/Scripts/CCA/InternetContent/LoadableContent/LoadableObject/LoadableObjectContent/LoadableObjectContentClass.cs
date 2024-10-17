using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using UnityEngine;

namespace CCA.InternetContent.LoadableContent.LoadableObject.LoadableObjectContent
{
    [System.Serializable]
    public class LoadableObjectContentClass : ILoadableContent
    {
        public string SourceURL => sourceURL;
        public string TitleFileName => titleFileName;
        public string StorageFolderName => storageFolderName;
        public string ContentText => contentText;

        [SerializeField] 
        private string sourceURL;
        
        [SerializeField]
        private string titleFileName;
        
        [SerializeField]
        private string storageFolderName;

        private string contentText;

        public LoadableObjectContentClass(string sourceURL, string titleFileName, string storageFolderName, string contentText)
        {
            this.sourceURL = sourceURL;
            this.titleFileName = titleFileName;
            this.storageFolderName = storageFolderName;
            this.contentText = contentText;
        }
    }
}