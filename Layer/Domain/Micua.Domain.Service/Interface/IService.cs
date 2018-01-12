// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 数据库访问层基接口
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
    /// 数据库访问层基接口
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:16 Created By iceStone
    /// </remarks>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public interface IService<TKey, TEntity> : IDisposable where TEntity : EntityBase<TKey>
    {
        #region Methods
        /// <summary>
        /// 向数据库表中插入一个对象记录
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="entity">要插入的实体对象</param>
        /// <param name="commit">是否提交到数据库</param>
        /// <returns>插入完成的实体</returns>
        TEntity Insert(TEntity entity, bool commit = false);
        /// <summary>
        /// 向数据库中插入多条数据
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="entities">要插入的实体</param>
        /// <param name="commit">是否提交到数据库</param>
        /// <returns>当前实体</returns>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, bool commit = false);
        /// <summary>
        /// 删除指定编号的记录
        /// </summary>
        /// <param name="id">实体记录编号 </param>
        /// <param name="commit">是否执行保存 </param>
        /// <returns>操作影响的行数 </returns>
        int Delete(TKey id, bool commit = false);
        /// <summary>
        /// 删除指定编号的记录
        /// </summary>
        /// <param name="id">实体记录编号 </param>
        /// <param name="commit">是否执行保存 </param>
        /// <returns>操作影响的行数 </returns>
        int Delete(IEnumerable<TKey> ids, bool commit = false);
        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entity">实体对象 </param>
        /// <param name="commit">是否执行保存 </param>
        /// <returns>操作影响的行数 </returns>
        int Delete(TEntity entity, bool commit = false);
        /// <summary>
        /// 删除实体记录集合
        /// </summary>
        /// <param name="entities">实体记录集合 </param>
        /// <param name="commit">是否执行保存 </param>
        /// <returns>操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool commit = false);
        /// <summary>
        /// 删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式 </param>
        /// <param name="commit">是否执行保存 </param>
        /// <returns>操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool commit = false);
        /// <summary>
        /// 更新数据库表中的一个实体
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="entity">要更新的实体</param>
        /// <param name="commit">是否提交到数据库</param>
        /// <returns>执行结果受影响行数</returns>
        int Update(TEntity entity, bool commit = false);
        /// <summary>
        /// 更新数据库中的多个实体
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="entities">要更新的实体</param>
        /// <param name="commit">是否提交到数据库</param>
        /// <returns>执行结果受影响行数</returns>
        int Update(IEnumerable<TEntity> entities, bool commit = false);

        /// <summary>
        /// 根据传入委托查询出存在条数
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:05 Created By iceStone
        /// </remarks>
        /// <returns>存在条数</returns>
        int QueryCount();
        /// <summary>
        /// 根据传入委托查询出存在条数
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="predicate">筛选条件表达式</param>
        /// <returns>存在条数</returns>
        int QueryCount(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据传入委托筛选出对应的单个数据实体
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="predicate">筛选条件表达式</param>
        /// <returns>查询到单个数据实体</returns>
        TEntity QuerySingle(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 选取数据库中的实体集合
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:05 Created By iceStone
        /// </remarks>
        /// <returns>实体集合</returns>
        IQueryable<TEntity> Query();
        /// <summary>
        /// 根据传入委托筛选出对应的数据实体集合
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="predicate">筛选条件表达式</param>
        /// <returns>查询到数据实体集合</returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据传入筛选委托和分页条件筛选出对应页面的数据实体集合
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <typeparam name="TField">排序字段类型</typeparam>
        /// <param name="page">当前页码</param>
        /// <param name="size">当前页大小</param>
        /// <param name="total">输出查询到的记录数</param>
        /// <param name="predicate">筛选条件委托</param>
        /// <param name="keySelector">排序字段委托</param>
        /// <param name="isDesc">是否降序</param>
        /// <returns>分页查询到数据实体集合</returns>
        IQueryable<TEntity> QueryPage<TField>(int page, int size, out int total, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TField>> keySelector, bool isDesc);
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>受影响行数</returns>
        int ExcuteNonQuery(string sql, params object[] parameters);
        /// <summary>
        /// 执行一个原始SQL查询，返回泛型类型迭代器
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>泛型类型迭代器</returns>
        IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters);
        /// <summary>
        /// 保存数据库的改变状态
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:16 Created By iceStone
        /// </remarks>
        /// <returns>受影响行数</returns>
        int SaveChanges();
        #endregion
    }
}
