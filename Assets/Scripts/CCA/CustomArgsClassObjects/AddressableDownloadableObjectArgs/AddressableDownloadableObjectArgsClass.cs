using CCA.CustomArgsClassObjects.DownloadableObjectArgs;
using UnityEngine;

namespace CCA.CustomArgsClassObjects.AddressableDownloadableObjectArgs
{
    [System.Serializable]
    public class AddressableDownloadableObjectArgsClass : DownloadableObjectArgsClass
    {
        public string AddressableContentName => addressableContentName;

        [SerializeField] 
        private string addressableContentName;
        
        public AddressableDownloadableObjectArgsClass(string url, string titleFileName, string storageFolderName, string addressableContentName) : base(url, titleFileName, storageFolderName)
        {
            this.addressableContentName = addressableContentName;
        }
    }
}