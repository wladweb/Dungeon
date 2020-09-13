using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes/Clothes", order = 52)]
public class Clothes : ScriptableObject, Goods
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Material _material;

    public Sprite Icon => _icon;
    public Material ClothesMaterial => _material;
}
