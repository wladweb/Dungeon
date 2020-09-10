using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputMouse : MonoBehaviour, PlayerInput
{
    private int _halfScreen;

    private void Start()
    {
        _halfScreen = Screen.width / 2;
    }

    public bool PlayerAction(out Direction direction)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            direction = GetDirection();
            return true;
        }

        direction = Direction.Right;
        return false;
    }

    private Direction GetDirection()
    {
        if (Input.mousePosition.x <= _halfScreen)
            return Direction.Left;
        else
            return Direction.Right;
    }
}
