using CCA.DownloadableContent.Interface;
using Cysharp.Threading.Tasks;

namespace CCA.ContentDownloader.Interface
{
    public interface IContentDownloader
    {
         UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent);
    }
}