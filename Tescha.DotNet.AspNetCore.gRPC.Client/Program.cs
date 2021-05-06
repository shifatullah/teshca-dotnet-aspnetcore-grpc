using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Teshca.DotNet.AspNetCore.gRPC.Client;

namespace Tescha.DotNet.AspNetCore.gRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:50050");

            // Simple Hello Grpc Service
            var greeterClient = new Greeter.GreeterClient(channel);

            var reply = await greeterClient.SayHelloAsync(
                new HelloRequest { Name = "Greeter Client" });

            Console.WriteLine($"Reply from gRPC GreeterService {reply.Message}");

            // Server Stream
            var serverStreamingClient = new ServerStreaming.ServerStreamingClient(channel);

            using var streamingCall = serverStreamingClient.StreamingFromServer(new Empty());

            var cts = new CancellationTokenSource();
            while (streamingCall.ResponseStream.MoveNext(cts.Token).Result)
            {
                Console.WriteLine(streamingCall.ResponseStream.Current.Message);
            }
            Console.WriteLine($"Reply from gRPC ServerStreamService completed.");
        }
    }
}
