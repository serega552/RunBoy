using TMPro;
using UnityEngine;

public class BoostView : MonoBehaviour
{
    [SerializeField] private Boost _boost;
    [SerializeField] private TMP_Text _countBoostsText;

    private void OnEnable()
    {
        _boost.OnUpdateCount += UpdateText;
    }

    private void OnDisable()
    {
        _boost.OnUpdateCount -= UpdateText;
    }

    private void UpdateText()
    {
        _countBoostsText.text = _boost.Count.ToString();
    }
}
