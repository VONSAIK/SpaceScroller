using NUnit.Framework;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class InteractableMover : MonoBehaviour, IService
{
    [SerializeField] private float _speedKoef;
    [SerializeField] private const float _lowBorderY = 4;
    [SerializeField] private LevelConfig _levelData;

    private List<Interactable> _interactables = new List<Interactable>();

    private float _timePassed;

    private EventBus _eventBus;

    private bool _isLevelRunning;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscride<InteractableActivatedSignal>(TryAdd);
        _eventBus.Subscride<InteractableDisposedSignal>(TryRemove);

        _eventBus.Subscride<GameStartSignal>(GameStart);
        _eventBus.Subscride<GameStopSignal>(GameStop);
    }

    private void TryAdd(InteractableActivatedSignal signal)
    {
        if (!_interactables.Contains(signal.Interactable) && _isLevelRunning)
        {
            _interactables.Add(signal.Interactable);
        }
    }

    private void TryRemove(InteractableDisposedSignal signal)
    {
        if (_interactables.Contains(signal.Interactable) && _isLevelRunning)
        {
            _interactables.Remove(signal.Interactable);
        }
    }

    private void GameStart(GameStartSignal signal)
    {
        _isLevelRunning = true;
        _timePassed = 0f;
    }

    private void GameStop(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }

    private void Update()
    {
        if (!_isLevelRunning)
        {
            return;
        }

        foreach(var interactable in _interactables)
        {
            interactable.transform.Translate(Vector3.down * Time.deltaTime * _speedKoef);
        }

        _timePassed += Time.deltaTime;
        _speedKoef = Mathf.Lerp(_levelData.SpeedStart, _levelData.SpeedEnd, (_timePassed / _levelData.LevelLength));
    }

    private void LateUpdate()
    {
        if (_interactables.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _interactables.Count; i++)
        {
            if ( _interactables[i].transform.position.y <= _lowBorderY || !_isLevelRunning )
            {
                _eventBus.Invoke(new DisposeInteractableSignal(_interactables[i]));
            }
        }
    }

    private void OnDestroy()
    {
        _isLevelRunning = false;

        _eventBus.Unsubscribe<InteractableActivatedSignal>();
        _eventBus.Unsubscribe<InteractableDisposedSignal>();

        _eventBus.Unsubscribe<GameStartSignal>();
        _eventBus.Unsubscribe<GameStopSignal>();
    }
}
