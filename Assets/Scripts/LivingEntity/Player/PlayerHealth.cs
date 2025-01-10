using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Canvas hitEffect;
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = Hp / maxHp;
        StartCoroutine(HitEffectRoutine());
    }

    IEnumerator HitEffectRoutine()
    {
        hitEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        hitEffect.gameObject.SetActive(false);
    }
}
