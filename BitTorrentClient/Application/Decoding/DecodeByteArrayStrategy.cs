namespace BitTorrentClient.Application.Decoding;

internal class DecodeByteArrayStrategy : IDecodeStrategy
{
    private const byte ByteArrayDivider = (byte)'e';

    public object Decode(IEnumerator<byte> enumerator)
    {
        List<byte> lengthBytes = new List<byte>();

        do
        {
            if (enumerator.Current == ByteArrayDivider)
                break;

            lengthBytes.Add(enumerator.Current);
        }
        while (enumerator.MoveNext());

        string lengthString = System.Text.Encoding.UTF8.GetString(lengthBytes.ToArray());

        int length;
        if (!int.TryParse(lengthString, out length))
        {
            throw new Exception("Unable to parse the length of the byte array");
        }

        byte[] bytes = new byte[length];

        for (int i = 0; i < length; i++)
        {
            enumerator.MoveNext();
            bytes[i] = enumerator.Current;
        }

        return bytes;
    }
}
