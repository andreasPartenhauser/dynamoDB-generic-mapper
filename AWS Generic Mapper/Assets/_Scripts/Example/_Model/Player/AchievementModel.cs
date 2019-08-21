using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamoDBMapper.Model
{
    public class AchievementModel : Model
    {
        public string Name { get; set; }

        public float Completion { get; set; }

        public override string ToString() {
            return "Achievement: " + Name + " - completed: " + Completion;
        }
    }
}
