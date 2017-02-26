using Microsoft.Extensions.Caching.Memory;

namespace SqlMapper.Common
{
    public class ContextCommon
    {
        internal static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
    }
}