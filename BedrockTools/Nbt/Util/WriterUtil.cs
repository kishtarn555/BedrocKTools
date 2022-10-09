using System.IO;
using BedrockTools.Nbt.Elements;
namespace BedrockTools.Nbt.Util {
    public static class WriterUtil {
        public static void WriteString(BinaryWriter writer, string message) {
            writer.Write((short)message.Length);
            foreach(char c in message) {
                writer.Write(c);
            }
        }
        public static void WriteRootCompound(BinaryWriter writer, NbtCompound rootCompound) {
            rootCompound.WriteTag(writer);
            WriteString(writer, "");
            rootCompound.WriteValue(writer);
        }
    }
}
