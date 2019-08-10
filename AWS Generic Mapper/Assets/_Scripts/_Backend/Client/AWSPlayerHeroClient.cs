using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using DynamoDBMapper.Model;
using System.Collections.Generic;

namespace DynamoDBMapper.Backend.Client
{
    public interface IPlayerHeroClient
    {
        void LoadSimplePlayers();
        void LoadComplexPlayers();
        void AddSimplePlayers();
        void AddComplexPlayers();
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
            return "DemoPlayer";
        }

        void Awake()
        {
            _client = Client;
            CreateTables();
        }
        // Start is called before the first frame update
        public void LoadSimplePlayers()
        {
            Condition condition = new Condition();
            condition.AttributeValueList = new List<AttributeValue>() { new AttributeValue("Simple") };
            condition.ComparisonOperator = ComparisonOperator.CONTAINS;
            Dictionary<string, Condition> filter = new Dictionary<string, Condition>();
            filter["Name"] = condition;

            LoadAll<DemoSimplePlayer>(filter, (mappedPlayers) => { 
                foreach (DemoSimplePlayer item in mappedPlayers)
                {
                    print("********* Loaded Simple Player " + item.Id);
                    print("Name: " + item.Name);
                    print("Age: " + item.Age);
                    print("E-Mail: " + item.EMail);
                }
            });
        }

        public void LoadComplexPlayers()
        {
            Condition condition = new Condition();
            condition.AttributeValueList = new List<AttributeValue>() { new AttributeValue("Complex")};
            condition.ComparisonOperator = ComparisonOperator.CONTAINS;
            Dictionary<string, Condition> filter = new Dictionary<string, Condition>();
            filter["Name"] = condition;

            LoadAll<DemoComplexPlayer>(null, (mappedPlayers) => {
                foreach (DemoComplexPlayer item in mappedPlayers)
                {
                    print("********* Loaded Complex Player " + item.Id);
                    print("Name: " + item.Name);
                    print("Age: " + item.Age);
                    print("E-Mail: " + item.EMail);
                    print("Coins: " + item.Conins);
                    foreach (AchievementModel achievement in item.Achievements) {
                        print(achievement.ToString());
                    }
                }
            });
        }

        private void CreateTables()
        {
            var productCatalogTableRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                            AttributeName = "Id",
                            AttributeType = "S"
                    }
                },
                // Primary key is 'Id'; Rows need to have at least this attribute
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                            AttributeName = "Id",
                            KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = TableName()
            };

            Client.CreateTableAsync(productCatalogTableRequest, (result) =>
            {
                if (result.Exception != null)
                {
                    print("**** Failed to create Table: " + result.Exception.Message);
                    return;
                }
                var tableDescription = result.Response.TableDescription;
                print(tableDescription.TableName + " created.");
            });
        }

        public void AddSimplePlayers()
        {
            var simplePlayer1 = new Dictionary<string, AttributeValue>();
            simplePlayer1["Id"] = new AttributeValue() { S = "42" }; ;
            simplePlayer1["Name"] = new AttributeValue("Simple Demo Player 1");
            simplePlayer1["EMail"] = new AttributeValue("dynamoMapper1@games.com");
            simplePlayer1["Registered"] = new AttributeValue() { BOOL = true };
            simplePlayer1["Age"] = new AttributeValue() { N = "21" }; ;

            Client.PutItemAsync(TableName(), simplePlayer1, (result) => {
                if (result.Exception != null)
                {
                    print("**** Failed to create Simpleplayer 1: " + result.Exception.Message);
                    return;
                }
                print("Created Simpleplayer 1");
            });

            var simplePlayer2 = new Dictionary<string, AttributeValue>();
            simplePlayer2["Id"] = new AttributeValue() { S = "72" }; ;
            simplePlayer2["Name"] = new AttributeValue("Simple Demo Player 2");
            simplePlayer2["EMail"] = new AttributeValue("dynamoMapper2@loreto.com");
            simplePlayer2["Registered"] = new AttributeValue() { BOOL = false };
            simplePlayer2["Age"] = new AttributeValue() { N = "36" }; ;

            Client.PutItemAsync(TableName(), simplePlayer2, (result) =>
            {
                if (result.Exception != null)
                {
                    print("**** Failed to create Simpleplayer 2: " + result.Exception.Message);
                    return;
                }
                print("Created Simpleplayer 2");
            });
        }

        public void AddComplexPlayers() {

            var complexPlayer1 = new Dictionary<string, AttributeValue>();
            complexPlayer1["Id"] = new AttributeValue("68");
            complexPlayer1["Name"] = new AttributeValue("Complex Demo Player 1");
            complexPlayer1["EMail"] = new AttributeValue("dynamoMapper3@loreto.com");
            complexPlayer1["Registered"] = new AttributeValue() { BOOL = false };
            complexPlayer1["Age"] = new AttributeValue() { N = "50" };
            complexPlayer1["Achievements"] = new AttributeValue() { L = CreateAchievements() };
            complexPlayer1["Coins"] = new AttributeValue() { N = "1001" };
            Client.PutItemAsync(TableName(), complexPlayer1, (result) =>
            {
                if (result.Exception != null)
                {
                    print("**** Failed to create Complexlayer 1: " + result.Exception.Message);
                    return;
                }
                print("Created Complexlayer 1");
            });

            var complexPlayer2 = new Dictionary<string, AttributeValue>();
            complexPlayer2["Id"] = new AttributeValue("100");
            complexPlayer2["Name"] = new AttributeValue("Complex Demo Player 2");
            complexPlayer2["EMail"] = new AttributeValue("dynamoMapper4@loreto.com");
            complexPlayer2["Registered"] = new AttributeValue() { BOOL = false };
            complexPlayer2["Age"] = new AttributeValue() { N = "50" };
            complexPlayer2["Achievements"] = new AttributeValue() { L = CreateAchievements() };
            complexPlayer2["Coins"] = new AttributeValue() { N = "1337" };
            Client.PutItemAsync(TableName(), complexPlayer2, (result) =>
            {
                if (result.Exception != null)
                {
                    print("**** Failed to create Complexlayer 2: " + result.Exception.Message);
                    return;
                }
                print("Created Complexlayer 2");
            });
        }

        private List<AttributeValue> CreateAchievements()
        {
            Dictionary<string, AttributeValue> achieve1 = new Dictionary<string, AttributeValue>();
            achieve1["Name"] = new AttributeValue("Mapper");
            achieve1["Completion"] = new AttributeValue() { N = "0.8"};
            Dictionary<string, AttributeValue> achieve2 = new Dictionary<string, AttributeValue>();
            achieve2["Name"] = new AttributeValue("Dynamo");
            achieve2["Completion"] = new AttributeValue() { N = "0.2" };

            List<AttributeValue> achievements = new List<AttributeValue>() { 
                new AttributeValue() { M = achieve1 },
                new AttributeValue() { M = achieve2 }
            };
            return achievements;
        }
    }
}
