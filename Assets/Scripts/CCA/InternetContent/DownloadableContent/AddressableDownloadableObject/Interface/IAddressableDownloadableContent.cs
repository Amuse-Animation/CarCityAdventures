using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;

namespace CCA.InternetContent.DownloadableContent.AddressableDownloadableObject.Interface
{
    public interface IAddressableDownloadableContent : IDownloadableContent
    {
        string AddressableContentName { get; }
    }
}