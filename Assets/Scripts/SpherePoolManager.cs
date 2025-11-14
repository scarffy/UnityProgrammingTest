using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAssignment
{
    public class SpherePoolManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spheres = new List<GameObject>();
        
        private void Awake()
        {
            foreach (var sphere in spheres)
            {
                if (sphere != null) sphere.SetActive(false);
            }
        }


        public void ShowAll(bool show)
        {
            foreach (var sphere in spheres)
            {
                if (sphere != null && sphere.activeSelf != show) sphere.SetActive(show);
            }
        }
    }
}
