using Controllers;
using UnityEngine;

namespace Tasks
{
    /// <summary>
    /// Will be used as a drop off spot for collected items during tasks and will also handle letting the TaskManager
    /// know when an item has been dropped off.
    /// </summary>
    public class ReturnSpot : MonoBehaviour
    {
        private ControllerManager _cm;
        [SerializeField] private AudioSource dropOffSound;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // When the player runs into this - drop off all held items
            if (other.gameObject.CompareTag("Player"))
            {
                dropOffSound.Play();
                _cm.InventoryManager.DropOffItems();
            }
        }
    }
}