using System.Collections.Generic;
using _Game.Character.Health.Interface;
using _Game.Util;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Character.Health
{
    public class Health : MonoBehaviour, IHealthBase, IHealthEvent
    {
        public static List<Health> allActive= new();
        [field:SerializeReference] public bool isImmune { get; private set; }
        [SerializeField] [Range(0, 1)] internal float percentage;
        [SerializeField] internal float value;
        [SerializeField] internal float maxHealth; 
        public List<PercentagePromise<Health>> PercentagePromises { get; } = new();
        [SerializeField] internal UnityEvent<float, float> eventOnHealthChanged;
        [SerializeField] internal UnityEvent<float, float> eventOnOverHeal;
        [SerializeField] internal UnityEvent<float, float> eventOnMaxHealthChanged;
        [SerializeField] internal UnityEvent<bool, float, float> eventOnImmuneChanged;
        [SerializeField] internal UnityEvent<float, float, GameObject> eventOnDamageTaken;
        [SerializeField] internal UnityEvent<float, float, GameObject> eventOnHeal;
        [SerializeField] internal UnityEvent<float, float> eventOnDead;
        private bool _isImmune;
        private bool _isImmune1;

        private void OnEnable()
        {
            allActive.Add(this);
        }

        private void OnDisable()
        {
            allActive.Remove(this);
        }

        private void OnValidate()
        {
            SetPercentage(percentage);
        }

        [Button]
        public float SetHealth(float value)
        {
            if (isImmune) return this.value;
            if (value <= 0) OnDead();
            if (value > maxHealth) OnOverHeal();
            this.value = Mathf.Clamp(value, 0, maxHealth);
            percentage = this.value / maxHealth;

            foreach (var promise in PercentagePromises)
            {
                promise.Invoke(this);
            }

            OnHealthChange();
            return this.value;
        }


        public float Heal(float point, GameObject healer)
        {
            if (point >= 0)
            {
                eventOnHeal?.Invoke(SetHealth(value + point), value, healer);
            }

            return value;
        }


        public float TakeDamage(float amount, GameObject attacker)
        {
            if (amount >= 0)
            {
                eventOnDamageTaken?.Invoke(SetHealth(value - amount), value, attacker);
            }

            return value;
        }

        public float GetPercentage()
        {
            throw new System.NotImplementedException();
        }


        public float SetMaxHealth(float value)
        {
            OnMaxHealthChange();
            return maxHealth = value;
        }


        bool IHealthBase.isImmune
        {
            get => _isImmune1;
            set => _isImmune1 = value;
        }

        [Button]
        public float SetImmune(bool state)
        {
            isImmune = state;
            OnImmuneChange();
            return value;
        }

        [Button]
        public float SetHealthToMax() => SetHealth(maxHealth);

        public float GetHealth() => value;

        public float GetMaxHealth() => maxHealth;

        public float SetPercentage(float normalizedPercentage)
        {
            float currentHealth = SetHealth(normalizedPercentage * maxHealth);
            return currentHealth / maxHealth;
        }

        public bool IsImmune() => isImmune;

        public float TakeDamage(float amount) => TakeDamage(amount, null);
        public void OnHealthChange() => eventOnHealthChanged?.Invoke(value, maxHealth);
        public void OnOverHeal() => eventOnOverHeal?.Invoke(value, maxHealth);
        public void OnImmuneChange() => eventOnImmuneChanged?.Invoke(isImmune, value, maxHealth);
        public void OnMaxHealthChange() => eventOnMaxHealthChanged?.Invoke(value, maxHealth);

        public void OnDamageTaken(GameObject attacker) =>
            eventOnDamageTaken?.Invoke(value, maxHealth, attacker);

        public void OnHeal(GameObject healer) =>
            eventOnHeal?.Invoke(value, maxHealth, healer);

        public void OnDead() => eventOnDead?.Invoke(value, maxHealth);

        public override string ToString() => $"Health: {value}/{maxHealth}";
        
    }
}