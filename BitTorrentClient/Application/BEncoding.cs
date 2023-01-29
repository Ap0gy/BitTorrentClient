using System.Text;

namespace BitTorrentClient.Application;

public class BEncoding
{
    //Strings 9:somewords
    //Integers i4e
    //Lists l4spam:3lole
    //Dictsd d8:greeting5:hello4:name4:jakee
    private const byte DictionaryStart = 100; // d
    private static byte DictionaryEnd = System.Text.Encoding.UTF8.GetBytes("e")[0]; // 101
    private const byte ListStart = 108; // l
    private static byte ListEnd = System.Text.Encoding.UTF8.GetBytes("e")[0]; // 101
    private const byte NumberStart = 105; // i
    private static byte NumberEnd = System.Text.Encoding.UTF8.GetBytes("e")[0]; // 101
    private static byte ByteArrayDivider = System.Text.Encoding.UTF8.GetBytes(":")[0]; //  58

    public static object Decode(byte[] bytes)
    {
        IEnumerator<byte> enumerator = (IEnumerator<byte>)bytes.GetEnumerator();
        enumerator.MoveNext();
        return DecodeNextObject(enumerator);
    }

    private static object DecodeNextObject(IEnumerator<byte> enumerator) => enumerator.Current switch
    {
        DictionaryStart => DecodeDictionary(enumerator),
        ListStart => DecodeList(enumerator),
        NumberStart => DecodeNumber(enumerator),
        _ => DecodeByteArray(enumerator)
    };

    private static object DecodeByteArray(IEnumerator<byte> enumerator)
    {
        throw new NotImplementedException();
    }
    private static object DecodeNumber(IEnumerator<byte> enumerator)
    {
        throw new NotImplementedException();
    }
    private static object DecodeList(IEnumerator<byte> enumerator)
    {
        throw new NotImplementedException();
    }
    private static object DecodeDictionary(IEnumerator<byte> enumerator)
    {
        throw new NotImplementedException();
    }

    public static object DecodeFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File path {path} does not exist");

        byte[] bytes = File.ReadAllBytes(path);

        return BEncoding.Decode(bytes);
    }
}