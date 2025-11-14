using System;
using UnityEngine;

namespace TestAssignment
{
    public class CubeMovement : MonoBehaviour
    {
        private enum ControlScheme
        {
            WASD = 0,
            ArrowKeys = 1
        }

        [Header("Player Settings")]
        [SerializeField] private ControlScheme controlScheme;
        [SerializeField] private float speed = 5f;
        
        [Header("Movement Bounds")]
        [SerializeField] private Vector3 minBounds = new Vector3(-5f, 0f, -5f);
        [SerializeField] private Vector3 maxBounds = new Vector3(5f, 0f, 5f);

        private void Update()
        {
            Vector3 input = GetInput();
            Movement();
        }

        private void Movement()
        {
            Vector3 input = GetInput();

            if (input.sqrMagnitude > 0f)
            {
                Vector3 movement = input.normalized * speed * Time.deltaTime;
                Vector3 newPosition = transform.position + movement;

                // Clamp within bounds
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
                newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

                transform.position = newPosition;
            }
        }
        
        private Vector3 GetInput()
        {
            float horizontal = 0f;
            float vertical = 0f;

            switch (controlScheme)
            {
                case ControlScheme.WASD:
                    horizontal = (Input.GetKey(KeyCode.D) ? 1f : 0f) +
                                 (Input.GetKey(KeyCode.A) ? -1f : 0f);
                    vertical = (Input.GetKey(KeyCode.W) ? 1f : 0f) +
                               (Input.GetKey(KeyCode.S) ? -1f : 0f);
                    break;

                case ControlScheme.ArrowKeys:
                    horizontal = (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f) +
                                 (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f);
                    vertical = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) +
                               (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
                    break;
            }

            return new Vector3(horizontal, 0f, vertical);
        }
    }
}
