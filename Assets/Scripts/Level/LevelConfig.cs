using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private float _levelLength;
    [SerializeField] private float _speedStart;
    [SerializeField] private float _speedEnd;
    [SerializeField] private List<InteractableData> _interactableData = new List<InteractableData>();

    public float LevelLength => _levelLength;
    public float SpeedStart => _speedStart;
    public float SpeedEnd => _speedEnd;
    public List<InteractableData> InteractableData => _interactableData;
}
