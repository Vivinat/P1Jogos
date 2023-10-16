using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moeda : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D col)
    {
        AudioSource.PlayClipAtPoint( GetComponent<AudioSource>().clip , transform.position);
        Destroy(gameObject);
    }
}
