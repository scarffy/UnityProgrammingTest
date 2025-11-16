using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace TestAssignment
{
    public class Spinner : MonoBehaviour
    {
        public float spinMultiplier = 5f;
        private Vector3 angularVelocity;
        private bool dragging = false;
        private Vector2 startPos;
        
        public event Action<Vector3, float> OnSpinnerSwipe;
        
        public float spinnerRadius = 1f;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
                startPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (dragging)
                {
                    Vector2 swipe = (Vector2)Input.mousePosition - startPos;

                    Ray ray = Camera.main.ScreenPointToRay(startPos);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.transform == transform)
                        {
                            angularVelocity = new Vector3(0f, swipe.x * spinMultiplier, 0f);

                            OnSpinnerSwipe?.Invoke(transform.position, swipe.magnitude);
                        }
                    }
                }
                dragging = false;
            }

            transform.Rotate(angularVelocity * Time.deltaTime, Space.World);
            angularVelocity = Vector3.Lerp(angularVelocity, Vector3.zero, 5f * Time.deltaTime);
        }
    }
}
