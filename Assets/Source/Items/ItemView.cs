using TMPro;
using UnityEngine;

namespace Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Sprite _icon;

        public string Name => _name.text;
    }
}