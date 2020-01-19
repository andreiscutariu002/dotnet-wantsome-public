using System;

namespace SoapServiceConsumer
{
    using System.Threading.Tasks;
    using WcfService;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceClient = new ServiceClient();

            RunAsync(serviceClient).Wait();
        }

        private static async Task RunAsync(ServiceClient serviceClient)
        {
            var r = await serviceClient.GetDataAsync(20);

            Console.WriteLine(r);

            var r2 = await serviceClient.GetDataUsingDataContractAsync(new CompositeType
            {
                BoolValue = true,
                StringValue = "some"
            });

            Console.WriteLine(r2.StringValue);
        }
    }
}
