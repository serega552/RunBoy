using System;
using System.Collections.Generic;
using UnityEngine;
using Items.OtherItems;
using System.Linq;

namespace Items.ItemsFactory
{
    public class ItemViewSpawner : MonoBehaviour
    {
        [SerializeField] private List<ItemView> _items = new List<ItemView>();
        [SerializeField] private Transform _targetPanel;
        [SerializeField] private ItemController _itemController;

        public void Spawn(OtherItem item)
        {
            var exists = _items.Any(itemView => itemView.Name.Trim().Equals(item.name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                var itemViewToSpawn = _items.First(iv => iv.Name.Trim().Equals(item.name.Trim(), StringComparison.OrdinalIgnoreCase));
                var instance = Instantiate(itemViewToSpawn, _targetPanel);
                _itemController.AddItem(instance, item);
            }
        }
    }
}