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
    private static byte NumberEnd = (byte)'e';
    private static byte ByteArrayDivider = (byte)':';

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

    private static byte[] DecodeByteArray(IEnumerator<byte> enumerator)
    {
        List<byte> lengthBytes = new();

        do
        {
            if (enumerator.Current == ByteArrayDivider)
                break;

            lengthBytes.Add(enumerator.Current);
        }
        while (enumerator.MoveNext());

        string lengthString = Encoding.UTF8.GetString(lengthBytes.ToArray());

        if (!int.TryParse(lengthString, out var length))
            throw new Exception("unable to parse length of byte array");

        byte[] bytes = new byte[length];

        for (int i = 0; i < length; i++)
        {
            enumerator.MoveNext();
            bytes[i] = enumerator.Current;
        }

        return bytes;
    }

    private static long DecodeNumber(IEnumerator<byte> enumerator)
    {
        List<byte> bytes = new();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current == NumberEnd)
                break;

            bytes.Add(enumerator.Current);
        }

        string numberAsString = Encoding.UTF8.GetString(bytes.ToArray());

        return Int64.Parse(numberAsString);
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

        return Decode(bytes);
    }
}