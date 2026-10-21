using System;
using System.Threading;
using UnityEngine;

public class PlayerBlaster : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform bulletSpawnLocation;
    [Tooltip("How many times the blaster will fire each second.")]
    [SerializeField, Range(0.1f, 5f)] public float fireRate = 1f;
    [SerializeField, Range(10f, 100f)] public float bulletSpeed = 20f;

    private float shootCooldown = 0f;
    private bool blasterActive = false;

    private void Awake()
    {
        DeactivateBlaster();
    }

    // Update is called once per frame
    void Update()
    {
        if (blasterActive)
        {
            if (shootCooldown > 0f)
            {
                shootCooldown -= Time.deltaTime * fireRate;
            }
            else
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnLocation.forward * bulletSpeed, ForceMode.Impulse);

        shootCooldown = 1;
    }

    public void ActivateBlaster()
    {
        blasterActive = true;
        shootCooldown = 1;
    }

    public void DeactivateBlaster()
    {
        blasterActive = false;
    }
}
