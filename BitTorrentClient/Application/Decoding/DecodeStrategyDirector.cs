namespace BitTorrentClient.Application.Decoding
{
    public interface IStrategyDirector<TKey, TStrategy>
    {
        TStrategy GetStrategy(TKey current, IStrategyCache<TKey, TStrategy> strategyCache);
    }

    public class DecodeStrategyDirector : IStrategyDirector<byte, IDecodeStrategy>
    {
        private const byte DictionaryStart = (byte)'d';
        private const byte ListStart = (byte)'l';
        private const byte NumberStart = (byte)'i';


        public DecodeStrategyDirector() { }

        public IDecodeStrategy GetStrategy(byte current, IStrategyCache<byte, IDecodeStrategy> strategyCache) => current switch
        {
            DictionaryStart => new DecodeDictionaryStrategy(),
            ListStart => new DecodeListStrategy(strategyCache),
            NumberStart => new DecodeNumberStrategy(),
            _ => new DecodeByteArrayStrategy()
        };
    }
}
