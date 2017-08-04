using Microsoft.EntityFrameworkCore;
using Tripeace.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace Tripeace.EF
{
    public partial class ServerContext : IdentityDbContext<AccountIdentity, IdentityRole, string>
    {
        public virtual DbSet<AccountBanHistory> AccountBanHistory { get; set; }
        public virtual DbSet<AccountBan> AccountBans { get; set; }
        public virtual DbSet<AccountIdentity> AccountIdentity { get; set; }
        public virtual DbSet<AccountViplist> AccountViplist { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<GuildInvite> GuildInvites { get; set; }
        public virtual DbSet<GuildMembership> GuildMembership { get; set; }
        public virtual DbSet<GuildRank> GuildRanks { get; set; }
        public virtual DbSet<GuildWar> GuildWars { get; set; }
        public virtual DbSet<Guild> Guilds { get; set; }
        public virtual DbSet<GuildWarKill> GuildwarKills { get; set; }
        public virtual DbSet<HouseList> HouseLists { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<IpBan> IpBans { get; set; }
        public virtual DbSet<MarketHistory> MarketHistory { get; set; }
        public virtual DbSet<MarketOffer> MarketOffers { get; set; }
        public virtual DbSet<PlayerDeath> PlayerDeaths { get; set; }
        public virtual DbSet<PlayerDepotItem> PlayerDepotitems { get; set; }
        public virtual DbSet<PlayerInboxItem> PlayerInboxitems { get; set; }
        public virtual DbSet<PlayerItem> PlayerItems { get; set; }
        public virtual DbSet<PlayerNamelock> PlayerNamelocks { get; set; }
        public virtual DbSet<PlayerSpell> PlayerSpells { get; set; }
        public virtual DbSet<PlayerStorage> PlayerStorage { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayersOnline> PlayersOnline { get; set; }
        public virtual DbSet<ServerConfig> ServerConfig { get; set; }
        public virtual DbSet<TileStore> TileStore { get; set; }

        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
        }

        public ServerContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // uncomment the bellow line to run migrations. Also update the connection string if need.
            optionsBuilder.UseMySql("Server=localhost;DataBase=TibiaOtNew;Uid=root;Pwd=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountBanHistory>(entity =>
            {
                entity.ToTable("account_ban_history");

                entity.HasIndex(e => e.AccountId)
                    .HasName("account_id");

                entity.HasIndex(e => e.BannedById)
                    .HasName("banned_by");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._bannedAt)
                    .HasColumnName("banned_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BannedById)
                    .HasColumnName("banned_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e._expiredAt)
                    .HasColumnName("expired_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountBanHistory)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("account_ban_history_ibfk_1");

                entity.HasOne(d => d.BannedBy)
                    .WithMany(p => p.AccountBanHistory)
                    .HasForeignKey(d => d.BannedById)
                    .HasConstraintName("account_ban_history_ibfk_2");

                entity.Ignore(i => i.BannedAt);

                entity.Ignore(i => i.ExpiredAt);
            });

            modelBuilder.Entity<AccountBan>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK_account_bans");

                entity.ToTable("account_bans");

                entity.HasIndex(e => e.BannedById)
                    .HasName("banned_by");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._bannedAt)
                    .HasColumnName("banned_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BannedById)
                    .HasColumnName("banned_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e._expiresAt)
                    .HasColumnName("expires_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.AccountBan)
                    .HasForeignKey<AccountBan>(d => d.AccountId)
                    .HasConstraintName("account_bans_ibfk_1");

                entity.HasOne(d => d.BannedBy)
                    .WithMany(p => p.AccountBans)
                    .HasForeignKey(d => d.BannedById)
                    .HasConstraintName("account_bans_ibfk_2");

                entity.Ignore(i => i.BannedAt);

                entity.Ignore(i => i.ExpiresAt);
            });

            modelBuilder.Entity<AccountViplist>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.PlayerId })
                    .HasName("account_player_index");

                entity.ToTable("account_viplist");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasColumnType("tinyint(2) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Notify)
                    .HasColumnName("notify")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountViplist)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("account_viplist_ibfk_1");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.AccountViplist)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("account_viplist_ibfk_2");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.Name)
                    .HasName("name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._creation)
                    .HasColumnName("creation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e._lastday)
                    .HasColumnName("lastday")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("char(40)");

                entity.Property(e => e.Premdays)
                    .HasColumnName("premdays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Secret)
                    .HasColumnName("secret")
                    .HasColumnType("char(16)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Ignore(i => i.Creation);

                entity.Ignore(i => i.LastDay);
            });

            modelBuilder.Entity<AccountIdentity>(entity =>
            {
                entity.HasOne(x => x.Account)
                    .WithOne(x => x.AccountIdentity)
                    .HasForeignKey<AccountIdentity>(x => x.AccountIdentityId);
            });

                modelBuilder.Entity<GuildInvite>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.GuildId })
                    .HasName("PK_guild_invites");

                entity.ToTable("guild_invites");

                entity.HasIndex(e => e.GuildId)
                    .HasName("guild_id");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.GuildId)
                    .HasColumnName("guild_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Guild)
                    .WithMany(p => p.GuildInvites)
                    .HasForeignKey(d => d.GuildId)
                    .HasConstraintName("guild_invites_ibfk_2");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.GuildInvites)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("guild_invites_ibfk_1");
            });

            modelBuilder.Entity<GuildMembership>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK_guild_membership");

                entity.ToTable("guild_membership");

                entity.HasIndex(e => e.GuildId)
                    .HasName("guild_id");

                entity.HasIndex(e => e.RankId)
                    .HasName("rank_id");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GuildId)
                    .HasColumnName("guild_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasColumnName("nick")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.RankId)
                    .HasColumnName("rank_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Guild)
                    .WithMany(p => p.GuildMembership)
                    .HasForeignKey(d => d.GuildId)
                    .HasConstraintName("guild_membership_ibfk_2");

                entity.HasOne(d => d.Player)
                    .WithOne(p => p.GuildMembership)
                    .HasForeignKey<GuildMembership>(d => d.PlayerId)
                    .HasConstraintName("guild_membership_ibfk_1");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.GuildMembership)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("guild_membership_ibfk_3");
            });

            modelBuilder.Entity<GuildRank>(entity =>
            {
                entity.ToTable("guild_ranks");

                entity.HasIndex(e => e.GuildId)
                    .HasName("guild_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GuildId)
                    .HasColumnName("guild_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Guild)
                    .WithMany(p => p.GuildRanks)
                    .HasForeignKey(d => d.GuildId)
                    .HasConstraintName("guild_ranks_ibfk_1");
            });

            modelBuilder.Entity<GuildWar>(entity =>
            {
                entity.ToTable("guild_wars");

                entity.HasIndex(e => e.Guild1)
                    .HasName("guild1");

                entity.HasIndex(e => e.Guild2)
                    .HasName("guild2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._ended)
                    .HasColumnName("ended")
                    .HasColumnType("bigint(15)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Guild1)
                    .HasColumnName("guild1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Guild2)
                    .HasColumnName("guild2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e._started)
                    .HasColumnName("started")
                    .HasColumnType("bigint(15)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("0");

                entity.Ignore(i => i.Ended);

                entity.Ignore(i => i.Started);
            });

            modelBuilder.Entity<Guild>(entity =>
            {
                entity.ToTable("guilds");

                entity.HasIndex(e => e.Name)
                    .HasName("name")
                    .IsUnique();

                entity.HasIndex(e => e.Ownerid)
                    .HasName("ownerid")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._creationdata)
                    .HasColumnName("creationdata")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Motd)
                    .IsRequired()
                    .HasColumnName("motd")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Ownerid)
                    .HasColumnName("ownerid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Owner)
                    .WithOne(p => p.Guilds)
                    .HasForeignKey<Guild>(d => d.Ownerid)
                    .HasConstraintName("guilds_ibfk_1");

                entity.Ignore(i => i.Creationdata);
            });

            modelBuilder.Entity<GuildWarKill>(entity =>
            {
                entity.ToTable("guildwar_kills");

                entity.HasIndex(e => e.Warid)
                    .HasName("warid");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Killer)
                    .IsRequired()
                    .HasColumnName("killer")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Killerguild)
                    .HasColumnName("killerguild")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasColumnName("target")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Targetguild)
                    .HasColumnName("targetguild")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e._time)
                    .HasColumnName("time")
                    .HasColumnType("bigint(15)");

                entity.Property(e => e.Warid)
                    .HasColumnName("warid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.War)
                    .WithMany(p => p.GuildwarKills)
                    .HasForeignKey(d => d.Warid)
                    .HasConstraintName("guildwar_kills_ibfk_1");

                entity.Ignore(i => i.Time);
            });

            modelBuilder.Entity<HouseList>(entity =>
            {
                entity.ToTable("house_lists");

                entity.HasIndex(e => e.HouseId)
                    .HasName("house_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HouseId)
                    .HasColumnName("house_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.List)
                    .IsRequired()
                    .HasColumnName("list")
                    .HasColumnType("text");

                entity.Property(e => e.Listid)
                    .HasColumnName("listid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.HouseLists)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("house_lists_ibfk_1");
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.ToTable("houses");

                entity.HasIndex(e => e.Owner)
                    .HasName("owner");

                entity.HasIndex(e => e.TownId)
                    .HasName("town_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Beds)
                    .HasColumnName("beds")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Bid)
                    .HasColumnName("bid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.BidEnd)
                    .HasColumnName("bid_end")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.HighestBidder)
                    .HasColumnName("highest_bidder")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LastBid)
                    .HasColumnName("last_bid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Paid)
                    .HasColumnName("paid")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Rent)
                    .HasColumnName("rent")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TownId)
                    .HasColumnName("town_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Warnings)
                    .HasColumnName("warnings")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Ignore(i => i.PaidInDays);
            });

            modelBuilder.Entity<IpBan>(entity =>
            {
                entity.HasKey(e => e.Ip)
                    .HasName("PK_ip_bans");

                entity.ToTable("ip_bans");

                entity.HasIndex(e => e.BannedBy)
                    .HasName("banned_by");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e._bannedAt)
                    .HasColumnName("banned_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BannedBy)
                    .HasColumnName("banned_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e._expiresAt)
                    .HasColumnName("expires_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.BannedByNavigation)
                    .WithMany(p => p.IpBans)
                    .HasForeignKey(d => d.BannedBy)
                    .HasConstraintName("ip_bans_ibfk_1");

                entity.Ignore(i => i.BannedAt);
                
                entity.Ignore(i => i.ExpiresAt);
            });

            modelBuilder.Entity<MarketHistory>(entity =>
            {
                entity.ToTable("market_history");

                entity.HasIndex(e => new { e.PlayerId, e.Sale })
                    .HasName("player_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e._expiresAt)
                    .HasColumnName("expires_at")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e._insertedAt)
                    .HasColumnName("inserted")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("itemtype")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sale)
                    .HasColumnName("sale")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("tinyint(1) unsigned");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MarketHistory)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("market_history_ibfk_1");

                entity.Ignore(i => i.InsertedAt);

                entity.Ignore(i => i.ExpiresAt);
            });

            modelBuilder.Entity<MarketOffer>(entity =>
            {
                entity.ToTable("market_offers");

                entity.HasIndex(e => e._created)
                    .HasName("created");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id");

                entity.HasIndex(e => new { e.Sale, e.Itemtype })
                    .HasName("sale");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Anonymous)
                    .HasColumnName("anonymous")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e._created)
                    .HasColumnName("created")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("itemtype")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sale)
                    .HasColumnName("sale")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MarketOffers)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("market_offers_ibfk_1");

                entity.Ignore(i => i.CreatedAt);
            });

            modelBuilder.Entity<PlayerDeath>(entity =>
            {
                entity.ToTable("player_deaths");

                entity.HasIndex(e => e.KilledBy)
                    .HasName("killed_by");

                entity.HasIndex(e => e.MostdamageBy)
                    .HasName("mostdamage_by");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsPlayer)
                    .HasColumnName("is_player")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.KilledBy)
                    .IsRequired()
                    .HasColumnName("killed_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.MostdamageBy)
                    .IsRequired()
                    .HasColumnName("mostdamage_by")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MostdamageIsPlayer)
                    .HasColumnName("mostdamage_is_player")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MostdamageUnjustified)
                    .HasColumnName("mostdamage_unjustified")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._time)
                    .HasColumnName("time")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Unjustified)
                    .HasColumnName("unjustified")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerDeaths)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_deaths_ibfk_1");

                entity.Ignore(i => i.Time);
            });

            modelBuilder.Entity<PlayerDepotItem>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.Sid })
                    .HasName("player_id_2");

                entity.ToTable("player_depotitems");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sid)
                    .HasColumnName("sid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasColumnType("blob");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("itemtype")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerDepotitems)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_depotitems_ibfk_1");
            });

            modelBuilder.Entity<PlayerInboxItem>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.Sid })
                    .HasName("player_id_2");

                entity.ToTable("player_inboxitems");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sid)
                    .HasColumnName("sid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasColumnType("blob");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("itemtype")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerInboxitems)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_inboxitems_ibfk_1");
            });

            modelBuilder.Entity<PlayerItem>(entity =>
            {
                entity.ToTable("player_items");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id");

                entity.HasIndex(e => e.Sid)
                    .HasName("sid");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasColumnType("blob");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("itemtype")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sid)
                    .HasColumnName("sid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerItems)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_items_ibfk_1");
            });

            modelBuilder.Entity<PlayerNamelock>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK_player_namelocks");

                entity.ToTable("player_namelocks");

                entity.HasIndex(e => e.NamelockedBy)
                    .HasName("namelocked_by");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e._namelockedAt)
                    .HasColumnName("namelocked_at")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.NamelockedBy)
                    .HasColumnName("namelocked_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.NamelockedByNavigation)
                    .WithMany(p => p.PlayerNamelocksNamelockedByNavigation)
                    .HasForeignKey(d => d.NamelockedBy)
                    .HasConstraintName("player_namelocks_ibfk_2");

                entity.HasOne(d => d.Player)
                    .WithOne(p => p.PlayerNamelocksPlayer)
                    .HasForeignKey<PlayerNamelock>(d => d.PlayerId)
                    .HasConstraintName("player_namelocks_ibfk_1");

                entity.Ignore(i => i.NamelockedAt);
            });

            modelBuilder.Entity<PlayerSpell>(entity =>
            {
                entity.ToTable("player_spells");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerSpells)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_spells_ibfk_1");
            });

            modelBuilder.Entity<PlayerStorage>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.Key })
                    .HasName("PK_player_storage");

                entity.ToTable("player_storage");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerStorage)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("player_storage_ibfk_1");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players");

                entity.HasIndex(e => e.AccountId)
                    .HasName("account_id");

                entity.HasIndex(e => e.Name)
                    .HasName("name")
                    .IsUnique();

                entity.HasIndex(e => e.Vocation)
                    .HasName("vocation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Blessings)
                    .HasColumnName("blessings")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Cap)
                    .HasColumnName("cap")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Conditions)
                    .IsRequired()
                    .HasColumnName("conditions")
                    .HasColumnType("blob");

                entity.Property(e => e._deletion)
                    .HasColumnName("deletion")
                    .HasColumnType("bigint(15)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValue(String.Empty);

                entity.Property(e => e.Experience)
                    .HasColumnName("experience")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Health)
                    .HasColumnName("health")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("150");

                entity.Property(e => e.Healthmax)
                    .HasColumnName("healthmax")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("150");

                entity.Property(e => e.Lastip)
                    .HasColumnName("lastip")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e._lastlogin)
                    .HasColumnName("lastlogin")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e._lastlogout)
                    .HasColumnName("lastlogout")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Lookaddons)
                    .HasColumnName("lookaddons")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lookbody)
                    .HasColumnName("lookbody")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lookfeet)
                    .HasColumnName("lookfeet")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lookhead)
                    .HasColumnName("lookhead")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Looklegs)
                    .HasColumnName("looklegs")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Looktype)
                    .HasColumnName("looktype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("136");

                entity.Property(e => e.Maglevel)
                    .HasColumnName("maglevel")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Mana)
                    .HasColumnName("mana")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Manamax)
                    .HasColumnName("manamax")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Manaspent)
                    .HasColumnName("manaspent")
                    .HasColumnType("int(11) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OfflinetrainingSkill)
                    .HasColumnName("offlinetraining_skill")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.OfflinetrainingTime)
                    .HasColumnName("offlinetraining_time")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("43200");

                entity.Property(e => e.Onlinetime)
                    .HasColumnName("onlinetime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Posx)
                    .HasColumnName("posx")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Posy)
                    .HasColumnName("posy")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Posz)
                    .HasColumnName("posz")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Save)
                    .HasColumnName("save")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillAxe)
                    .HasColumnName("skill_axe")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillAxeTries)
                    .HasColumnName("skill_axe_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillClub)
                    .HasColumnName("skill_club")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillClubTries)
                    .HasColumnName("skill_club_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillDist)
                    .HasColumnName("skill_dist")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillDistTries)
                    .HasColumnName("skill_dist_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillFishing)
                    .HasColumnName("skill_fishing")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillFishingTries)
                    .HasColumnName("skill_fishing_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillFist)
                    .HasColumnName("skill_fist")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillFistTries)
                    .HasColumnName("skill_fist_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillShielding)
                    .HasColumnName("skill_shielding")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillShieldingTries)
                    .HasColumnName("skill_shielding_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SkillSword)
                    .HasColumnName("skill_sword")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SkillSwordTries)
                    .HasColumnName("skill_sword_tries")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Skull)
                    .HasColumnName("skull")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Skulltime)
                    .HasColumnName("skulltime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Soul)
                    .HasColumnName("soul")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Stamina)
                    .HasColumnName("stamina")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("2520");

                entity.Property(e => e.TownId)
                    .HasColumnName("town_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Vocation)
                    .HasColumnName("vocation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("players_ibfk_1");

                entity.Ignore(i => i.Deletion);

                entity.Ignore(i => i.LastLogin);

                entity.Ignore(i => i.LastLogout);
            });

            modelBuilder.Entity<PlayersOnline>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK_players_online");

                entity.ToTable("players_online");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("player_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ServerConfig>(entity =>
            {
                entity.HasKey(e => e.Config)
                    .HasName("PK_server_config");

                entity.ToTable("server_config");

                entity.Property(e => e.Config)
                    .HasColumnName("config")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<TileStore>(entity =>
            {
                entity.ToTable("tile_store");

                entity.HasIndex(e => e.HouseId)
                    .HasName("house_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.HouseId)
                    .HasColumnName("house_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.TileStore)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("tile_store_ibfk_1");
            });
        }
    }
}