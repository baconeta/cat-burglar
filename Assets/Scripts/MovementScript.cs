using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    private Animator mAnimator;
    public Vector3 mVelocity;
    private NavMeshAgent NPC;
    public float WalkSpeed = 3.0f;
    public float RunSpeed = 5.0f;
    public float SprintSpeed = 8.0f;

    float speedy;

    // Start is called before the first frame update
    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        NPC = GetComponent<NavMeshAgent>();
        mVelocity = Vector3.zero;
        mAnimator.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    private void Update()
    {
        // //horizontal movement
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // //update vel
        // mVelocity.x = move.x * speedy;
        // mVelocity.z = move.z * speedy;
        // mVelocity.y -= 9.8f * Time.deltaTime;

        // //update animation
        // mAnimator.SetFloat("VelocityX", move.x);
        // mAnimator.SetFloat("VelocityZ", move.z);
        // mAnimator.SetFloat("MoveSpeed", speedy / 5.0f);
        mAnimator.SetFloat("MoveSpeed", NPC.velocity.magnitude);
        if(NPC.velocity.magnitude > 0f){
            mAnimator.SetBool("IsMoving", true);
        }
    }
}