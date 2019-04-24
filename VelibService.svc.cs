using System.Collections.Generic;
using VelibGatewayWebService.requests;

namespace VelibGatewayWebService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "VelibService" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez VelibService.svc ou VelibService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class VelibService : IVelibService
    {
        

       

        public int GetVelibsAvailableForStation(string city, string station)
        {
            VelibRequest request = new VelibRequest();
            return request.getAvalaibleBikes(city, station);
        }

        public IList<Station> GetVelibStationsInCity(string city)
        {
            VelibRequest request = new VelibRequest();
            return request.getStationsForCity(city);
        }
    }
}
