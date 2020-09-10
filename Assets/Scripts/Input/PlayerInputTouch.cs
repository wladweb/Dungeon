using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputTouch : MonoBehaviour, PlayerInput
{
    private int _halfScreen;

    private void Start()
    {
        _halfScreen = Screen.width / 2;
    }

    public bool PlayerAction(out Direction direction)
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began
            && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            direction = GetDirection(touch);
            return true;
        }
        else
        {
            direction = Direction.Right;
            return false;
        }
    }

    private Direction GetDirection(Touch touch)
    {
        if (touch.position.x <= _halfScreen)
            return Direction.Left;
        else
            return Direction.Right;
    }
}
