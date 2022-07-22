using UnityEngine;

namespace Assets.Scripts.Model
{
    /**
     * Konkrete Ableitung der abstrakten Klasse Weapon und stellt die Pistole dar.
     */
    class Pistol : Weapon
    {
        /**
         * Konstruktor der Klasse, die den super Konstruktor mit allen relevanten Daten aufruft.
         */
        public Pistol(GameObject player) : base(player, "Pistol", 10, 0.65f, StaticMenueFunctions.FindComponentInChildWithTag<Component>(player, "Pistol").gameObject, 12)
        {
            mInPossess = true;
        }

        public override void Activate()
        {
            mWeaponModel.transform.parent = mPlayer.transform;
            mWeaponModel.SetActive(true);

        }

        public override void Deactivate()
        {
            mWeaponModel.SetActive(false);
        }
    }
}
