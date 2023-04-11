namespace _Game.Util
{
    public class PercentagePromise<T>
    {
        public delegate void HealthDelegate(T data);
        public delegate bool PromiseDelegate();
        private bool isResolved { get; set; }
        private bool isRejected { get; set; }
        private readonly PromiseDelegate promise;
        private readonly HealthDelegate resolve;
        private readonly HealthDelegate reject;

        public PercentagePromise(PromiseDelegate promise, HealthDelegate resolve, HealthDelegate reject = null)
        {
            this.promise = promise;
            this.resolve = resolve;
            this.reject = reject;
        }

        public void Invoke(T data)
        {
            bool result = promise.Invoke();
            if (result)
            {
                if (!isResolved)
                {
                    resolve.Invoke(data);
                    isResolved = true;
                    isRejected = false;
                }
            }
            else
            {
                if (!isRejected)
                {
                    reject?.Invoke(data);
                    isRejected = true;
                    isResolved = false;
                }
            }
        }
    }
}