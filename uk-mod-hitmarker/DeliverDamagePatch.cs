using UnityEngine;

namespace HitmarkerMod
{
    public static class DeliverDamagePatch
    {
        public static void ApplyHooks()
        {
            On.EnemyIdentifier.DeliverDamage += EnemyIdentifier_DeliverDamage;
            On.StyleHUD.AddPoints += StyleHUD_AddPoints;
        }

        private static void StyleHUD_AddPoints(On.StyleHUD.orig_AddPoints orig, StyleHUD self, int points, string pointID, GameObject sourceWeapon, EnemyIdentifier eid, int count, string prefix, string postfix)
        {
            //Special events that can't be reliably checked in EnemyIdentifier_DeliverDamage.
            switch (pointID)
            {
                case "ultrakill.chargeback":
                    Hitmarker.Instance.OnEnemyDamage(10, 1);
                    break;
            }
            orig(self, points, pointID, sourceWeapon, eid, count, prefix, postfix);
        }

        private static void EnemyIdentifier_DeliverDamage(On.EnemyIdentifier.orig_DeliverDamage orig, EnemyIdentifier self, GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon)
        {
            if (self.dead || self.blessed)
            {
                orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
                return;
            }
            Debug.Log(self.hitter);

            if (sourceWeapon == null)
            {
                //Source cannot be determined. Only check for damage dealt by
                //the player that isn't used by a gun.
                switch (self.hitter)
                {
                    case "drill":           //Screwdriver
                    case "punch":
                    case "heavypunch":
                    case "ground slam":
                    case "projectile":      //Enemy projectile. Parried.
                    case "ffexplosion":     //Kamakaze drone
                        Hitmarker.Instance.OnEnemyDamage(multiplier, critMultiplier);
                    break;
                }
                orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
                return;
            }

            Debug.Log("source weapon isn't null");
            if (!(sourceWeapon.transform.parent != null && sourceWeapon.transform.parent.tag == "GunControl"))
            {
                //Damage wasn't dealt by the player.
                orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
                return;
            }

            //TODO: Add hit types later.
            switch (self.hitter)
            {
                case "shotgun":             //Shotgun pellet
                case "shotgunzone":         //Shotgun hitscan
                case "revolver":
                case "railcannon":
                case "sawblade":
                case "nail":
                case "coin":                //Fistful of dollar
                    Hitmarker.Instance.OnEnemyDamage(multiplier, critMultiplier);
                    break;
                case "explosion":
                    if (multiplier != 0f)
                    {
                        Hitmarker.Instance.OnEnemyDamage(multiplier, critMultiplier);
                    }
                    break;
            }
            orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
        }

        public static void RemoveHooks()
        {
            On.EnemyIdentifier.DeliverDamage -= EnemyIdentifier_DeliverDamage;
            On.StyleHUD.AddPoints -= StyleHUD_AddPoints;
        }
    }
}
