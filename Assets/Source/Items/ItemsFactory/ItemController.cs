using System;
using System.Collections.Generic;
using System.Linq;
using Items.OtherItems;
using UnityEngine;

namespace Items.ItemsFactory
{
    public class ItemController : MonoBehaviour
    {
        private readonly List<ItemView> _items = new List<ItemView>();
        private readonly float _itemsCount = 3;

        public void AddItem(ItemView newItem, OtherItem otherItem)
        {
            if (_items.Any() && _items.Any(existingItem => existingItem.Name != newItem.Name))
            {
                ActivationDeboost(otherItem);
            }

            if (!_items.Any() || (_items.All(existingItem => existingItem.Name == newItem.Name) && _items.Count < _itemsCount))
            {
                _items.Add(newItem);

                if (_items.Count == _itemsCount)
                {
                    ActivationBoost(otherItem);
                }
            }
        }

        private void ActivationBoost(OtherItem otherItem)
        {
            otherItem.Boost();
            ClearPanel();
        }

        private void ActivationDeboost(OtherItem otherItem)
        {
            otherItem.DeBoost();
            ClearPanel();
        }

        private void ClearPanel()
        {
            foreach (var itemView in _items)
            {
                Destroy(itemView.gameObject);
            }

            _items.Clear();
        }
    }
}