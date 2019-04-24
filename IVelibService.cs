using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VelibGatewayWebService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IVelibService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IVelibService
    {

        [OperationContract]
        IList<Station> GetVelibStationsInCity(string city);

        [OperationContract]
        int GetVelibsAvailableForStation(string city, string station);


      

        // TODO: ajoutez vos opérations de service ici
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class Station
    {
        string name = "station";
        int available_bikes = 0;

        

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int AvailableBikes
        {
            get { return available_bikes; }
            set { available_bikes = value; }
        }
    }
}
