using System.Text;

namespace BitTorrentClient.Application.Decoding;
internal class DecodeDictionaryStrategy : IDecodeStrategy
{
    private const byte DictionaryEnd = (byte)'e';
    private readonly IStrategyCache<byte, IDecodeStrategy> _strategyCache;
    private DecodeByteArrayStrategy _decodeByteArrayStrategy = new();

    public object Decode(IEnumerator<byte> enumerator)
    {
        Dictionary<string, object> dict = new();
        List<string> keys = new();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current == DictionaryEnd)
                break;


            string key = Encoding.UTF8.GetString((byte[])_decodeByteArrayStrategy.Decode(enumerator));
            enumerator.MoveNext();
            object val = _strategyCache[enumerator.Current].Decode(enumerator);

            keys.Add(key);
            dict.Add(key, val);
        }

        var sortedKeys = keys.OrderBy(x => BitConverter.ToString(Encoding.UTF8.GetBytes(x)));
        if (keys.SequenceEqual(sortedKeys) is false)
        {
            throw new Exception("Error loading dictionary. Keys not sorted.");
        }

        return dict;
    }
}