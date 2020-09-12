using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public bool IsAlive { get; private set; } = true;

    public event UnityAction Died;

    public void Die()
    {
        _animator.Play("Dead");
        IsAlive = false;
    }

    public void GetLive()
    {
        IsAlive = true;
    }

    public void OnDeadAnimationStop()
    {
        Died?.Invoke();
    }
}
