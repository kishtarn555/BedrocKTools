using System.IO;
using System.Text;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Advanced.Vox {
    public class MagicaVoxImporter {
        IBlockPalette palette;
        McStructure buildObject;
        public MagicaVoxImporter() {
            palette = new BlockPaletteArray(new Block[] { VanillaBlockFactory.Quartz(Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default) });
        }

        public MagicaVoxImporter(IBlockPalette palette) {
            this.palette = palette;            
        }

        public Block GetBlock(byte index) {            
            return palette[index];
        }

        string readTag(BinaryReader reader) {
            return Encoding.ASCII.GetString(reader.ReadBytes(4));
        }
        bool readTag(BinaryReader reader, string expected) {
            string val = Encoding.ASCII.GetString(reader.ReadBytes(4));
            if (expected != val) {
                throw new System.Exception($"While reading a VOX file, expected a tag of type: {expected}, but found {val}");
            }
            return true;
        }


        int GetMFromMainChunk(BinaryReader reader) {
            readTag(reader, "MAIN");
            reader.ReadInt32();
            int M = reader.ReadInt32();
            return M;
        }
        void skipChunk(BinaryReader reader) {
            int N = reader.ReadInt32();
            int M = reader.ReadInt32();
            for (int i = 0;i < N+M; i++) {
                reader.ReadByte();
            }
        }

        void ReadSizeChunk(BinaryReader reader) {

            string response = "!";
            while (response != "SIZE") {
                response = readTag(reader);
                if (response != "SIZE") skipChunk(reader);
            }
            reader.ReadInt32();
            reader.ReadInt32();
            int X =   reader.ReadInt32();
            int Z =   reader.ReadInt32();
            int Y =   reader.ReadInt32();
            buildObject = new McStructure(new Dimensions(X , Y, Z)); 
        }

        void ReadXYZIChunk(BinaryReader reader) {
            readTag(reader, "XYZI");
            reader.ReadInt32();
            reader.ReadInt32();
            int N = reader.ReadInt32();
            for (int i =0; i < N; i++) {
                byte x = reader.ReadByte();
                byte z = reader.ReadByte();
                byte y = reader.ReadByte();
                byte idx = reader.ReadByte();

                buildObject.SetBlock(x, y, z, GetBlock(idx));
            }
        }

        public McStructure Import(string path) {

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open))) {
                readTag(reader, "VOX ");
                int version = reader.ReadInt32();
                int M = GetMFromMainChunk(reader);
                ReadSizeChunk(reader);
                ReadXYZIChunk(reader);
                
            }
            return buildObject;
        }
    }
}
w