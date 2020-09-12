using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private int _range;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _verticalOffset;
    [SerializeField] private Transform _container;
    [SerializeField] private GridObject _chest;

    private HashSet<Vector3Int> _engagedCells = new HashSet<Vector3Int>();
    private Vector3Int _areaCenter;
    private Vector3 _startLevelPosition = new Vector3(0, 1, 0);
    private Vector3 _endLevelPosition;
    private Game _game;

    public event UnityAction ReachedEndLevel;

    private void Start()
    {
        _game = GetComponent<Game>();
        StartNewLevel();
    }

    private void OnEnable()
    {
        _player.ReachedNextCell += OnReachNextCell;
    }

    private void OnDisable()
    {
        _player.ReachedNextCell -= OnReachNextCell;
    }

    public void StartNewLevel()
    {
        ResetGenerator();
        _player.transform.position = _startLevelPosition;

        Vector3Int _endLevelGridosition = new Vector3Int(0, 0, _game.GetLevelLength());
        _endLevelPosition = GridToWorldPosition(_endLevelGridosition);

        PlaceSingleObject(_chest, _endLevelGridosition);
        FillRange(_player.transform.position, _range);
    }

    private void OnReachNextCell()
    {
        FillRange(_player.transform.position, _range);
        ControlGridObjectsVisibility();
        CheckEndLevel();
    }

    private void PlaceSingleObject(GridObject gridObject, Vector3Int position)
    {
        position.y = (int) gridObject.Layer;
        Instantiate(gridObject, GridToWorldPosition(position), Quaternion.identity, _container.transform);
        _engagedCells.Add(position);
    }

    private void CheckEndLevel()
    {
        if (_player.transform.position.z == _endLevelPosition.z)
        {
            ReachedEndLevel?.Invoke();
        }
    }

    private void FillRange(Vector3 center, float _range)
    {
        int cellsCountInRange = (int)(_range / Game.CELL_SIZE);
        _areaCenter = WorldToGridPosition(center);

        for (int i = -cellsCountInRange; i < cellsCountInRange; i++)
        {
            TryCreateGridObject(GridLayer.Ground, _areaCenter + new Vector3Int(0, 0, i));
            TryCreateGridObject(GridLayer.OnGround, _areaCenter + new Vector3Int(0, 0, i));
            TryCreateGridObject(GridLayer.InAir, _areaCenter + new Vector3Int(0, 0, i));
        }
    }

    private void ControlGridObjectsVisibility()
    {
        for (int i = 0, l = _container.childCount; i < l; i++)
        {
            Transform child = _container.GetChild(i);

            if (IsObjectAbroadRange(child))
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private bool IsObjectAbroadRange(Transform gridObject)
    {
        if (gridObject.position.z < GridToWorldPosition(_areaCenter - new Vector3Int(0, 0, _range / 2)).z
            || gridObject.position.z > GridToWorldPosition(_areaCenter + new Vector3Int(0, 0, _range / 2)).z)
        {
            return true;
        }

        return false;
    }

    private void TryCreateGridObject(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;
        
        if (_engagedCells.Contains(gridPosition))
            return;
        else
            _engagedCells.Add(gridPosition);
        
        if (!TryGetRandomTemplate(layer, out GridObject template))
            return;
        
        Vector3 position = GridToWorldPosition(gridPosition);
        Instantiate(template, position, Quaternion.identity, _container.transform);
    }

    private bool TryGetRandomTemplate(GridLayer layer, out GridObject template)
    {
        var variants = _templates.Where(item => item.Layer == layer);

        foreach (GridObject variant in variants)
        {
            if (variant.Chance > Random.Range(0, 100))
            {
                template = variant;
                return true;
            }
        }

        template = null;
        return false;
    }

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * Game.CELL_SIZE,
            gridPosition.y * Game.CELL_SIZE,
            gridPosition.z * Game.CELL_SIZE);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / Game.CELL_SIZE),
            (int)(worldPosition.y / Game.CELL_SIZE),
            (int)(worldPosition.z / Game.CELL_SIZE));
    }

    private void ResetGenerator()
    {
        _engagedCells.Clear();

        for (int i = 0, l = _container.childCount; i < l; i++)
        {
            Transform child = _container.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
