using UnityEngine;

namespace Game.Character.Health.Interface
{
    public interface IHealthEvent
    {
        void OnHealthChange();
        void OnOverHeal();
        void OnImmuneChange();
        void OnMaxHealthChange();
        void OnDamageTaken( GameObject attacker);
        void OnHeal(GameObject healer);
        void OnDead();
    }
}