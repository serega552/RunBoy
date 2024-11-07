using UnityEngine;

public class Dance : Product
{
    [SerializeField] private string _nameDanceAnim;

    public string NameDanceAnim => _nameDanceAnim;
}
