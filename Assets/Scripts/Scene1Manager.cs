using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace TestAssignment
{
    public class Scene1Manager : MonoBehaviour
    {
        [SerializeField] private Transform redCube;
        [SerializeField] private Transform greenCube;
        [SerializeField] private SpherePoolManager spherePool;
        [SerializeField] private TextMeshProUGUI distanceText;
        
        private void Update()
        {
            
            float dist = Vector3.Distance(redCube.position, greenCube.position);
            distanceText.text = $"Distance {dist:F2} m";
            
            if (dist < 2f)
            {
                spherePool.ShowAll(true);
            }
            else
            {
                spherePool.ShowAll(false);
            }


            if (dist < 1f)
            {
                // SceneManager.LoadScene("Scene2");
            }
        }
    }
}
