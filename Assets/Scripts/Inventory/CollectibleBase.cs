using Controllers;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Sprite))] // this is the inventory representation of the item
    public class CollectibleBase : MonoBehaviour
    {
        private InventoryManager _im;

        public string itemName;
        public bool canBePickedUp = true;
        public Sprite inventorySprite;
        public bool useParticles = true;
        public GameObject particlesPrefab;

        private void Start()
        {
            _im = FindObjectOfType<InventoryManager>();
            if (useParticles && particlesPrefab != default)
            {
                Instantiate(particlesPrefab, transform.position, particlesPrefab.transform.rotation, transform);
            }
        }

        protected virtual void PickupItem() // can be overridden if we want special/multi-stacking items for some reason
        {
            // When collected add it to the inventory then remove it from the game world
            _im.AddToInventory(this);

            Destroy(gameObject);
            // TODO Could have a cool pop up here, play a sound, hide the object from the game world, etc
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