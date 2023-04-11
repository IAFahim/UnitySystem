using System.Collections.Generic;

namespace _Game.World
{
    public interface IEffectedBySlowMo
    {
        public static List<IEffectedBySlowMo> all= new();
        public void OnSlowMo(float slowMoFactor);
        public void FixSlowMo(float slowMoFactor);
        public void OnNormalSpeed(float slowMoFactor);
    }
}