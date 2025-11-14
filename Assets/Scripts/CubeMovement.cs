using System;
using UnityEngine;

namespace TestAssignment
{
    public class CubeMovement : MonoBehaviour
    {
        private enum ControllerType
        {
            WASD = 0,
            Arrow = 1
        }

        [SerializeField] private ControllerType controllerType;
        [SerializeField] private float speed = 5f;
        
        [SerializeField] private Vector3 minBounds = new Vector3(-5f, 0f, -5f);
        [SerializeField] private Vector3 maxBounds = new Vector3(5f, 0f, 5f);

        private void Update()
        {
            Vector3 input = Vector3.zero;

            if (controllerType == ControllerType.WASD)
            {
                if (Input.GetKey(KeyCode.W)) input += Vector3.forward;
                if (Input.GetKey(KeyCode.S)) input += Vector3.back;
                if (Input.GetKey(KeyCode.A)) input += Vector3.left;
                if (Input.GetKey(KeyCode.D)) input += Vector3.right;
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow)) input += Vector3.forward;
                if (Input.GetKey(KeyCode.DownArrow)) input += Vector3.back;
                if (Input.GetKey(KeyCode.LeftArrow)) input += Vector3.left;
                if (Input.GetKey(KeyCode.RightArrow)) input += Vector3.right;
            }

            if (input.sqrMagnitude > 0f)
            {
                input = input.normalized * speed * Time.deltaTime;
                Vector3 newPosition = transform.position + new Vector3(input.x, 0f, input.z);
                
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

                transform.position = newPosition;
            }
        }
    }
}
