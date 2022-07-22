using UnityEngine;
namespace Assets.Scripts.Model
{
    /**
     * Konkrete Ableitung der abstrakten Klasse Weapon und stellt das Maschinengewehr (M4A1) dar.
     */
    class MachineGun : Weapon
    {
        /**
         * Konstruktor der Klasse, die den super Konstruktor mit allen relevanten Daten aufruft.
         */
        public MachineGun(GameObject player) : base(player, "Machine Gun", 10, 0.3f, StaticMenueFunctions.FindComponentInChildWithTag<Component>(player, "Machine Gun").gameObject, 30)
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
