using Controllers;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Sprite))] // this is the inventory representation of the item
    public class CollectibleBase : MonoBehaviour
    {
        public string itemName;
        public bool canBePickedUp = true;
        public Sprite inventorySprite;
        public bool useParticles = true;
        public GameObject particlesPrefab;

        private ControllerManager _cm;
        private InventoryManager _im;
        private AudioSource _pickupSfxSource;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
            _im = _cm.InventoryManager;

            if (useParticles && particlesPrefab != default)
            {
                Instantiate(particlesPrefab, transform.position, particlesPrefab.transform.rotation, transform);
            }

            _pickupSfxSource = GameObject.FindGameObjectWithTag("Pickup").GetComponent<AudioSource>();
        }

        private void PickupItem() // can be overridden if we want special/multi-stacking items for some reason
        {
            // When collected, add it to the inventory and remove it from the game world
            _im.AddToInventory(this);
            _cm.Achievements.CollectItem(GetType().Name);
            _pickupSfxSource.Play();

            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            // We want to pick up the item from the game world if the player collides with it
            if (other.gameObject.CompareTag("Player") && canBePickedUp)
            {
                PickupItem();
            }
        }
    }
}