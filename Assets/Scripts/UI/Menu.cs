using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _exit;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _shopPanel;

    private void OnEnable()
    {
        _play.onClick.AddListener(OnPlayButtonClick);
        _shop.onClick.AddListener(OnShopButtonClick);
        _exit.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _play.onClick.RemoveListener(OnPlayButtonClick);
        _shop.onClick.RemoveListener(OnShopButtonClick);
        _exit.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnShopButtonClick()
    {
        _shopPanel.SetActive(true);
    }

    private void OnPlayButtonClick()
    {
        gameObject.SetActive(false);
        _game.StartNewGame();
    }
}
