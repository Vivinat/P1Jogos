using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int fireDelay;
    [SerializeField] private bool canFire = true;
    [SerializeField] private float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            Fire();    
        }
    }

    private void Fire()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Debug.Log("Atira");
        GameObject shoot = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = shoot.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
        canFire = false;
        yield return new WaitForSecondsRealtime(fireDelay);
        canFire = true;
    }
}
