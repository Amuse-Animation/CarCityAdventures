using CCA.DownloadableContent.Interface;
using UnityEngine;

namespace CCA.CustomArgsClassObjects.DownloadableObjectArgs
{
    [System.Serializable]
    public class DownloadableObjectArgsClass : IDownloadableContent
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

        public DownloadableObjectArgsClass(string url, string titleFileName, string storageFolderName)
        {
            this.url = url;
            this.titleFileName = titleFileName;
            this.storageFolderName = storageFolderName;
        }
    }
}