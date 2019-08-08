using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using DynamoDBMapper.Model;

namespace DynamoDBMapper.Backend.Client {
    public interface IPlayerHeroClient
    {
        void RequestSimplePlayers();
        void RequestComplexPlayers();
    }

    public class AWSPlayerClient : AWSBaseClient, IPlayerHeroClient
    {
        private IAmazonDynamoDB _client;
        private DynamoDBContext _context;

        private DynamoDBContext Context
        {
            get
            {
                if (_context == null)
                    _context = new DynamoDBContext(_client);

                return _context;
            }
        }

        public override string TableName()
        {
            return "Player";
        }

        void Awake()
        {
            _client = Client;
        }
        // Start is called before the first frame update
        public void RequestSimplePlayers()
        {
            LoadAll<DemoSimplePlayer>(null, (mappedPlayers) => {});
        }

        public void RequestComplexPlayers()
        {
            LoadAll<DemoComplexPlayer>(null, (mappedPlayers) => { });
        }
    }
}
