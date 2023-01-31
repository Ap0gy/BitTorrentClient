using BitTorrentClient.Application.DecodingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrentClient.Application
{
    public interface IStrategyDirector<TKey, TStrategy>
    {
        TStrategy GetDecodeStrategy(TKey current, IStrategyCache<TKey, TStrategy> strategyCache);
    }

    public class DecodeStrategyDirector : IStrategyDirector<byte, IDecodeStrategy>
    {
        private const byte DictionaryStart = (byte)'d';
        private const byte DictionaryEnd = (byte)'e';
        private const byte ListStart = (byte)'l';
        private static byte ListEnd = (byte)'e';
        private const byte NumberStart = (byte)'i';


        public DecodeStrategyDirector() { }

        public IDecodeStrategy GetDecodeStrategy(byte current, IStrategyCache<byte, IDecodeStrategy> strategyCache) => current switch
        {
            DictionaryStart => new DecodeDictionaryStrategy(),
            ListStart => new DecodeListStrategy(strategyCache),
            NumberStart => new NumberDecodeStrategy(),
            _ => new DecodeByteArrayStrategy()
        };
    }
}
