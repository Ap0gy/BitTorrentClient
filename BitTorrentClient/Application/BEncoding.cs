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
        IEnumerator<byte> enumerator = (IEnumerator<byte>)bytes.GetEnumerator();
        enumerator.MoveNext();
        return DecodeNextObject(enumerator);
    }

    public static object DecodeFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File path {path} does not exist");

        byte[] bytes = File.ReadAllBytes(path);

        return Decode(bytes);
    }

    private static object DecodeNextObject(IEnumerator<byte> enumerator) => enumerator.Current switch
    {
        DictionaryStart => DecodeDictionary(enumerator),
        ListStart => DecodeList(enumerator),
        NumberStart => DecodeNumber(enumerator),
        _ => DecodeByteArray(enumerator)
    };


    private static List<object> DecodeList(IEnumerator<byte> enumerator)
    {
        List<object> list = new();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current == ListEnd)
                break;

            list.Add(DecodeNextObject(enumerator));
        }

        return list;
    }
}