using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Micua.Core.Tracing.Interception
{
    /// <summary>
    /// 跟踪调用过程
    /// </summary>
    public class TracingCallAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TracingCallHandler { Order = Order };
        }
    }
}