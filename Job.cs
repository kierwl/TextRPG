using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
 
    
        enum JobType { Warrior, Mage, Archer,Sparta, 무직백수 }

        class Job
        {
            public JobType JobType { get; }
            public string Name { get; }
            public int HealthBonus { get; }
            public int AttackBonus { get; }
            public int DefenseBonus { get; }

            public Job(JobType jobType)
            {
                JobType = jobType;

                switch (jobType)
                {
                    case JobType.Warrior:
                        Name = "전사";
                        HealthBonus = 20;
                        AttackBonus = 5;
                        DefenseBonus = 3;
                    break;
                    case JobType.Mage:
                        Name = "마법사";
                        HealthBonus = 10;
                        AttackBonus = 10;
                        DefenseBonus = 1;
                        break;
                    case JobType.Archer:
                        Name = "궁수";
                        HealthBonus = 15;
                        AttackBonus = 7;
                        DefenseBonus = 2;
                        break;

                    case JobType.Sparta:
                        Name = "스파르타";
                        HealthBonus = 20;
                        AttackBonus = 55;
                        DefenseBonus = -100;
                        break;
                default:
                        Name = "무직백수";
                        HealthBonus = 0;
                        AttackBonus = 0;
                        DefenseBonus = 0;
                        break;
                }
            }
        }
    }


