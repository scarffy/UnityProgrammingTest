using System;
using UnityEngine;
using UnityEngine.VFX;

namespace TestAssignment
{
    public class VfxController : MonoBehaviour
    {
        [SerializeField] private VisualEffect vfxGraph;
        [SerializeField] private Spinner spinner;
        [SerializeField] private float deflectForce = 5f;

        private void OnEnable()
        {
            spinner.OnSpinnerSwipe += HandleSpinnerSwipe;
        }

        private void Start()
        {
            Scene2Manager.Instance.OnTextureDownloaded += UpdateTexture;
        }

        private void OnDisable()
        {
            spinner.OnSpinnerSwipe -= HandleSpinnerSwipe;
            Scene2Manager.Instance.OnTextureDownloaded -= UpdateTexture;
        }

        private void HandleSpinnerSwipe(Vector3 position, float swipeMagnitude)
        {
            if (vfxGraph != null)
            {
                vfxGraph.SetVector3("SpinnerPosition", position);
                vfxGraph.SetFloat("SpinnerForce", deflectForce * swipeMagnitude);
            }
        }

        private void UpdateTexture(Texture2D texture)
        {
            vfxGraph.SetTexture("Texture2D", texture);
            Debug.Log("Applied texture to VFX Graph");
        }
    }
}
