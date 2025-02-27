using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] protected float _speedInterable;
    [SerializeField] protected bool _onMove;

    protected abstract void Interact();

    protected virtual void Move()
    {
        if (!_onMove)
        {
            return;
        }
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * _speedInterable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(PLAYER_TAG))
        {
            Interact();
            Destroy(gameObject);
        }
    }


}
