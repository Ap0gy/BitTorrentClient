using BitTorrentClient.Application.DecodingStrategies;
using System.Text;

namespace BitTorrentClient.Application;

public class BEncoding
{
    //Strings 9:somewords
    //Integers i4e
    //Lists l4spam:3lole
    //Dictsd d8:greeting5:hello4:name4:jakee
    private const byte DictionaryStart = (byte)'d';
    private const byte DictionaryEnd = (byte)'e';
    private const byte ListStart = (byte)'l';
    private static byte ListEnd = (byte)'e';
    private const byte NumberStart = (byte)'i';

    public static object Decode(byte[] bytes)
    {
        var strategyCache = new StrategyCache<byte, IDecodeStrategy, DecodeStrategyDirector>();
        IEnumerator<byte> enumerator = (IEnumerator<byte>)bytes.GetEnumerator();
        enumerator.MoveNext();
        return strategyCache[enumerator.Current].Decode(enumerator);
    }

    public static object DecodeFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File path {path} does not exist");

        byte[] bytes = File.ReadAllBytes(path);

        return Decode(bytes);
    }
}