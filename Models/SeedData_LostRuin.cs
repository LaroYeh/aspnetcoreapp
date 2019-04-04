using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LostRuin.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LostRuinContext(serviceProvider.GetRequiredService<DbContextOptions<LostRuinContext>>()))
            {
                // Look for any Map.
                if (context.Map.Any())
                {
                    return;   // DB has been seeded
                }

                //2層，實際尺寸可不同
                //使用6面骰就好
                //

                #region Add Context
                context.Map.AddRange(
                    new Map { ID = 1, Name = "遺跡_水上", Desc = "遺跡漏出水面的部分", StartDate = new DateTime(2019, 3, 1),  EndDate = new DateTime(2019, 5, 1) },
                    new Map { ID = 2, Name = "遺跡_水下", Desc = "遺跡沉在水中的部分", StartDate = new DateTime(2019, 4, 1),  EndDate = new DateTime(2019, 5, 1), Effect = "Breath:-1"}
                );
                context.Block.AddRange(
                    new Block { ID = 1, MapId = 1, X = 0, Y = 0 },
                    new Block { ID = 2, MapId = 1, X = 1, Y = 0 },
                    new Block { ID = 3, MapId = 1, X = 0, Y = 1 },
                    new Block { ID = 4, MapId = 1, X = 1, Y = 1 },
                    new Block { ID = 5, MapId = 2, X = 0, Y = 1 },
                    new Block { ID = 6, MapId = 2, X = 1, Y = 1 }
                );

                #region 定義
                context.Environment.AddRange(
                    new Environment { ID = 1, Name = "營地" }, //Start
                    new Environment { ID = 2, Name = "洋流" },
                    new Environment { ID = 3, Name = "海床" },
                    new Environment { ID = 4, Name = "沈船" } //End
                );
                context.Tag.AddRange(
                    new Tag { ID = 1, Name = "往右方", Desc ="可向右移動", 
                        Svg = "<polygon points='6,30 24,30 24,40 43,25 24,10 24,20 6,20' style='fill:#000;'", Effect = "CanRight:1"},
                    new Tag { ID = 2, Name = "往下方", Desc ="可向下移動", 
                        Svg = "<polygon points='6,30 24,30 24,40 43,25 24,10 24,20 6,20' style='fill:#000;' transform='rotate(90 25,25)'/>", Effect = "CanDown:1"},
                    new Tag { ID = 3, Name = "往左方", Desc ="可向左移動", 
                        Svg = "<polygon points='6,30 24,30 24,40 43,25 24,10 24,20 6,20' style='fill:#000;' transform='rotate(180 25,25)'/>", Effect = "CanLeft:1"},
                    new Tag { ID = 4, Name = "往上方", Desc ="可向上移動", 
                        Svg = "<polygon points='6,30 24,30 24,40 43,25 24,10 24,20 6,20' style='fill:#000;' transform='rotate(270 25,25)'/>", Effect = "CanUp:1"},
                    new Tag { ID = 5, Name = "垂直移動", Desc ="可上浮或下沉", 
                        Svg = "<polygon points='13,26 9,26 16,6 24,26 20,26 20,43 13,43' style='fill:#000;' transform='rotate(0 25,25)'/><polygon points='13,26 9,26 16,6 24,26 20,26 20,43 13,43' style='fill:#000;' transform='rotate(180 25,25)'/>", Effect = "CanFloat:1,CanDive:1"}
                );
                context.Trigger.AddRange(
                    new Trigger { ID = 1, Name = "無" },
                    new Trigger { ID = 2, Name = "事件" },
                    new Trigger { ID = 3, Name = "遇敵" }
                );
                context.Event.AddRange(
                    new Event { ID = 1, TriggerId = 2, Level = 1, Name = "1★事件", Desc = "", Effect = ""},
                    new Event { ID = 2, TriggerId = 2, Level = 2, Name = "2★事件", Desc = "", Effect = ""},
                    new Event { ID = 3, TriggerId = 3, Level = 1, Name = "1★怪物", Desc = "", Effect = ""},
                    new Event { ID = 4, TriggerId = 3, Level = 2, Name = "2★怪物", Desc = "", Effect = ""}
                );
                #endregion

                #region 關聯資料
                context.Block_Env.AddRange(
                    new Block_Env { ID = 1, BlockId = 1, EnvId = 1 },
                    new Block_Env { ID = 2, BlockId = 2, EnvId = 2 },
                    new Block_Env { ID = 3, BlockId = 3, EnvId = 2 },
                    new Block_Env { ID = 4, BlockId = 4, EnvId = 2 },
                    new Block_Env { ID = 5, BlockId = 5, EnvId = 3 },
                    new Block_Env { ID = 6, BlockId = 6, EnvId = 4 }                    
                );
                context.Block_Tag.AddRange(
                    //Map1
                    new Block_Tag { ID = 1, BlockId = 1, TagId = 1},
                    new Block_Tag { ID = 2, BlockId = 1, TagId = 4},
                    new Block_Tag { ID = 3, BlockId = 2, TagId = 3},
                    new Block_Tag { ID = 4, BlockId = 2, TagId = 2},
                    new Block_Tag { ID = 5, BlockId = 3, TagId = 1},
                    new Block_Tag { ID = 6, BlockId = 3, TagId = 4},
                    new Block_Tag { ID = 7, BlockId = 3, TagId = 5},
                    new Block_Tag { ID = 8, BlockId = 4, TagId = 1},
                    new Block_Tag { ID = 9, BlockId = 4, TagId = 3},
                    new Block_Tag { ID = 10, BlockId = 4, TagId = 5},
                     //Map2
                    new Block_Tag { ID = 11, BlockId = 5, TagId = 1},
                    new Block_Tag { ID = 12, BlockId = 5, TagId = 5},
                    new Block_Tag { ID = 13, BlockId = 6, TagId = 3},
                    new Block_Tag { ID = 14, BlockId = 6, TagId = 5}
                );
                //context.Environment_Trigger.AddRange();
                //context.Trigger_Event.AddRange();
                #endregion

                #endregion

                // //寫入DB
                context.SaveChanges();
            }

        }

    }

}