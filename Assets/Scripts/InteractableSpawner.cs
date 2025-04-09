using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Linq.Expressions;
using System;
using Random = UnityEngine.Random;

public class InteractableSpawner : MonoBehaviour, IService
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _posSpawnY;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private bool _isLevelRunning;

    private float _curTime;

    private EventBus _eventBus;

    private Dictionary<string, ObjectPool<Interactable>> _pools = new Dictionary<string, ObjectPool<Interactable>>();

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscride<GameStartSignal>(OnGameStart);
        _eventBus.Subscride<GameStopSignal>(OnGameStop);

        _eventBus.Subscride<DisposeInteractableSignal>(Dispose);

    }

    private void OnGameStart(GameStartSignal signal)
    {
        _isLevelRunning = true;

        _curTime = 0;

        var interactables = _levelConfig.InteractableData;
        foreach (var interactable in interactables)
        {
            _ = WaitCooldownForSpawn(interactable);
        }
        _ = TrackLevelProgress();
    }

    private async UniTask WaitCooldownForSpawn(InteractableData interactableData)
    {
        float cooldown = interactableData.SpawnCooldownStart;
        while(_isLevelRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            SpawnInteractable(interactableData);

            cooldown = Mathf.Lerp(interactableData.SpawnCooldownStart, interactableData.SpawnCooldownEnd, (_curTime / _levelConfig.LevelLength));
        }
    }

    private async UniTask TrackLevelProgress()
    {
        while(_isLevelRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            _curTime += 0.1f;
            if (_curTime >= _levelConfig.LevelLength)
            {
                _isLevelRunning = false;
            }
        }
    }

    private void SpawnInteractable(InteractableData interactableData)
    {
        var interactable = interactableData.Prefab;
        var pool = GetPool(interactable);

        var item = pool.Get();
        item.transform.position = RandomPosition();

        _eventBus.Invoke(new InteractableActivatedSignal(item));
    }

    private ObjectPool<Interactable> GetPool(Interactable interactable)
    {
        string objectTypeStr = interactable.GetType().ToString();
        ObjectPool<Interactable> pool;

        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new ObjectPool<Interactable>(interactable, 5);
            _pools.Add(objectTypeStr, pool);
        }
        else
        {
            pool = _pools[objectTypeStr];
        }

        return pool;
    }

    private void Dispose(DisposeInteractableSignal signal)
    {
        var interactable = signal.Interactable;
        var pool = GetPool(interactable);
        pool.Realease(interactable);

        _eventBus.Invoke(new InteractableDisposedSignal(interactable));
    }

    private Vector3 RandomPosition()
    {
        var x = Random.Range(_minX, _maxX);
        return new Vector3(x, _posSpawnY, 0);
    }

    private void OnGameStop(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }







}
