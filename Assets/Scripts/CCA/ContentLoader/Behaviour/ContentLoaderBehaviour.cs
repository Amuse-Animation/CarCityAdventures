using System;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.LoadDataSerializer;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;

namespace CCA.ContentLoader.Behaviour
{
    public class ContentLoaderBehaviour
    {
        public string DoGetLoadableContentData(IDownloadableContent downloadableContent, Action<string> onDownloadComplete = null)
        {
            string data = LoadDataSerializerStaticClass.GetDownloadedFileDataFromDisk(downloadableContent.TitleFileName, downloadableContent.StorageFolderName);
            onDownloadComplete?.Invoke(data);
            return data;
        }
    }
}