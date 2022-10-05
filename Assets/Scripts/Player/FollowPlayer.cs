using UnityEngine;

namespace Player
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform cameraPos;

        private void Update()
        {
            transform.position = cameraPos.position;
        }
    }
}