namespace BitTorrentClient.Application.DecodingStrategies
{
    public interface IDecodeStrategy
    {
        object Decode(IEnumerator<byte> data);
    }
}
