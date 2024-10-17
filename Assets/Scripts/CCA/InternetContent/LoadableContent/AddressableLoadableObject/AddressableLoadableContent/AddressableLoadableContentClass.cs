using CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;

namespace CCA.InternetContent.LoadableContent.AddressableLoadableObject.AddressableLoadableContent
{
    public class AddressableLoadableContentClass : IAddressableLoadableContent
    {
        public string SourceURL => sourceURL;
        public string TitleFileName => titleFileName;
        public string StorageFolderName => storageFolderName;
        public string ContentText => contentText;
        public IResourceLocator ResourceLocator => resourceLocator;
        public string AddressableContentName => addressableContentName;

        [SerializeField] 
        private string sourceURL;
        
        [SerializeField]
        private string titleFileName;
        
        [SerializeField]
        private string storageFolderName;

        private string contentText;
        private IResourceLocator resourceLocator;
        private string addressableContentName;

        public AddressableLoadableContentClass(string sourceURL, string titleFileName, string storageFolderName, string contentText, IResourceLocator resourceLocator, string addressableContentName)
        {
            this.sourceURL = sourceURL;
            this.titleFileName = titleFileName;
            this.storageFolderName = storageFolderName;
            this.contentText = contentText;
            this.resourceLocator = resourceLocator;
            this.addressableContentName = addressableContentName;
        }

        public AddressableLoadableContentClass(ILoadableContent loadableContent, IResourceLocator resourceLocator, string addressableContentName)
        {
            this.sourceURL = loadableContent.SourceURL;
            this.titleFileName = loadableContent.TitleFileName;
            this.storageFolderName = loadableContent.StorageFolderName;
            this.contentText = loadableContent.ContentText;
            this.resourceLocator = resourceLocator;
            this.addressableContentName = addressableContentName;
        }
    }
}