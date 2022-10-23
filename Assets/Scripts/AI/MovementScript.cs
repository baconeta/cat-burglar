using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class MovementScript : MonoBehaviour
    {
        private Animator _mAnimator;
        public Vector3 mVelocity;
        private NavMeshAgent NPC;
        public float walkSpeed = 3.0f;
        public float runSpeed = 5.0f;
        public float sprintSpeed = 8.0f;

        private float _speedy;
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        // Start is called before the first frame update
        private void Start()
        {
            _mAnimator = GetComponent<Animator>();
            NPC = GetComponent<NavMeshAgent>();
            mVelocity = Vector3.zero;
            _mAnimator.SetBool(IsGrounded, true);
        }

        // Update is called once per frame
        private void Update()
        {
            _mAnimator.SetFloat(MoveSpeed, NPC.velocity.magnitude);
            if (NPC.velocity.magnitude > 0f)
            {
                _mAnimator.SetBool(IsMoving, true);
            }
        }
    }
}