// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   实体模型基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
namespace Micua.Domain.Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// 实体模型基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class EntityBase<TKey> : EntityBase
    {
        public abstract TKey Id { get; set; }
    }
    /// <summary>
    /// 实体模型基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class EntityBase
    {

        public static System.Collections.Generic.IEnumerable<Type> EntityTypes
        {
            get
            {
                var modelAssembly = System.Reflection.Assembly.GetAssembly(typeof(MicuaContext));
                var modelTypes = modelAssembly.GetTypes().Where(t => typeof(EntityBase).IsAssignableFrom(t) && !t.IsAbstract);
                return modelTypes;
            }
        }
    }
}
