using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Cache
{
    public class RedisCache : ICache<RedisCache>
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        private  string __connStr;
        private static ConnectionMultiplexer redisMultiplexer;
        static IDatabase db = null;
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="connStr">链接字符串</param>
        public RedisCache(string connStr)
        {
            __connStr = connStr;
            try
            {
                redisMultiplexer = ConnectionMultiplexer.Connect(connStr);
                db = redisMultiplexer.GetDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                redisMultiplexer = null;
                db = null;
                throw;
            }
        }
        /// <summary>
        /// 一个新的数据库实例
        /// </summary>
        /// <param name="db">要连接的db</param>
        /// <param name="connStr">连接字符串</param>
        /// <returns></returns>
        public RedisCache Current(int dbIndex = 0, string connStr = null)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                __connStr =connStr;
            }
            redisMultiplexer = ConnectionMultiplexer.Connect(connStr);
            db = redisMultiplexer.GetDatabase(dbIndex);
            return this;
        }
        public Task<bool> SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return default;
                }
                return db.StringSet(key, value, expiry);
            });
        }
        public Task<string> GetStringKey(string key)
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return default;
                }
                RedisValue value = db.StringGet(key);
                if (value.HasValue)
                    return value.ToString();
                else
                    return null;
            });
        }
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        public Task<T> GetStringKey<T>(string key) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return default;
                }
                var value = db.StringGet(key);
                if (value.IsNullOrEmpty)
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(value);
            });
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="obj"></param>
        public Task<bool> SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return false;
                }
                string json = JsonConvert.SerializeObject(obj);
                return db.StringSet(key, json, expiry);
            });
        }
        public Task<bool> SetList<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return false;
                }
                string json = JsonConvert.SerializeObject(obj);
                return db.ListLeftPushAsync(key, json).Result > 0;
            });
        }
        public Task<bool> SetList<T>(string key, List<T> objList, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return false;
                }
                List<RedisValue> redisValues = new List<RedisValue>();
                if (objList != null && objList.Count > 0)
                {
                    foreach (var item in objList)
                    {
                        redisValues.Add(JsonConvert.SerializeObject(item));
                    }

                    return db.ListLeftPushAsync(key, redisValues.ToArray()).Result > 0;
                }
                return false;
            });
        }
        public Task<List<T>> GetList<T>(string key, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return null;
                }
                var value = db.ListRangeAsync(key);
                if (value != null && value.Result.Length > 0)
                {
                    List<T> list = new List<T>();
                    foreach (var item in value.Result)
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                    return list;
                }
                return null;
            });
        }
        public Task<bool> HashSet<T>(string key, T obj, string fielId, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return false;
                }
                string json = JsonConvert.SerializeObject(obj);
                return db.HashSet(key, fielId, json);
            });
        }
        public Task<bool> HashSet(string key, HashEntry[] hashEntries, TimeSpan? expiry = default(TimeSpan?))
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return false;
                }
                db.HashSet(key, hashEntries);
                return true;
            });
        }
        public Task<List<T>> HashGetAll<T>(string key, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return null;
                }
                var value = db.HashGetAll(key);
                if (value != null && value.Length > 0)
                {
                    List<T> list = new List<T>();
                    foreach (var item in value)
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(item.Value));
                    }
                    return list;
                }
                return null;
            });
        }
        public Task<List<T>> HashGet<T>(string key, RedisValue[] fielIds, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return null;
                }
                var value = db.HashGet(key, fielIds);
                if (value != null && value.Length > 0)
                {
                    List<T> list = new List<T>();
                    foreach (var item in value)
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                    return list;
                }
                return null;
            });
        }
        public Task<T> HashGet<T>(string key, string fielId, TimeSpan? expiry = default(TimeSpan?)) where T : class, new()
        {
            return Task.Run(() =>
            {
                if (db == null)
                {
                    return null;
                }
                var value = db.HashGet(key, fielId);
                if (value.IsNullOrEmpty)
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(value);
            });
        }
   
    }
}
