// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   基于EF的仓储实现基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Micua.Domain.Model;

    /// <summary>
    /// 基于EF的仓储实现基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : EntityBase<TKey>
    {
        protected DbContext DbContext { get { return DbSession.Instance.DbContext; } }

        protected DbSet<TEntity> DbSet { get { return DbContext.Set<TEntity>(); } }

        public IQueryable<TEntity> Entities { get { return DbSet; } }

        public TEntity Insert(TEntity entity, bool commit = false)
        {
            DbSet.Add(entity);
            if (commit) SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, bool commit = false)
        {
            foreach (var entity in entities)
                DbSet.Add(entity);
            if (commit) SaveChanges();
            return entities;
        }

        public int Delete(TKey id, bool commit = false)
        {
            var del = DbSet.Find(id);
            return Delete(del, commit = false);
        }

        public int Delete(IEnumerable<TKey> ids, bool commit = false)
        {
            foreach (var id in ids)
            {
                var del = DbSet.Find(id);
                Delete(del, false);
            }
            return commit ? SaveChanges() : 0;
        }

        public int Delete(TEntity entity, bool commit = false)
        {
            DbSet.Remove(entity);
            return commit ? SaveChanges() : 0;
        }

        public int Delete(IEnumerable<TEntity> entities, bool commit = false)
        {
            DbSet.RemoveRange(entities);
            return commit ? SaveChanges() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool commit = false)
        {
            var dels = DbSet.Where(predicate);
            DbSet.RemoveRange(dels);
            return commit ? SaveChanges() : 0;
        }

        public int Update(TEntity entity, bool commit = false)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return commit ? SaveChanges() : 0;
        }

        public int Update(IEnumerable<TEntity> entities, bool commit = false)
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            return commit ? SaveChanges() : 0;
        }

        public int QueryCount()
        {
            return DbSet.Count();
        }

        public int QueryCount(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public TEntity QuerySingle(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> QueryPage<TField>(int page, int size, out int total, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TField>> keySelector, bool isDesc)
        {
            total = QueryCount(predicate);
            if (isDesc)
                return DbContext.Set<TEntity>()
                        .Where<TEntity>(predicate)
                        .OrderByDescending<TEntity, TField>(keySelector)
                        .Skip<TEntity>(size * (page - 1))
                        .Take<TEntity>(size);
            return DbContext.Set<TEntity>()
                    .Where<TEntity>(predicate)
                    .OrderBy<TEntity, TField>(keySelector)
                    .Skip<TEntity>(size * (page - 1))
                    .Take<TEntity>(size);
        }

        public int ExcuteNonQuery(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(sql, parameters);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
