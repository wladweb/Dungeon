using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Material _background;
    [SerializeField] private int _length;

    public string Name => _name;
    public Material Background => _background;
    public int Length => _length;
}
