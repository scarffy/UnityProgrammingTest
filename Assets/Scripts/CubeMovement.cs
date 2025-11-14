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
                transform.position += new Vector3(input.x, 0f, input.z);
            }
        }
    }
}
