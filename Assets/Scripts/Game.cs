using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;

    public const string APP_NAME = "Dungeon";
    public const string APP_VERSION = "0.1.0";
    public const float CELL_SIZE = 2;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.PlayerAction(out Direction direction) && _mover.Available)
        {
            switch (direction)
            {
                case Direction.Left:
                    _mover.MoveLeft();
                    break;
                case Direction.Right:
                    _mover.MoveRight();
                    break;
            }
        }
    }
}
