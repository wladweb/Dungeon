using UnityEngine;

public class StudioMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _sensetive;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _player.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _sensetive);
    }
}
