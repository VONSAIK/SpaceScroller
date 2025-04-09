using UnityEngine;
using CustomEventBus;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private TileMover _tileMover;
    [SerializeField] private Player _player;
    [SerializeField] private InteractableMover _interactableMover;
    [SerializeField] private InteractableSpawner _interactableSpawner;

    private EventBus _eventBus;
    private GameController _gameController;

    private void Awake()
    {
        _eventBus = new EventBus();
        _gameController = new GameController();

        RegisterServices();
        Initialize();
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_gameController);
        ServiceLocator.Current.Register<TileMover>(_tileMover);
        ServiceLocator.Current.Register<Player>(_player);
        ServiceLocator.Current.Register<InteractableMover>(_interactableMover);
        ServiceLocator.Current.Register<InteractableSpawner>(_interactableSpawner);
    }

    private void Initialize()
    {
        _tileMover.Init();
        _interactableMover.Init();
        _interactableSpawner.Init();
        _gameController.Init();
    }
}

