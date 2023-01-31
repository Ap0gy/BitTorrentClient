using BitTorrentClient.Application.Decoding;

namespace BitTorrentClient.Application
{
    public interface IStrategyCache<TKey, TStrategy>
    {
        TStrategy this[TKey key] { get; }
    }

    public class StrategyCache<TKey, TStrategy, TStrategyDirector> : IStrategyCache<TKey, TStrategy> where TStrategyDirector : IStrategyDirector<TKey, TStrategy>, new()
    {
        private readonly TStrategyDirector _strategyDirector;

        private readonly Dictionary<TKey, TStrategy> _strategies;

        public StrategyCache()
        {
            _strategies = new Dictionary<TKey, TStrategy>();
            _strategyDirector = new TStrategyDirector();
        }

        public TStrategy this[TKey key]
        {
            get
            {
                if (_strategies.TryGetValue(key, out TStrategy strategy))
                    return strategy;

                strategy = _strategyDirector.GetStrategy(key, this);

                _strategies.Add(key, strategy);

                return strategy;
            }
        }
    }
}
