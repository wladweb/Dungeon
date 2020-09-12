using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    private int _currentLevelIndex;

    public bool HasLevel()
    {
        return (_currentLevelIndex) < _levels.Length;
    }

    public Level GetCurrentLevel()
    {
        return _levels[_currentLevelIndex];
    }

    public bool TryGetLevel(out Level level)
    {
        if (HasLevel())
        {
            level = _levels[_currentLevelIndex];
            _currentLevelIndex++;
            return true;
        }
        else
        {
            level = null;
            return false;
        }
    }

    public void ResetLevels()
    {
        _currentLevelIndex = 0;
    }
}
