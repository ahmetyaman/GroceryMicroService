using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceCounterAspect : MethodInterception
    {
        private Stopwatch _stopwatch;

        public PerformanceCounterAspect(int interval = 5)
        {
            _stopwatch = (Stopwatch)Utilities.Helpers.HttpContext.Current.RequestServices.GetService(typeof(Stopwatch));
        }

        public override void Intercept(IInvocation invocation)
        {
            _stopwatch.Start();

            invocation.Proceed();
            _stopwatch.Stop();

            if (_stopwatch.Elapsed.TotalSeconds > 5)
            {
                // Log the performance issue
                Debug.WriteLine($"Performance issue: {invocation.Method.Name} took {_stopwatch.Elapsed.TotalSeconds} seconds.");
            }

            _stopwatch.Reset();
        }
    }
}