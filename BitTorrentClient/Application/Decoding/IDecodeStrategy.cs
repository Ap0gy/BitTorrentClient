namespace BitTorrentClient.Application.Decoding
{
    public interface IDecodeStrategy
    {
        object Decode(IEnumerator<byte> enumerator);
    }
}
