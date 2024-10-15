namespace CCA.DownloadableContent.Interface
{
    public interface IDownloadableContent
    {
        string URL { get; }
        
        string TitleFileName { get; }
        
        string StorageFolderName { get; }
    }
}