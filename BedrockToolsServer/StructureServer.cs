using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.IO;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure;
using BedrockTools.Structure.Features.Geometry;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockToolsServer {
    internal class StructureServer {
        public string InputHandle;
        public string OutputHandle;
        public bool Running {
            get; protected set;
        }

        public StructureServer(string inputHandle, string outputHandle) {
            InputHandle = inputHandle;
            OutputHandle = outputHandle;
            Running = false;
        }

        public void Start() {
            using (var pipeRead = new AnonymousPipeClientStream(PipeDirection.In, InputHandle))
            using (var pipeWrite = new AnonymousPipeClientStream(PipeDirection.Out, OutputHandle)) {
                try {
                    var sr = new StreamString(pipeRead);
                    var sw = new StreamString(pipeWrite);
                    Console.WriteLine("Server started");
                    Running = true;
                    while (Running) {
                        if (!pipeRead.IsConnected || !pipeWrite.IsConnected) {
                            Running = false;
                            break;
                        }
                        Communicate(sr, sw,pipeRead, pipeWrite);
                    }
                    

                }
                catch (Exception ex) {
                    throw;
                }
            }
        }
        void Communicate(StreamString sr, StreamString sw, AnonymousPipeClientStream pipeRead, AnonymousPipeClientStream pipeWrite) {
            Console.WriteLine("Sending structure");
            McStructure structure = new McStructure(new Dimensions(10, 10, 10));
            SphereFeature sphere = new SphereFeature(structure.Size, FillMode.BorderThick, VanillaBlockFactory.Purpur());
            sphere.PlaceInStructure(McTransform.Identity, structure);
            NbtCompound nbt = new McStructureSerializer(structure).GetStructureAsNbt();
            StringWriter stringWriter = new StringWriter();
            new SNbtWriter(stringWriter).Write(nbt);

            string response = stringWriter.ToString();
            sw.WriteString(response);
            pipeWrite.Flush();
            Thread.Sleep(200);

            string message = sr.ReadString();
            Console.WriteLine("[Server] " + message);
            Thread.Sleep(200);
            sw.WriteString(message);
            pipeWrite.Flush();

          
            
        }

    }
}
