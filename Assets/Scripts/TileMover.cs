using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class TileMover : MonoBehaviour, IService
{
    [SerializeField] private List<TileData> _tiles;
    [SerializeField] private float _speed;

    [SerializeField] private bool _onMove;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscride<GameStartSignal>(OnGameStart);
        _eventBus.Subscride<GameStopSignal>(OnGameStop);
    }

    private void OnGameStart(GameStartSignal signal)
    {
        _onMove = true;
    }
    private void OnGameStop(GameStopSignal signal)
    {
        _onMove = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_onMove)
        {
            return;
        }

        foreach (var tile in _tiles)
        {
            tile.transform.Translate(Vector3.down * Time.deltaTime * _speed);
            if (tile.transform.position.y <= -1)
            {
                tile.transform.Translate(Vector3.up * tile.TileLenght * _tiles.Count);
            }
        }
    }
}
