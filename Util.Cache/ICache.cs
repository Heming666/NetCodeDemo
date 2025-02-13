﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Cache
{
    /// <summary>
    /// 缓存处理类
    /// </summary>
    public interface ICache 
    {
        /// <summary>
        /// 更改数据库实例
        /// </summary>
        /// <param name="db"></param>
        /// <param name="connStr"></param>
        void ChangeDB(int db = 0, string connStr = null);
        Task<bool> SetStringKey(string key, string value, TimeSpan? expiry = default);
        Task<string> GetStringKey(string key);
        Task<T> GetStringKey<T>(string key) where T : class, new();
        Task<bool> SetStringKey<T>(string key, T obj, TimeSpan? expiry = default) where T : class, new();
        Task<bool> SetList<T>(string key, T obj, TimeSpan? expiry = default) where T : class, new();
        Task<bool> SetList<T>(string key, List<T> objList, TimeSpan? expiry = default) where T : class, new();
        Task<List<T>> GetList<T>(string key) where T : class, new();
        Task<bool> HashSet<T>(string key, T obj, string fielId, TimeSpan? expiry = default) where T : class, new();
        Task<bool> HashSet(string key, HashEntry[] hashEntries, TimeSpan? expiry = default);
        Task<List<T>> HashGetAll<T>(string key, TimeSpan? expiry = default) where T : class, new();
        Task<List<T>> HashGet<T>(string key, RedisValue[] fielIds, TimeSpan? expiry = default) where T : class, new();
        Task<T> HashGet<T>(string key, string fielId, TimeSpan? expiry = default) where T : class, new();
        Task<bool> KeyExistsAsync(string key);
    }
}
