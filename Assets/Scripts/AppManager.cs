using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAssignment
{
    public class AppManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
