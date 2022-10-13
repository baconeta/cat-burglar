using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerScript : MonoBehaviour {
    // Nav Mesh Agent
    private NavMeshAgent mNavAgent;

    // Start is called before the first frame update
    void Start() {
        mNavAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position) {
        // Set Target Position
        mNavAgent.destination = position;
    }

    // Update is called once per frame
    void Update() {
        // If Prisoner is Moving
        if(mNavAgent.velocity.sqrMagnitude > Mathf.Epsilon) {
            // Face in direction of travel
            transform.rotation = Quaternion.LookRotation(mNavAgent.velocity.normalized);
        }
    }
}
