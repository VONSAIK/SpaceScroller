using UnityEngine;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private TileMover _tileMover;
    [SerializeField] private Player _player;

    private void Awake()
    {
        
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register<TileMover>(_tileMover);
        ServiceLocator.Current.Register<Player>(_player);
    }
}
