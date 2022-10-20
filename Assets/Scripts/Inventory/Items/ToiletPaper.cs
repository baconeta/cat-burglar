namespace Inventory.Items
{
    public class ToiletPaper : CollectibleBase
    {
        protected override void PickupItem()
        {
            _cm.Achievements.CollectItem("ToiletPaper");
            base.PickupItem();
        }
    }
}