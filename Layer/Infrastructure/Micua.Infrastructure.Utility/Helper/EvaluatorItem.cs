using System;

namespace Micua.Infrastructure.Utility
{
    /// <summary> 
    /// 可执行字符串项（即一条可执行字符串） 
    /// </summary> 
    public class EvaluatorItem
    {
        /// <summary> 
        /// 返回值类型 
        /// </summary> 
        public Type ReturnType;
        /// <summary> 
        /// 执行表达式 
        /// </summary> 
        public string Expression;
        /// <summary> 
        /// 执行字符串名称 
        /// </summary> 
        public string Name;
        /// <summary> 
        /// 可执行字符串项构造函数 
        /// </summary> 
        /// <param name="returnType">返回值类型</param> 
        /// <param name="expression">执行表达式</param> 
        /// <param name="name">执行字符串名称</param> 
        public EvaluatorItem(Type returnType, string expression, string name)
        {
            ReturnType = returnType;
            Expression = expression;
            Name = name;
        }
    }
}