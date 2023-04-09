using System.Collections.Generic;

namespace Game.World
{
    public interface IEffectedBySlowMo
    {
        public static List<IEffectedBySlowMo> all= new();
        public void OnSlowMo();
        public void OnNormalSpeed();
    }
}