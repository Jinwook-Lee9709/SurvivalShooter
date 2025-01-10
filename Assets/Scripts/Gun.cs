using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    
    private LineRenderer lineRenderer;
    private float coolTime;
    private float nextFire;
    private float damage = 10f;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        coolTime = 1f / fireRate;
    }
    public bool Fire()
    {
        if (Time.time <= nextFire)
            return false;
        nextFire = Time.time + coolTime;
        
        float randomSpray = Random.Range(-1f, 1f);
        Quaternion randomRotation = Quaternion.Euler(randomSpray, randomSpray, randomSpray);
        
        Vector3 hitPoint;
        Ray ray = new Ray(firePoint.position, randomRotation * firePoint.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100f))
        {
            hitPoint = hit.point;
            if (hit.collider.CompareTag("Monster"))
            {
                LivingEntity entity = hit.collider.gameObject.GetComponent<LivingEntity>();
                entity.OnDamage(damage, hitPoint, hit.normal);
                hitEffect.transform.position = hitPoint;
                hitEffect.transform.rotation = Quaternion.Euler(hit.normal);
                hitEffect.Play();
            }
        }
        else
        {
            hitPoint = ray.origin + ray.direction * 100f;
        }

        muzzleFlash.Play();
        StartCoroutine(FireEffectRoutine(hitPoint));
        return true;
    }

    IEnumerator FireEffectRoutine(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
