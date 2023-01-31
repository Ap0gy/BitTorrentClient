namespace BitTorrentClient.Application.Decoding
{
    public class DecodeListStrategy : IDecodeStrategy
    {
        private static byte ListEnd = (byte)'e';
        private readonly IStrategyCache<byte, IDecodeStrategy> _strategyCache;

        public DecodeListStrategy(IStrategyCache<byte, IDecodeStrategy> strategyCache)
        {
            _strategyCache = strategyCache;
        }

        public IStrategyCache<byte, IDecodeStrategy> StrategyCache { get; }

        public object Decode(IEnumerator<byte> enumerator)
        {
            List<object> list = new();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current == ListEnd)
                    break;

                list.Add(_strategyCache[enumerator.Current].Decode(enumerator));
            }

            return list;
        }
    }
}
