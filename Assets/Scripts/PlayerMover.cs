using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    public bool Available { get; private set; } = true;

    public event UnityAction ReachedNextCell;

    public void MoveLeft()
    {
        if (transform.position.z == 0)
            return;

        Available = false;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z - Game.CELL_SIZE);
        transform.LookAt(target);
        StartCoroutine(MovementLeft(target));
    }

    public void MoveRight()
    {
        Available = false;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z + Game.CELL_SIZE);
        transform.LookAt(target);
        StartCoroutine(MovementRight(target));
    }

    private IEnumerator MovementRight(Vector3 target)
    {
        while (transform.position.z < target.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            _animator.SetFloat("Speed", target.z - transform.position.z);
            yield return null;
        }
        transform.position = target;
        ReachedNextCell?.Invoke();
        Available = true;
    }

    private IEnumerator MovementLeft(Vector3 target)
    {
        while (transform.position.z > target.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            _animator.SetFloat("Speed", transform.position.z - target.z);
            yield return null;
        }
        transform.position = target;
        ReachedNextCell?.Invoke();
        Available = true;
    }
}
