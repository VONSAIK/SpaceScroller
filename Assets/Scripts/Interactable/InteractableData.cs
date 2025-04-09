using System;
using UnityEngine;

[Serializable]
public class InteractableData 
{
    [SerializeField] private Interactable _prefab;
    [SerializeField] private InteractableEnum _interactableType;

    [SerializeField] private float _spawnCooldownStart;
    [SerializeField] private float _spawnCooldownEnd;

    public Interactable Prefab => _prefab;
    public InteractableEnum InteractableType => _interactableType;
    public float SpawnCooldownStart => _spawnCooldownStart;
    public float SpawnCooldownEnd => _spawnCooldownEnd;

}
