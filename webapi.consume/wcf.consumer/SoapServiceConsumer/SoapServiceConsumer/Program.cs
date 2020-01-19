namespace SoapServiceConsumer
{
    using System;
    using System.Threading.Tasks;
    using WcfService;

    internal class Program
    {
        private static void Main(string[] args)
        {
            CallServiceUsingGeneratedCode();
        }

        private static void CallServiceUsingGeneratedCode()
        {
            var serviceClient = new ServiceClient();

            RunAsync(serviceClient).Wait();

            serviceClient.CloseAsync().Wait();
        }

        private static async Task RunAsync(IService serviceClient)
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
