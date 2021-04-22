using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Teshca.DotNet.AspNetCore.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to succ
        //
        // essfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //https://www.fabiocozzolino.eu/HTTP2-over-TLS-is-not-supported-on-macOS/
                    //https://github.com/grpc/grpc-dotnet/issues/416
                    // Uncomment below configuration to disblae TLS on macOS.
                    // Use it only for development environment.
                    // Make sure to keep it commented on production environment.
                    //.ConfigureKestrel(options =>
                    //{
                    //    options.Limits.MinRequestBodyDataRate = null;

                    //    options.ListenAnyIP(50050,
                    //          listenOptions => {
                    //              listenOptions.Protocols = HttpProtocols.Http1;
                    //              listenOptions.UseHttps();
                    //          });

                    //    options.ListenAnyIP(50051,
                    //          listenOptions => {
                    //              listenOptions.Protocols = HttpProtocols.Http2;
                    //          });
                    //});
                });
    }
}
