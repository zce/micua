namespace Micua.Core.ViewEngine
{
    using System;
    using System.Reflection;

    using NVelocity;
    using NVelocity.Runtime;
    using NVelocity.Util.Introspection;

    public class ExtensionDuck : IDuck
	{
		private readonly object _instance;
		private readonly Type _instanceType;
		private readonly Type[] _extensionTypes;
		private Introspector _introspector;

		public ExtensionDuck(object instance)
			: this(instance, Type.EmptyTypes)
		{
		}

		public ExtensionDuck(object instance, params Type[] extentionTypes)
		{
			if(instance == null) throw new ArgumentNullException("instance");

			this._instance = instance;
			this._instanceType = this._instance.GetType();
			this._extensionTypes = extentionTypes;
		}

		public Introspector Introspector
		{
			get
			{
				if(this._introspector == null)
				{
					this._introspector = RuntimeSingleton.Introspector;
				}
				return this._introspector;
			}
			set { this._introspector = value; }
		}

		public object GetInvoke(string propName)
		{
			throw new NotSupportedException();
		}

		public void SetInvoke(string propName, object value)
		{
			throw new NotSupportedException();
		}

		public object Invoke(string method, params object[] args)
		{
			if(string.IsNullOrEmpty(method)) return null;

			MethodInfo methodInfo = this.Introspector.GetMethod(this._instanceType, method, args);
			if(methodInfo != null)
			{
				return methodInfo.Invoke(this._instance, args);
			}

			object[] extensionArgs = new object[args.Length + 1];
			extensionArgs[0] = this._instance;
			Array.Copy(args, 0, extensionArgs, 1, args.Length);

			foreach(Type extensionType in this._extensionTypes)
			{
				methodInfo = this.Introspector.GetMethod(extensionType, method, extensionArgs);
				if(methodInfo != null)
				{
					return methodInfo.Invoke(null, extensionArgs);
				}
			}

			return null;
		}
	}
}