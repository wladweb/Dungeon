using UnityEngine;

public class Game : MonoBehaviour
{
    public const string APP_NAME = "Dungeon";
    public const string APP_VERSION = "0.1.0";

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.PlayerAction(out Direction direction))
        { 
            Debug.Log(direction);
        }
    }
}
