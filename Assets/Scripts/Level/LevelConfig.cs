using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private List<InteractableData> _interactableData = new List<InteractableData>();
}
