using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LostRuin.Models
{
    public class LostRuinContext : DbContext
    {
        #region DbSet
        public DbSet<LostRuin.Models.Map> Map { get; set; }
        public DbSet<LostRuin.Models.Block> Block { get; set; }
        public DbSet<LostRuin.Models.Environment> Environment { get; set; }
        public DbSet<LostRuin.Models.Tag> Tag { get; set; }
        public DbSet<LostRuin.Models.Trigger> Trigger { get; set; }
        public DbSet<LostRuin.Models.Event> Event { get; set; }
        public DbSet<LostRuin.Models.Block_Env> Block_Env { get; set; }
        public DbSet<LostRuin.Models.Block_Tag> Block_Tag { get; set; }
        public DbSet<LostRuin.Models.Environment_Trigger> Environment_Trigger { get; set; }
        public DbSet<LostRuin.Models.Trigger_Event> Trigger_Event { get; set; }
        public DbSet<LostRuin.Models.Code> Code { get; set; }
        #endregion

        public LostRuinContext(DbContextOptions<LostRuinContext> option) : base(option) 
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Map>().HasKey(c => c.ID);
            modelBuilder.Entity<Block>().HasKey(c => c.ID);

            modelBuilder.Entity<Environment>().HasKey(c => c.ID);
            modelBuilder.Entity<Tag>().HasKey(c => c.ID);
            modelBuilder.Entity<Trigger>().HasKey(c => c.ID);
            modelBuilder.Entity<Event>().HasKey(c => c.ID);

            modelBuilder.Entity<Code>().HasKey(c => c.ID);

            modelBuilder.Entity<Block_Env>().HasKey(c => c.ID);
            modelBuilder.Entity<Block_Tag>().HasKey(c => c.ID);
            modelBuilder.Entity<Environment_Trigger>().HasKey(c => c.ID);
            modelBuilder.Entity<Trigger_Event>().HasKey(c => c.ID);
        }
    }

    public class Map //地圖
    {
        public int ID { get; set; }

        [Required][DataType(DataType.Text)][MaxLength(50)]
        public string Name_twn { get; set; } 
        [DataType(DataType.Text)][MaxLength(200)]
        public string Desc_twn { get; set; }
        public string Effect { get; set; } //地圖影響
        [Required][DataType(DataType.DateTime)] //YYYY-mm-DD HH:MM:SS
        public DateTime StartDate { get; set; } //開放時間
        [Required][DataType(DataType.DateTime)] //YYYY-mm-DD HH:MM:SS Default:9999-12-30 23:59:59
        public DateTime EndDate { get; set; } //結束時間
        // [Column("最後更新使用者")][DataType(DataType.Text)][MaxLength(50)]
        // public string LastModifyUser { get; set; }
        // [Column("最後更新日期")][DataType(DataType.DateTime)]
        // public DateTime LastModifyDate { get; set; } //YYYY-mm-DD HH:MM:SS
    }
    public class Block //方塊
    {
        public int ID { get; set; }
        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    #region 定義
    public class Environment //環境
    {
        public int ID { get; set; }
        [Required]
        public string Name_twn { get; set; }
        public string Desc_twn { get; set; }
        public string Img { get; set; }
        public string Effect { get; set; } // [Stauts1]:[Value1], [Stauts2]:[Value2] ex: Breath:-1
    }
    public class Tag //特性、標籤
    {
        public int ID { get; set; }
        [Required]
        public string Name_twn { get; set; }        
        public string Desc_twn { get; set; }  
        public string Img { get; set; }
        public string Effect { get; set; } // [Stauts1]:[Value1], [Stauts2]:[Value2] ex: Breath:-1
    }
    public class Trigger //觸發器
    {
        public int ID { get; set; }
        public string Name_twn { get; set; }
        public string Effect { get; set; } // [Stauts1]:[Value1], [Stauts2]:[Value2] ex: Breath:-1
    }
    public class Event //事件
    {
        // Event_LostRuin_Event	Id	int	Unchecked	Y		事件id			INTEGER
        // Event_LostRuin_Event	DescId	int	Unchecked		Y	說明id(事件類型)	FK - DescIndex		INTEGER
        // Event_LostRuin_Event	GM_Dice	int	Unchecked		Y	對應的暗骰	1~100		INTEGER
        // Event_LostRuin_Event	Result	varchar(255)	Unchecked			事件的結果			TEXT
        // Event_LostRuin_Event	Comment	varchar(255)	Checked			備註			TEXT
        // Event_LostRuin_Event	LastModifyUser	varchar(20)	Unchecked			最後更新的使用者			TEXT
        // Event_LostRuin_Event	LastModifyDate	Datetime	Unchecked			最後更新的日期			TEXT as ("YYYY-MM-DD HH:MM:SS.SSS").
        public int ID { get; set; }
        public int TriggerId { get; set; }
        public string Name_twn { get; set; }
        public string Desc_twn { get; set; }

    }
    #endregion

    public class Code
    {
        public int ID { get; set; }
        public string Name_twn { get; set; }        
        public string Desc_twn { get; set; } 
        public string Comment { get; set; }
    }

    #region 關聯資料
    public class Block_Env //方塊對應的環境
    {
        public int ID { get; set; }
        public int BlockId { get; set; }
        public int EnvId { get; set; }
    }
    public class Block_Tag //方塊對應的標籤
    {
        public int ID { get; set; }
        public int BlockId { get; set; }
        public int TagId { get; set; }
    }
    public class Environment_Trigger //環境會觸發的情境對應骰表
    {
        public int ID { get; set; }
        [Required]
        public int EnvId { get; set; }
        [Required]
        public int Dice { get; set; } //PL_Dice 玩家骰子
        public int TriggerId { get; set; }
    }
    public class Trigger_Event //觸發的事件內容的對應骰表
    {
        public int ID { get; set; }
        [Required]
        public int TriggerId { get; set; }
        [Required]
        public int Dice { get; set; } //GM_Dice 暗骰
        public int EventiD { get; set; }
    }
    #endregion

}