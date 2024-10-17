namespace CCA.InternetContent.LoadableContent.LoadableObject.Interface
{
    public interface ILoadableContent
    {
        string SourceURL { get; }
        
        string TitleFileName { get; }
        
        string StorageFolderName { get; }

        string ContentText { get; }
    }
}