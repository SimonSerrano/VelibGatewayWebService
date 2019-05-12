using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using VelibServiceLibrary;

namespace VelibServiceLibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8000/Velib/");
            Uri baseAddressMonitoring = new Uri("http://localhost:8000/Monitoring/");

            // Step 2: Create a ServiceHost instance.
            ServiceHost selfHost = new ServiceHost(typeof(VelibService), baseAddress);
            ServiceHost selfHost2 = new ServiceHost(typeof(MonitoringService), baseAddressMonitoring);
          

            try
            {
                // Step 3: Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(IVelibService), new WSHttpBinding(), "VelibService");

                selfHost.AddServiceEndpoint(typeof(IVelibService), new BasicHttpBinding(), "");

                selfHost2.AddServiceEndpoint(typeof(IMonitoringService), new WSHttpBinding(), "MonitoringService");
                selfHost2.AddServiceEndpoint(typeof(IMonitoringService), new BasicHttpBinding(), "");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost2.Description.Behaviors.Add(smb);

                // Step 5: Start the service.
                selfHost.Open();
                selfHost2.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
                selfHost2.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
