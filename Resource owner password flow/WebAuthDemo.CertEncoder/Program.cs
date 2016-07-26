using System;
using System.IO;

namespace WebAuthDemo.CertEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            // Steps to generate a self signed certificate:
            // https://github.com/fekberg/examples/tree/master/Creating%20a%20Self%20Signed%20Certificate
            var certificate = Convert.ToBase64String(File.ReadAllBytes(@"D:\DemoAuthCert.pfx"));

            Console.WriteLine(certificate);
            Console.ReadKey();
        }
    }
}