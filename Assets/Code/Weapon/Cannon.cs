﻿using System;
using UnityEngine;

namespace BotArena
{
    public class Cannon : IWeaponController
    {

        public override void Attack(float power)
        {
            GameObject res;

            res = Instantiate(projectileObject, bulletSpawner.position, bulletSpawner.rotation);
            res.GetComponent<Rigidbody>().velocity = transform.forward * power * 5;
            res.GetComponent<IProjectileController>().SetWeapon(this);
        }

        public override float GetStaminaCost()
        {
            return 1;
        }
    }
}