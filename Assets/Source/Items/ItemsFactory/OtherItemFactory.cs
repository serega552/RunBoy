using Chunks;
using Items.OtherItems;
using Player;
using UnityEngine;

namespace Items.ItemsFactory
{
    public class OtherItemFactory : ItemFactory
    {
        [SerializeField] private ItemViewSpawner _itemViewSpawner;

        public override GameObject CreateItem(GameObject prefab, Chunk chunk)
        {
            GameObject itemObject = base.CreateItem(prefab, chunk);

            if (itemObject.TryGetComponent(out OtherItem otherItemComponent))
            {
                otherItemComponent.Init(_itemViewSpawner);
            }

            return itemObject;
        }
    }
}