using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Generator _generator;
    [SerializeField] private Levels _levels;
    [SerializeField] private MeshRenderer _background;
    [SerializeField] private TMP_Text _levelName;

    public const string APP_NAME = "Dungeon";
    public const string APP_VERSION = "0.1.0";
    public const float CELL_SIZE = 2;

    private PlayerInput _playerInput;
    private Level _currentLevel;
    private Player _player;

    public event UnityAction GameFinished;
    public event UnityAction LevelFinished;
    public event UnityAction PlayerDied;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        ChangeLevel();
    }

    private void OnEnable()
    {
        _generator.ReachedEndLevel += OnReachEndLevel;
        _player = _mover.GetComponent<Player>();
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _generator.ReachedEndLevel -= OnReachEndLevel;
        _player.Died -= OnPlayerDied;
    }

    private void Update()
    {
        if (_playerInput.PlayerAction(out Direction direction)
            && _mover.Available 
            && _player.IsAlive)
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

    private void OnReachEndLevel()
    {
        LevelFinished?.Invoke();
    }

    public void StartNewGame()
    {
        _levels.ResetLevels();
        _player.GetLive();
        StartNextLevel();
    }

    public void StartNextLevel()
    {
        ChangeLevel();
        _generator.StartNewLevel();
    }

    private void ChangeLevel()
    {
        if (_levels.TryGetLevel(out Level level))
        {
            _currentLevel = level;
            _background.material = _currentLevel.Background;
            _levelName.text = _currentLevel.Name;
        }
        else
        {
            Time.timeScale = 0;
            GameFinished?.Invoke();
        }
    }

    private void OnPlayerDied()
    {
        Debug.Log("dead");
        PlayerDied?.Invoke();    
    }

    public int GetLevelLength()
    {
        return _currentLevel.Length;
    }

    public bool HasNextLevel()
    {
       return _levels.HasLevel();
    }
}
