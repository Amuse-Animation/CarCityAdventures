using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using CCA.InternetContent.LoadableContent.LoadableObject.Interface;
using Cysharp.Threading.Tasks;

namespace CCA.ContentLoader.Interface
{
    public interface IContentLoader
    {
        UniTask<ILoadableContent> DoGetLoadableContent(IDownloadableContent downloadableContent, System.Action<ILoadableContent> onDownloadComplete = null);
    }
}