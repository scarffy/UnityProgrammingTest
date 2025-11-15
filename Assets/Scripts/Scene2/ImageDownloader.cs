using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestAssignment
{
    public static class ImageDownloader
    {
        private static readonly string[] validExtensions = 
            { ".jpg", ".jpeg", ".png" };

        public static async Task<Texture2D> LoadTextureFromUrlAsync(
            string url,
            Action<float> onProgress = null,
            int timeoutSeconds = 15)
        {
            if (!IsValidImageUrl(url))
            {
                Debug.LogWarning($"Invalid image URL: {url}");
                return null;
            }

            Debug.Log($"[Downloader] Starting download: {url}");

            using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
            {
                req.timeout = timeoutSeconds;

                var op = req.SendWebRequest();

                float lastProgress = -1f;
                
                while (!op.isDone)
                {
                    float progress = req.downloadProgress;

                    if (!Mathf.Approximately(progress, lastProgress))
                    {
                        lastProgress = progress;
                        onProgress?.Invoke(req.downloadProgress);
                        Debug.Log($"[Downloader] Progress: {req.downloadProgress * 100f:0}%");
                    }
                    
                    await Task.Yield();
                }

                if (!Mathf.Approximately(lastProgress, 1f))
                {
                    onProgress?.Invoke(1f);
                    Debug.Log("[Downloader] Progress: 100%");
                }

                if (req.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"[Downloader] Error: {req.error}");
                    return null;
                }

                Debug.Log("[Downloader] Download complete!");
                return DownloadHandlerTexture.GetContent(req);
            }
        }

        private static bool IsValidImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            string ext = System.IO.Path.GetExtension(url);

            foreach (var e in validExtensions)
            {
                if (ext.Equals(e, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}