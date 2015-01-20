﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="list"></param>
        /// <param name="selectorList"></param>
        public static void Sort<T, TKey>(this List<T> list, params Func<T, TKey>[] keySelectors)
        {
            List<Comparison<T>> l = new List<Comparison<T>>();

            foreach (var selector in keySelectors)
            {
                l.Add((x, y) => Comparer<TKey>.Default.Compare(selector(x), selector(y)));
            }
            list.Sort<T>(l.ToArray());
        }
        /// <summary>
        /// 指定した優先度順で並び替えを実行します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparisonList"></param>
        public static void Sort<T>(this List<T> list, params Comparison<T>[] comparisonList)
        {
            Comparison<T> md = (x, y) =>
            {
                foreach (Comparison<T> t in comparisonList)
                {
                    int z = t(x, y);
                    if (z == 0)
                    { continue; }
                    return z;
                }
                return 0;
            };
            list.Sort(md);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddIfNotExist<T>(this List<T> list, T item)
        {
            AddIfNotExist(list, item, (x, y) => Object.Equals(x, y));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="equalityFunc"></param>
        public static void AddIfNotExist<T>(this List<T> list, T item, Func<T, T, Boolean> equalityFunc)
        {
            if (list.Exists(el => equalityFunc(item, el)) == false)
            {
                list.Add(item);
            }
        }
    }
}
