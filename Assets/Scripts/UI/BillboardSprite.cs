using UnityEngine;

namespace UI
{
    public class BillboardSprite : MonoBehaviour
    {
        private Camera _mainCamera;
        public float maxHeight = 1f;
        public float minHeight = -1f;
        public float lerpSpeed = 1;
        private float _t;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(0f, _mainCamera.transform.rotation.eulerAngles.y, 0f);

            transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, _t),
                transform.position.z);
            _t += Time.deltaTime;

            if (_t > 1.0f / lerpSpeed)
            {
                (maxHeight, minHeight) = (minHeight, maxHeight);
                _t = 0.0f;
            }
        }
    }
}