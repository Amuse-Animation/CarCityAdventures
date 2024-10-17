using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using Cysharp.Threading.Tasks;

namespace CCA.ContentDownloader.Interface
{
    public interface IContentDownloader
    {
         UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent, System.Action<string> onDownloadComplete = null);
    }
}