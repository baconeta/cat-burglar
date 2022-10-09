using UnityEngine;

namespace Inventory.Items
{
    public class Apple : CollectibleBase
    {
        public void OnTriggerEnter(Collider other)
        {
            // We want to pick up the item from the game world if the player collides with it
            if (other.gameObject.CompareTag("Player"))
            {
                PickupItem();
            }
        }
    }
}