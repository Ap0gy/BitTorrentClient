using BitTorrentClient.Application.Decoding;
using System.Text;

namespace BitTorrentClient.Application;

public class BEncoding
{
    public static object DecodeFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File path {path} does not exist.");

        byte[] bytes = File.ReadAllBytes(path);

        return Decode(bytes);
    }

    public static object Decode(byte[] bytes)
    {
        var strategyCache = new StrategyCache<byte, IDecodeStrategy, DecodeStrategyDirector>();
        IEnumerator<byte> enumerator = (IEnumerator<byte>)bytes.GetEnumerator();
        enumerator.MoveNext();
        return strategyCache[enumerator.Current].Decode(enumerator);
    }
}