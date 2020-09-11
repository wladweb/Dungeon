using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;

    public GridLayer Layer => _layer;
    public int Chance => Mathf.Clamp(_chance, 0, 100);
}
