using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public PrisonerScript Prisoner;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(1)) {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 1.5f, Space.World);
            transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * 1.5f, Space.Self);
            
        }
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * 10.0f * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * 10.0f * Time.deltaTime, Space.Self);

        // Player Pressed Left Mouse Button
        if(Input.GetMouseButtonDown(0)) {
            // Generate Ray through screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // Cast Ray
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                // Move Prisoner to point
                Prisoner.MoveTo(hit.point);
            }
        }
    }
}
