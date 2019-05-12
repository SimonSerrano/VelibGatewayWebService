using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibServiceLibrary
{
    [ServiceContract]
    public interface IMonitoringService
    {

        /// <summary>
        /// Request the mean request duration for every request
        /// </summary>
        /// <returns>the mean request time</returns>
        [OperationContract]
        int MeanRequestTime();

        /// <summary>
        /// Request the total number of requests made on the server
        /// </summary>
        /// <returns>the number of request</returns>
        [OperationContract]
        int NumberOfRequest();
    }
}
