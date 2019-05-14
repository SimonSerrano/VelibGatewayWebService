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
        /// <param name="userIdHashed">user id hashed to check if he has the right to access data</param>
        /// <returns>the mean request time</returns>
        [OperationContract]
        int MeanRequestTime(string userIdHashed);

        /// <summary>
        /// Request the total number of requests made on the server
        /// </summary>
        /// <param name="userIdHashed">user id hashed to check if he has the right to access data</param>
        /// <returns>the number of request</returns>
        [OperationContract]
        int NumberOfRequest(string userIdHashed);

        /// <summary>
        /// Request the list of evolution of request time
        /// </summary>
        /// <param name="userIdHashed">user id hashed to check if he has the right to access data</param>
        /// <returns>the list of mean request time</returns>
        [OperationContract]
        IDictionary<DateTime, int> RequestTimeEvolution(string userIdHashed);
    }
}