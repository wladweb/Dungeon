using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClothesView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private Clothes _clothesObject;

    public event UnityAction<Clothes, ClothesView> ButtonClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(_clothesObject, this);
    }

    public void Render(Clothes clothesObject)
    {
        _clothesObject = clothesObject;
        _icon.sprite = clothesObject.Icon;
    }
}
