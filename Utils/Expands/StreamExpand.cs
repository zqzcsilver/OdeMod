using System.Collections.Generic;
using System.IO;

namespace OdeMod.Utils.Expands
{
    internal static class StreamExpand
    {
        public static byte[] GetByteArray(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            List<byte> bytes = new List<byte>();
            int b = stream.ReadByte();
            while (b != -1)
            {
                bytes.Add((byte)b);
                b = stream.ReadByte();
            }
            return bytes.ToArray();
        }
    }
}