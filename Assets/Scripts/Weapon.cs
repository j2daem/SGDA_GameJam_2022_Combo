using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform firePoint = null;
    [SerializeField] Animator animator = null;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootAnimiationDuration = .25f;
    [SerializeField] float bulletDuration = .5f;

    GameObject bullet;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            StartCoroutine(ShootAnimation());
        }
    }

    private void Shoot()
    {
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, bulletDuration);
    }

    IEnumerator ShootAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(shootAnimiationDuration);
        animator.SetBool("isAttacking", false);
    }
}
