using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{
    [SerializeField] private List<TileData> _tiles;
    [SerializeField] private float _speed;

    [SerializeField] bool _onMove = true;

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
