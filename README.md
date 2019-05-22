# SI4 : Service oriented computing - Web Services 

## Auteurs
[Simon Serrano](simon.serrano@hotmail.fr)
[Lucas Oms](lucas.oms@gmail.com)

## Extensions
 - Interface utilisateur client sous Angular
 - Remplacer les appels à l'API par des appels asynchrones
 - Ajout d'un cache entre WS et JCDecaux afin d'augmenter la rapidité des appels sur le WS
 - Fonctionnalité de monitoring qui permet d'avoir des mesures sur les performances du serveur. Accesiible uniquement via une compte "administrateur"
 - Sécurité simple en utilisant un binding wsHttp pour activer la couche transport i.e https. Vérification du hash du compte "administrateur" afin d'avoir accès au monitoring

## Ordre de lancement
Pour que tous les projets fonctionnent correctement il est nécessaire de les lancer dans un certain ordre :  
 1. VelibServiceLibraryHost
 2. Le client Angular que l'on peut trouver à ce lien : https://github.com/LucasOMS/ProjetWebService-GUI
 3. Vous pouvez lancer la fonctionnalité à tester   

Voir "Architecture du projet".   
**(?)** Si on choisit d'importer tous les projets dans une seule solution (configuration actuelle) alors l'ordre des dépendances est normalement déjà implémenté, et lors d'un lancement la totalité des projets sera lancée.

## Architecture du projet
Le dépôt courant est composé de 3 projets (de manière à bien découper en cas de client/serveur) :  
 1. **ClientConsoleVelibGateway :** Projet client console qui communique via un protocol SOAP avec le service fournit par le projet `Wcf_SOAP-Velib`. Tapez `help` pour voir les commandes disponibles. Vous pourrez intéragir via la console Windows.    
 → Ce projet utilise les méthodes synchrones (séquentielles classiques) fournies par le service SOAP.  
 _L'API publique est documentée._

 2. **VelibServiceLibrary** : Projet WCF (bibliothèque) qui fournit un service SOAP communiquant avec l'API de JC Decaux qui suit l'architecture REST. 
 _L'API publique est documentée._  

 3. **VelibServiceLibraryHost** : Projet console qui permet d'être hôte de la bibliothèque WCF. Il suffit de lancer et le service fonctionne.
