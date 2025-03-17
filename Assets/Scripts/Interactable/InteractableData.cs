using System;
using UnityEngine;

[Serializable]
public class InteractableData 
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private InteractableEnum _interactableType;

    [SerializeField] private float _speedStart;
    [SerializeField] private float _speedEnd;

    [SerializeField] private float _spawnCooldownStart;
    [SerializeField] private float _spawnCooldownEnd;
}
