using CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface;
using UnityEngine;

namespace CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.AddressableDownloadableContent
{
    [System.Serializable]
    public class AddressableDownloadableContentClass : IAddressableDownloadableContent
    {
        public string URL => url;
        public string TitleFileName => titleFileName;
        public string StorageFolderName => storageFolderName;
        public string AddressableContentName => addressableContentName;

        [SerializeField] 
        private string addressableContentName;
        
        [SerializeField]
        private string url;
        
        [SerializeField]
        private string titleFileName;
        
        [SerializeField]
        private string storageFolderName;
        
        public AddressableDownloadableContentClass(string url, string titleFileName, string storageFolderName, string addressableContentName)
        {
            this.url = url;
            this.titleFileName = titleFileName;
            this.storageFolderName = storageFolderName;
            this.addressableContentName = addressableContentName;
        }

    }
}