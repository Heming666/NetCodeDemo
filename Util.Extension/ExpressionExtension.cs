using System;
using System.Linq;
using System.Linq.Expressions;

namespace Util.Extension
{
    public static class ExpressionExtension
    {
        /// <summary>
        /// 单元 true 表达式
        /// </summary>
        /// <typeparam name="T">指定泛型 T</typeparam>
        /// <returns>true</returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return item => true;
        }

        /// <summary>
        /// 单元 false 表达式
        /// </summary>
        /// <typeparam name="T">指定泛型 T</typeparam>
        /// <returns>false</returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return item => false;
        }

        /// <summary>
        /// 双元 Or 表达式
        /// </summary>
        /// <typeparam name="T">指定泛型 T</typeparam>
        /// <param name="exprleft">左表达式</param>
        /// <param name="exprright">右表达式</param>
        /// <returns>返回合并表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exprleft, Expression<Func<T, bool>> exprright)
        {
            if (exprleft == null) { return exprright; }
            var invokedExpr = Expression.Invoke(exprright, exprleft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exprleft.Body, invokedExpr), exprleft.Parameters);
        }

        /// <summary>
        /// 双元 And 表达式
        /// </summary>
        /// <typeparam name="T">指定泛型 T</typeparam>
        /// <param name="exprleft">左表达式</param>
        /// <param name="exprright">右表达式</param>
        /// <returns>返回合并表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exprleft, Expression<Func<T, bool>> exprright)
        {
            if (exprleft == null) { return exprright; }
            var invokedExpr = Expression.Invoke(exprright, exprleft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exprleft.Body, invokedExpr), exprleft.Parameters);
        }
    }
}
