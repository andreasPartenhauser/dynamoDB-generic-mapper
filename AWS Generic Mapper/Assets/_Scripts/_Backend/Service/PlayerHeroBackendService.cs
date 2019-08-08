using UnityEngine;
using DynamoDBMapper.Backend.Client;

namespace DynamoDBMapper.Backend.Service
{
    public class PlayerHeroBackendService : IPlayerHeroClient
    {
        private IPlayerHeroClient client;

        public PlayerHeroBackendService()
        {
            client = Camera.main.GetComponent<Loader>().GetComponent<AWSPlayerClient>();
        }

        public void RequestSimplePlayers()
        {
            client.RequestSimplePlayers();
        }

        public void RequestComplexPlayers()
        {
            client.RequestComplexPlayers();
        }
    }

}
