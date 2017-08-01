using Tripeace.Domain.Enums;
using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class Player
    {
        public Player()
        {
            AccountBanHistory = new HashSet<AccountBanHistory>();
            AccountBans = new HashSet<AccountBan>();
            AccountViplist = new HashSet<AccountViplist>();
            GuildInvites = new HashSet<GuildInvite>();
            IpBans = new HashSet<IpBan>();
            MarketHistory = new HashSet<MarketHistory>();
            MarketOffers = new HashSet<MarketOffer>();
            PlayerDeaths = new HashSet<PlayerDeath>();
            PlayerDepotitems = new HashSet<PlayerDepotItem>();
            PlayerInboxitems = new HashSet<PlayerInboxItem>();
            PlayerItems = new HashSet<PlayerItem>();
            PlayerNamelocksNamelockedByNavigation = new HashSet<PlayerNamelock>();
            PlayerSpells = new HashSet<PlayerSpell>();
            PlayerStorage = new HashSet<PlayerStorage>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public long Balance { get; set; }
        public int Blessings { get; set; }
        public int Cap { get; set; }
        public byte[] Conditions { get; set; }
        public long _deletion { get; set; }
        public long Experience { get; set; }
        public PlayerGroup GroupId { get; set; }
        public int Health { get; set; }
        public int Healthmax { get; set; }
        public int Lastip { get; set; }
        public long _lastlogin { get; set; }
        public long _lastlogout { get; set; }
        public int Level { get; set; }
        public int Lookaddons { get; set; }
        public int Lookbody { get; set; }
        public int Lookfeet { get; set; }
        public int Lookhead { get; set; }
        public int Looklegs { get; set; }
        public int Looktype { get; set; }
        public int Maglevel { get; set; }
        public int Mana { get; set; }
        public int Manamax { get; set; }
        public int Manaspent { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int OfflinetrainingSkill { get; set; }
        public int OfflinetrainingTime { get; set; }
        public int Onlinetime { get; set; }
        public int Posx { get; set; }
        public int Posy { get; set; }
        public int Posz { get; set; }
        public bool Save { get; set; }
        public Sex Sex { get; set; }
        public int SkillAxe { get; set; }
        public long SkillAxeTries { get; set; }
        public int SkillClub { get; set; }
        public long SkillClubTries { get; set; }
        public int SkillDist { get; set; }
        public long SkillDistTries { get; set; }
        public int SkillFishing { get; set; }
        public long SkillFishingTries { get; set; }
        public int SkillFist { get; set; }
        public long SkillFistTries { get; set; }
        public int SkillShielding { get; set; }
        public long SkillShieldingTries { get; set; }
        public int SkillSword { get; set; }
        public long SkillSwordTries { get; set; }
        public bool Skull { get; set; }
        public int Skulltime { get; set; }
        public int Soul { get; set; }
        public int Stamina { get; set; }
        public int TownId { get; set; }
        public Vocation Vocation { get; set; }

        public virtual ICollection<AccountBanHistory> AccountBanHistory { get; set; }
        public virtual ICollection<AccountBan> AccountBans { get; set; }
        public virtual ICollection<AccountViplist> AccountViplist { get; set; }
        public virtual ICollection<GuildInvite> GuildInvites { get; set; }
        public virtual GuildMembership GuildMembership { get; set; }
        public virtual Guild Guilds { get; set; }
        public virtual ICollection<IpBan> IpBans { get; set; }
        public virtual ICollection<MarketHistory> MarketHistory { get; set; }
        public virtual ICollection<MarketOffer> MarketOffers { get; set; }
        public virtual ICollection<PlayerDeath> PlayerDeaths { get; set; }
        public virtual ICollection<PlayerDepotItem> PlayerDepotitems { get; set; }
        public virtual ICollection<PlayerInboxItem> PlayerInboxitems { get; set; }
        public virtual ICollection<PlayerItem> PlayerItems { get; set; }
        public virtual ICollection<PlayerNamelock> PlayerNamelocksNamelockedByNavigation { get; set; }
        public virtual PlayerNamelock PlayerNamelocksPlayer { get; set; }
        public virtual ICollection<PlayerSpell> PlayerSpells { get; set; }
        public virtual ICollection<PlayerStorage> PlayerStorage { get; set; }
        public virtual Account Account { get; set; }
        
        public DateTime Deletion
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_deletion);
            }

            set
            {
                _deletion = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
        public DateTime LastLogin
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_lastlogin);
            }

            set
            {
                _lastlogin = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
        public DateTime LastLogout
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_lastlogout);
            }

            set
            {
                _lastlogout = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
    }
}
