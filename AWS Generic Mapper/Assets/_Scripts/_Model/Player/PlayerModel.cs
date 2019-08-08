using System.Collections.Generic;

namespace DynamoDBMapper.Model
{
    public class DemoSimplePlayer: Model
    {
        public string id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public bool registered { get; set; }
        public string email { get; set; }
    }

    public class DemoComplexPlayer : DemoSimplePlayer
    {
        public List<AchievementModel> achievements;

        public int conins { get; set; }
    }
}
