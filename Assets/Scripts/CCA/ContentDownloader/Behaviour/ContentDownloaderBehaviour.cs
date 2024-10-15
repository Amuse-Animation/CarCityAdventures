using System;
using CCA.DownloadableContent.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CCA.ContentDownloader.Behaviour
{
    public class ContentDownloaderBehaviour
    {
        public async UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent)
        {
            try
            {
                UnityWebRequest webRequest = UnityWebRequest.Get(downloadableContent.URL);
                UnityWebRequestAsyncOperation asyncWebRequest = webRequest.SendWebRequest();

                await asyncWebRequest.ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"{webRequest.result} - Could not load content catalog async! - Path: {downloadableContent.URL}");
                    return string.Empty;
                }
                
                return webRequest.downloadHandler.text;

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
            
        }
    }
}