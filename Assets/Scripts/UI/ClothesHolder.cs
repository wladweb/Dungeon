using UnityEngine;

public class ClothesHolder : MonoBehaviour, GoodsHolder
{
    [SerializeField] private Clothes[] _clothes;

    public Goods[] GetGoodsList()
    {
        return _clothes;
    }
}
