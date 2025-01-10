using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : LivingEntity
{
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }
}
