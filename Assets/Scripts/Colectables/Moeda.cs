using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CollectableType
{
    coin,
    block
}

public class Moeda : MonoBehaviour
{
    public Player player;
    [SerializeField] private CollectableType type;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (type == CollectableType.coin && col.CompareTag("Player") 
                                         && col is CapsuleCollider2D)
        {
            AudioSource.PlayClipAtPoint( GetComponent<AudioSource>().clip , transform.position);
            player.AddColectable("coin");
            Destroy(gameObject);
        }

        if (type == CollectableType.block && col.CompareTag("Player")
                                          && col is CapsuleCollider2D)
        {
            AudioSource.PlayClipAtPoint( GetComponent<AudioSource>().clip , transform.position);
            player.AddColectable("block");
            Destroy(gameObject);
        }
    }
}
