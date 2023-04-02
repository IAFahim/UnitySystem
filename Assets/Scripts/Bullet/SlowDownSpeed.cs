using System;
using UnityEngine.UI;

namespace Bullet
{
    public abstract class SlowDownSpeed
    {
        private static readonly float SlowDownRate = 0.02f;
        private static float value = 1f;
        public static Action onSlowMo;
        public static Action onNormalSpeed;

        public static bool isSlowMo = false;
        public static float GetSpeed()
        {
            return value;
        }
        
        public static bool GetIsSlowMo()
        {
            return isSlowMo;
        }

        public static void Toggle()
        {
            isSlowMo = !isSlowMo;
            if (isSlowMo)
            {
                value = SlowDownRate;
                onSlowMo?.Invoke();
            }
            else
            {
                value = 1f;
                onNormalSpeed?.Invoke();
            }
        }
    }
}