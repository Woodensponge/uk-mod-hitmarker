using UnityEngine;

namespace HitmarkerMod
{
    //TODO: Probably rename to damage patch
    public static class GunPatch
    {
        public static void ApplyHooks()
        {
            On.EnemyIdentifier.DeliverDamage += EnemyIdentifier_DeliverDamage;
        }

        //TODO: This is broken.
        private static void EnemyIdentifier_DeliverDamage(On.EnemyIdentifier.orig_DeliverDamage orig, EnemyIdentifier self, GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon)
        {
            bool isPlayer = false;

            if (self.dead 
                || !(sourceWeapon.transform != null && sourceWeapon.transform.parent.tag == "GunControl"))
            {
                orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
                return;
            }
            

            Debug.Log(self.hitter);
            //TODO: Add hit types later.
            switch (self.hitter)
            {
                case "melee":
                case "heavy melee":
                case "ground slam":
                case "shotgun":
                case "revolver":
                case "railcannon":
                case "sawblade":
                case "nail":
                case "drill":
                case "explosion":
                    Hitmarker.Instance.GenericAnimation();
                    break;
            }
            orig(self, target, force, hitPoint, multiplier, tryForExplode, critMultiplier, sourceWeapon);
        }

        public static void RemoveHooks()
        {
            On.EnemyIdentifier.DeliverDamage -= EnemyIdentifier_DeliverDamage;
        }
    }
}
