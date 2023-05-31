using System;

namespace Nada.NZazu.Contracts.Suggest;

public delegate void CacheEventHandler<TKey, TValue>(object sender, Tuple<TKey, TValue> e);