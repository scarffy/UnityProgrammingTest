using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TestAssignment
{
    public class Scene2Manager : MonoBehaviour
    {
        public static Scene2Manager Instance { get; private set; }
        
        [SerializeField] private Material material;
        [SerializeField] private TMP_InputField urlInputField;
        [SerializeField] private Button applyButton;
        
        [SerializeField] private TextMeshProUGUI statusText;

        public Action<Texture2D> OnTextureDownloaded;

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
            
        }

        private void Start()
        {
            applyButton?.onClick.AddListener(OnApplyButtonClicked);
        }

        private async void OnApplyButtonClicked()
        {
            string url = urlInputField.text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                Debug.Log("No URL provided.");
                return;
            }

            Debug.Log($"Attempting to download image from: {url}");

            Texture2D texture = await ImageDownloader.LoadTextureFromUrlAsync(
                url,
                (p)=> statusText?.SetText($"Downloading image: {(p * 100f):0}%"),
                15
                );

            if (texture != null)
            {
                material.SetTexture("_BaseMap", texture);
                statusText?.SetText("Texture applied to material successfully.");
                OnTextureDownloaded?.Invoke(texture);
                Debug.Log("Texture applied to material successfully.");
            }
            else
            {
                statusText?.SetText("Failed to load texture.");
                Debug.Log("Failed to load texture.");
            }
        }

    }
}
