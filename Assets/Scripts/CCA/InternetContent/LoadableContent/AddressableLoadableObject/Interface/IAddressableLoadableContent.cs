using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using UnityEngine.AddressableAssets.ResourceLocators;

namespace CCA.InternetContent.LoadableContent.AddressableLoadableObject.Interface
{
    public interface IAddressableLoadableContent : ILoadableContent
    {
        IResourceLocator ResourceLocator { get; }
        string AddressableContentName { get; }
    }
}