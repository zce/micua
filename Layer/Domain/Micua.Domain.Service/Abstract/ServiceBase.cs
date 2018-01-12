// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 服务抽象基类 <ServiceBase>
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
namespace Micua.Domain.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Micua.Domain.Model;
    using Micua.Domain.Repository;

    /// <summary>
    /// 服务抽象基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ServiceBase<TKey, TEntity> : IService<TKey, TEntity> where TEntity : EntityBase<TKey>
    {
        protected abstract IRepository<TKey, TEntity> Repository { get; }
        public virtual TEntity Insert(TEntity entity, bool commit = false)
        {
            return Repository.Insert(entity, commit);
        }

        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, bool commit = false)
        {
            return Repository.Insert(entities, commit);
        }

        public virtual int Delete(TKey id, bool commit = false)
        {
            return Repository.Delete(id, commit);
        }

        public virtual int Delete(IEnumerable<TKey> ids, bool commit = false)
        {
            return Repository.Delete(ids, commit);
        }

        public virtual int Delete(TEntity entity, bool commit = false)
        {
            return Repository.Delete(entity, commit);
        }

        public virtual int Delete(IEnumerable<TEntity> entities, bool commit = false)
        {
            return Repository.Delete(entities, commit);
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool commit = false)
        {
            return Repository.Delete(predicate, commit);
        }

        public virtual int Update(TEntity entity, bool commit = false)
        {
            return Repository.Update(entity, commit);
        }

        public virtual int Update(IEnumerable<TEntity> entities, bool commit = false)
        {
            return Repository.Update(entities, commit);
        }

        public virtual int QueryCount()
        {
            return Repository.QueryCount();
        }

        public virtual int QueryCount(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.QueryCount(predicate);
        }

        public virtual TEntity QuerySingle(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.QuerySingle(predicate);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return Repository.Query();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Query(predicate);
        }

        public virtual IQueryable<TEntity> QueryPage<TField>(int page, int size, out int total, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TField>> keySelector, bool isDesc)
        {
            return Repository.QueryPage<TField>(page, size, out total, predicate, keySelector, isDesc);
        }

        public virtual int ExcuteNonQuery(string sql, params object[] parameters)
        {
            return Repository.ExcuteNonQuery(sql, parameters);
        }

        public virtual IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return Repository.SqlQuery(sql, parameters);
        }

        public virtual int SaveChanges()
        {
            return Repository.SaveChanges();
        }

        public virtual void Dispose()
        {
            //Repository.Dispose();
        }
    }
}
