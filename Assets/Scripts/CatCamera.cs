using UnityEngine;

public class CatCamera : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ControllerManager cm;
    public float turnSpeed = 5f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float _rotationX;

    private void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!cm) cm = FindObjectOfType<ControllerManager>();
    }


    private void FixedUpdate()
    {
        TurnCamera();
    }

    private void TurnCamera()
    {
        // get the mouse inputs
        var y = Input.GetAxis("Mouse X") * turnSpeed;
        _rotationX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        _rotationX = Mathf.Clamp(_rotationX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-_rotationX, transform.eulerAngles.y + y, 0);
    }
}