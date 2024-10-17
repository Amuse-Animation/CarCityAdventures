using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using UnityEngine;

namespace CCA.InternetContent.DownloadableContent.DownloadableObject.DownloadableObjectContent
{
    [System.Serializable]
    public class DownloadableObjectContentClass : IDownloadableContent
    {
        public string URL => url;
        public string TitleFileName => titleFileName;
        public string StorageFolderName => storageFolderName;

        [SerializeField]
        private string url;
        
        [SerializeField]
        private string titleFileName;
        
        [SerializeField]
        private string storageFolderName;

        public DownloadableObjectContentClass(string url, string titleFileName, string storageFolderName)
        {
            this.url = url;
            this.titleFileName = titleFileName;
            this.storageFolderName = storageFolderName;
        }
    }
}