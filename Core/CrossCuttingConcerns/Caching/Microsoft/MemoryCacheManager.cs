using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //Adapter Pattern : var olan sistemi  kendi sistemime uyarlıyorum
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;//belleğe bakıyor
        public MemoryCacheManager()
        {
            //enjekte edilmiş bütün interfaceleri böyle çekebiliriz
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();//ımemorycache in karşlığını ver
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key , out _);
            //1 şeyy döndürmek istemiyorsam > out _
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)//! ona verdi. patterna göre silme işlemi yapıcak -- çalışma anında bellekten silmeye yarıyor
        {
            //git belleğe bak , bellekte memorycache türünde olan enti.collec. ı bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;//definition ı memory cache olanları bul 
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)//her bir cache elemanını gez 
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            //şu kurala uyanlar 
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);//pattern oluşturma
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();//benim  gönderd. değerlerden uyan varsa keystoRemove a atıcak

            foreach (var key in keysToRemove)//keylerini buluyorum ve remove ediyorum
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
