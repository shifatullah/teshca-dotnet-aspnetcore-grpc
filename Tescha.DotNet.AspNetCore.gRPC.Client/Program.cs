using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Teshca.DotNet.AspNetCore.gRPC.Client;

namespace Tescha.DotNet.AspNetCore.gRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
           

            using var channel = GrpcChannel.ForAddress("https://localhost:50050");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "Greeter Client" } );

            Console.WriteLine($"Reply from gRPC GreeterService {reply.Message}");
        }
    }
}
