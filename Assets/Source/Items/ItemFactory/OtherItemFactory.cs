using UnityEngine;

public class OtherItemFactory : ItemFactory
{
    [SerializeField] private ItemViewSpawner _itemViewSpawner;

    public override GameObject CreateItem(GameObject prefab, Chunk chunk, PlayerView player)
    {
        GameObject itemObject = base.CreateItem(prefab, chunk, player);
        OtherItem otherItemComponent = itemObject.GetComponent<OtherItem>();

        if (otherItemComponent != null)
        {
            otherItemComponent.Init(_itemViewSpawner);
        }

        return itemObject;
    }
}
