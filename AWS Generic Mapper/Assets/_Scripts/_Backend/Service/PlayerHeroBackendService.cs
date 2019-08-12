using UnityEngine;
using DynamoDBMapper.Backend.Client;

namespace DynamoDBMapper.Backend.Service
{
    public class PlayerHeroBackendService : IPlayerHeroClient
    {
        private IPlayerHeroClient client;

        public PlayerHeroBackendService()
        {
            client = Camera.main.GetComponent<AWSPlayerClient>();
        }

        public void LoadSimplePlayers()
        {
            client.LoadSimplePlayers();
        }

        public void LoadComplexPlayers()
        {
            client.LoadComplexPlayers();
        }

        public void AddSimplePlayers() {
            client.AddSimplePlayers();
        }

        public void AddComplexPlayers() {
            client.AddComplexPlayers();
        }
    }

}
