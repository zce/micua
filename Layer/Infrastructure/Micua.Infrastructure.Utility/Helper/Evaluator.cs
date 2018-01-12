// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Evaluator.cs" company="Wedn.Net">
//   Copyright ? 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   动态执行类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Microsoft.CSharp;

    /// <summary> 
    /// 动态执行类
    /// </summary> 
    public class Evaluator
    {
        #region 私有成员
        /// <summary> 
        /// 用于动态引用生成的类，执行其内部包含的可执行字符串 
        /// </summary> 
        object _compiled;
        #endregion

        #region 构造函数
        /// <summary> 
        /// 可执行串的构造函数 
        /// </summary> 
        /// <param name="items"> 
        /// 可执行字符串数组 
        /// </param> 
        public Evaluator(IEnumerable<EvaluatorItem> items)
        {
            this.ConstructEvaluator(items);      // 调用解析字符串构造函数进行解析 
        }

        /// <summary> 
        /// 可执行串的构造函数 
        /// </summary> 
        /// <param name="returnType">返回值类型</param> 
        /// <param name="expression">执行表达式</param> 
        /// <param name="name">执行字符串名称</param> 
        public Evaluator(Type returnType, string expression, string name)
        {
            // 创建可执行字符串数组 
            EvaluatorItem[] items = { new EvaluatorItem(returnType, expression, name) };
            this.ConstructEvaluator(items);      // 调用解析字符串构造函数进行解析 
        }

        /// <summary> 
        /// 可执行串的构造函数 
        /// </summary> 
        /// <param name="item">可执行字符串项</param> 
        public Evaluator(EvaluatorItem item)
        {
            EvaluatorItem[] items = { item }; // 将可执行字符串项转为可执行字符串项数组 
            this.ConstructEvaluator(items);      // 调用解析字符串构造函数进行解析 
        }

        /// <summary> 
        /// 解析字符串构造函数 
        /// </summary> 
        /// <param name="items">待解析字符串数组</param> 
        private void ConstructEvaluator(IEnumerable<EvaluatorItem> items)
        {
            var provider = new CSharpCodeProvider();

            // 创建C#编译器实例 
            ICodeCompiler comp = provider.CreateCompiler();

            // 编译器的传入参数 
            var cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");              // 添加程序集 system.dll 的引用 
            cp.ReferencedAssemblies.Add("system.data.dll");         // 添加程序集 system.data.dll 的引用 
            cp.ReferencedAssemblies.Add("system.xml.dll");          // 添加程序集 system.xml.dll 的引用 
            cp.GenerateExecutable = false;                          // 不生成可执行文件 
            cp.GenerateInMemory = true;                             // 在内存中运行 

            var code = new StringBuilder();               // 创建代码串 

            // 添加常见且必须的引用字符串
            code.Append("using System; \n");
            code.Append("using System.Data; \n");
            code.Append("using System.Data.SqlClient; \n");
            code.Append("using System.Data.OleDb; \n");
            code.Append("using System.Xml; \n");

            code.Append("namespace Micua.Temp { \n");                  // 生成代码的命名空间为EvalGuy，和本代码一样 

            code.Append(" public class _Evaluator { \n");          // 产生 _Evaluator 类，所有可执行代码均在此类中运行 
            // 遍历每一个可执行字符串项 
            foreach (EvaluatorItem item in items)
            {   
                // 添加定义公共函数代码 
                // 函数返回值为可执行字符串项中定义的返回值类型 
                // 函数名称为可执行字符串项中定义的执行字符串名称 
                code.AppendFormat("    public {0} {1}(object obj) ", item.ReturnType.Name, item.Name);

                code.Append("{ ");                                  // 添加函数开始括号 
                // code.AppendFormat("return ({0});", item.Expression);//添加函数体，返回可执行字符串项中定义的表达式的值
                code.AppendFormat(item.Expression); // 添加函数体，返回可执行字符串项中定义的表达式的值
                code.Append("}\n"); // 添加函数结束括号 
            }

            code.Append("} }");                                 // 添加类结束和命名空间结束括号 

            // 得到编译器实例的返回结果 
            CompilerResults cr = comp.CompileAssemblyFromSource(cp, code.ToString());

            // 如果有错误 
            if (cr.Errors.HasErrors)                            
            {
                var error = new StringBuilder();          // 创建错误信息字符串 
                error.Append("编译有错误的表达式: ");                // 添加错误文本 
                // 遍历每一个出现的编译错误 
                foreach (CompilerError err in cr.Errors)
                {
                    // 添加进错误文本，每个错误后换行 
                    error.AppendFormat("{0}\n", err.ErrorText);
                }

                throw new Exception("编译错误: " + error); // 抛出异常 
            }

            Assembly a = cr.CompiledAssembly;                       // 获取编译器实例的程序集 
            this._compiled = a.CreateInstance("Micua.Temp._Evaluator");     // 通过程序集查找并声明 EvalGuy._Evaluator 的实例 
        }
        #endregion

        #region 公有成员

        /// <summary> 
        /// 执行字符串并返回整型值 
        /// </summary> 
        /// <param name="name">执行字符串名称</param>
        /// <param name="obj">参数对象</param>
        /// <returns>执行结果</returns> 
        public int EvaluateInt(string name, object obj)
        {
            return (int)this.Evaluate(name, obj);
        }

        /// <summary> 
        /// 执行字符串并返回字符串型值 
        /// </summary> 
        /// <param name="name">执行字符串名称</param> 
        /// <param name="obj">参数对象</param>
        /// <returns>执行结果</returns> 
        public string EvaluateString(string name, object obj)
        {
            return (string)this.Evaluate(name, obj);
        }

        /// <summary> 
        /// 执行字符串并返回布尔型值 
        /// </summary> 
        /// <param name="name">执行字符串名称</param> 
        /// <param name="obj">参数对象</param>
        /// <returns>执行结果</returns> 
        public bool EvaluateBool(string name, object obj)
        {
            return (bool)this.Evaluate(name, obj);
        }

        /// <summary> 
        /// 执行字符串并返 object 型值 
        /// </summary> 
        /// <param name="name">执行字符串名称</param> 
        /// <param name="obj">参数对象</param>
        /// <returns>执行结果</returns> 
        public object Evaluate(string name, object obj)
        {
            MethodInfo mi = this._compiled.GetType().GetMethod(name); // 获取 _Compiled 所属类型中名称为 name 的方法的引用 
            object[] objs = { obj };
            return mi.Invoke(this._compiled, objs); // 执行 mi 所引用的方法
        }
        #endregion
    }
}