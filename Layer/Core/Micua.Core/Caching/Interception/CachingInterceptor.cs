using System;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Micua.Core.Caching.Interception
{
    //public class CachingCallHandler : ICallHandler
    //{
    //    public int Order { get; set; }
    //    public int Duration { get; set; }

    //    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    //    {
    //        var key = input.Target.GetType().Name;
    //        var result = CacheHelper.Get<IMethodReturn>(key);
    //        if (result==null)
    //        {
    //            //Console.WriteLine("The CallHandler applied to \"{0}\" is invoked.", input.Target.GetType().Name);
    //            result = getNext()(input, getNext);
    //            CacheHelper.Set(key, result,TimeSpan.FromSeconds(Duration));
    //        }

    //        return getNext()(input, getNext);
    //    }
    //}
    //public class CachingResultAttribute : HandlerAttribute
    //{
    //    public int Duration { get; set; }
    //    public CachingResultAttribute(int duration = 600)
    //    {
    //        Duration = duration;
    //    }
    //    public override ICallHandler CreateHandler(IUnityContainer container)
    //    {
    //        return new CachingCallHandler { Order = this.Order, Duration = Duration };
    //    }
    //}
}