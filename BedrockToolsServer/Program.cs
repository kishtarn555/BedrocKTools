using BedrockToolsServer;
Console.WriteLine("===============");
Console.WriteLine("Bedrock tools");
Console.WriteLine("===============");
Console.Out.Flush();
if (args.Length < 1) {
    Console.WriteLine("Start mode not specified in arguments");
    return;
}

string executionMode = args[0];

if (executionMode == "server") {

    if (args.Length < 3) {
        Console.WriteLine("Not enough arguments");
        return;
    }
    string inputHandle = args[1];
    string outputHandle = args[2];
    
    StructureServer server = new StructureServer(inputHandle, outputHandle);
    
    server.Start();
    return;
}
