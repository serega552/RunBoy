using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private List<ItemView> _items = new List<ItemView>();

    public event Action OnItemsClearedDueToMismatch;
    public event Action OnItemsClearedDueToMatch;

    public void AddItem(ItemView newItem, OtherItem otherItem)
    {
        if (_items.Any() && _items.Any(existingItem => existingItem.Name != newItem.Name))
        {
            ActivationDeboost(otherItem);
        }

        if (!_items.Any() || (_items.All(existingItem => existingItem.Name == newItem.Name) && _items.Count < 3))
        {
            _items.Add(newItem);

            if (_items.Count == 3)
            {
                ActivationBoost(otherItem);
            }
        }
    }

    private void ActivationBoost(OtherItem otherItem)
    {
        otherItem.Boost();
        ClearPanel();
        OnItemsClearedDueToMatch?.Invoke();
    }

    private void ActivationDeboost(OtherItem otherItem)
    {
        otherItem.DeBoost();
        ClearPanel();
        OnItemsClearedDueToMismatch?.Invoke();
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
