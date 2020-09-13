using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private Button _clothes;
    [SerializeField] private ClothesHolder _clothesHolder;
    [SerializeField] private Transform _viewPort;
    [SerializeField] private ClothesView _template;
    [SerializeField] private GameObject _menuButtonTemplate;
    [SerializeField] private SkinnedMeshRenderer _playerRenderer;

    private Dictionary<string, GoodsHolder> _goodsHolders = new Dictionary<string, GoodsHolder>();

    private void Start()
    {
        _goodsHolders.Add("Clothes", _clothesHolder);
    }

    private void OnEnable()
    {
        _close.onClick.AddListener(OnCloseButtonClick);
        _clothes.onClick.AddListener(OnClothesButtonClick);
    }

    private void OnDisable()
    {
        _close.onClick.RemoveListener(OnCloseButtonClick);
        _clothes.onClick.RemoveListener(OnClothesButtonClick);
    }

    private void OnClothesButtonClick()
    {
        ClearViewPort(_viewPort);

        ClothesHolder clothesHolder = (ClothesHolder) _goodsHolders["Clothes"];

        Clothes[] clothes = (Clothes[]) clothesHolder.GetGoodsList();
        GenerateClothesViews(clothes, _viewPort);
    }

    private void ClearViewPort(Transform vierwPort)
    {
        for (int i = 0, l = _viewPort.childCount; i < l; i++)
        {
            Destroy(_viewPort.GetChild(i).gameObject);
        }
    }

    private void GenerateClothesViews(Clothes[] materials, Transform container)
    {
        for (int i = 0, l = materials.Length; i < l; i++)
        {
            ClothesView clothesView = Instantiate(_template, container);
            clothesView.ButtonClicked += OnSelectClothesButtonClick;
            clothesView.Render(materials[i]);
        }
    }

    private void OnSelectClothesButtonClick(Clothes clothesObject, ClothesView view)
    {
        _playerRenderer.material = clothesObject.ClothesMaterial;
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
