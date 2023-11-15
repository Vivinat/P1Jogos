using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] private int lifeTime;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Platform") || col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(BulletDuration());
    }

    IEnumerator BulletDuration()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
    }
}
