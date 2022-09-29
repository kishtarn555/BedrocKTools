using System.IO;

namespace BedrockTools.Nbt.Util {
    internal static class WriterUtil {
        public static void WriteString(BinaryWriter writer, string message) {
            writer.Write((short)message.Length);
            foreach(char c in message) {
                writer.Write(c);
            }
        }

        public static void WriteRootCompound(BinaryWriter writer, NbtCompoundBase rootCompound) {
            rootCompound.WriteTag(writer);
            WriteString(writer, "");
            rootCompound.WriteValue(writer);
        }
    }
}
