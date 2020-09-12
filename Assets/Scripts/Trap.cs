using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private Animator _animator;

    private float _timeSpread = 1f;
    private float _lastTrigger;
    private bool _isActive;


    private void Update()
    {
        _lastTrigger += Time.deltaTime;

        if (_lastTrigger >= (_timer + Random.Range(0.1f, _timeSpread)))
        {
            MoveUp();
            _isActive = true;
            _lastTrigger = 0;
        }
    }

    private void MoveUp()
    {
        _animator.Play("Active");
    }

    public void Deactivate()
    {
        _animator.Play("Inactive");
        _isActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && _isActive)
        {
            player.Die();
        }
    }
}
