using System;
using CCA.InternetContent.DownloadableContent.DownloadableObject.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CCA.ContentDownloader.Behaviour
{
    public class ContentDownloaderBehaviour
    {
        public async UniTask<string> GetDownloadableContentAsString(IDownloadableContent downloadableContent, System.Action<string> callback = null)
        {
            try
            {
                UniTaskCompletionSource<string> taskCompletionSource = new UniTaskCompletionSource<string>();
                UnityWebRequest webRequest = UnityWebRequest.Get(downloadableContent.URL);
                UnityWebRequestAsyncOperation asyncWebRequest = webRequest.SendWebRequest();

                asyncWebRequest.completed += operation =>
                {

                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                            break;
                        case UnityWebRequest.Result.Success:
                        {
                            taskCompletionSource.TrySetResult(webRequest.downloadHandler.text);
                            callback?.Invoke(webRequest.downloadHandler.text);
                        }
                            break;
                        case UnityWebRequest.Result.ConnectionError:
                            taskCompletionSource.TrySetException(new Exception($"{webRequest.error} - Could not download content async! - Path: {downloadableContent.URL} Response code {webRequest.responseCode}"));
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            taskCompletionSource.TrySetException(new Exception($"{webRequest.error} - Could not load content catalog async! - Path: {downloadableContent.URL} Response code {webRequest.responseCode}"));
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                };

                await asyncWebRequest.ToUniTask(Progress.Create<float>(x => Debug.Log(x)));
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