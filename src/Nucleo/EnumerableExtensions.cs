namespace Nucleo;

public static class EnumerableExtensions
{
    public static IEnumerable<IList<T>> Chunk2<T>(this IEnumerable<T> items, int size, int step)
    {
        if (step > size)
        {
            throw new ArgumentException($"'{nameof(step)}' cannot be larger than '{nameof(size)}'");
        }

        var buffer = new T[size];
        var bufferIndex = 0;

        bool IsBufferFull() => bufferIndex == size;

        var e = items.GetEnumerator();
        while (e.MoveNext())
        {
            if (!IsBufferFull())
            {
                // store current
                buffer[bufferIndex] = e.Current;
                bufferIndex++;
            }

            if (IsBufferFull())
            {
                yield return buffer;

                var itemsToReuse = size - step;
                if (itemsToReuse == 0)
                {
                    buffer = new T[size];
                    bufferIndex = 0;
                }
                else // reuse already materialized data
                {
                    var yieldedBuffer = buffer;

                    buffer = new T[size];

                    Array.Copy(
                        sourceArray: yieldedBuffer,
                        sourceIndex: yieldedBuffer.Length - itemsToReuse,
                        destinationArray: buffer,
                        destinationIndex: 0,
                        length: itemsToReuse);

                    bufferIndex = itemsToReuse;
                }
            }
        }
    }
}
