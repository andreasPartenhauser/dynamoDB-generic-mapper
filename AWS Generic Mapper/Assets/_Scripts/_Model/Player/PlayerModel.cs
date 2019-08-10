using System.Collections.Generic;

namespace DynamoDBMapper.Model
{
    public class DemoSimplePlayer: Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Registered { get; set; }
        public string EMail { get; set; }
    }

    public class DemoComplexPlayer : Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Registered { get; set; }
        public string EMail { get; set; }
        public List<AchievementModel> Achievements { get; set; }

        public int Conins { get; set; }
    }
}
