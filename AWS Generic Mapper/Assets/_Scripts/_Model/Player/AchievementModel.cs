using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamoDBMapper.Model
{
    public class AchievementModel : Model
    {
        public string name { get; set; }

        public float completion { get; set; }
    }
}
