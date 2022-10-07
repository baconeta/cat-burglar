using UnityEngine;

namespace Player
{
    public class CatCamera : MonoBehaviour
    {
        public Transform playerOrientation;

        public float xSensitivity = 5f;
        public float ySensitivity = 5f;

        private float _rotationX;
        private float _rotationY;

        private void Update()
        {
            TurnCamera();
        }

        private void TurnCamera()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * ySensitivity;

            // Handle vertical look
            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            // Handle horizontal look
            _rotationY += mouseX;

            // Rotate camera and orientation player
            transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
            playerOrientation.rotation = Quaternion.Euler(0f, _rotationY, 0f);
        }
    }
}