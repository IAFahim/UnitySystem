using System.Collections.Generic;
using _Game.Util;

namespace _Game.Character.Health.Interface
{
    public interface IPercentageAction<T>
    {
        public List<PercentagePromise<T>> PercentageList { get; set; }
        public void AddNotification(PercentagePromise<T> percentagePromise);
        public void RemoveNotification(PercentagePromise<T> percentagePromise);
    }
}