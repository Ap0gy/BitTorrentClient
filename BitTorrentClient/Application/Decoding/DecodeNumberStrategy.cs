using System.Text;

namespace BitTorrentClient.Application.Decoding;

internal class DecodeNumberStrategy : IDecodeStrategy
{
    private const byte NumberEnd = (byte)'e';

    public object Decode(IEnumerator<byte> enumerator)
    {
        List<byte> bytes = new();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current == NumberEnd)
                break;

            bytes.Add(enumerator.Current);
        }

        string numAsString = Encoding.UTF8.GetString(bytes.ToArray());

        return Int64.Parse(numAsString);
    }
}
