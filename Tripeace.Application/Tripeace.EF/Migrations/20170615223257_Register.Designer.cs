using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Tripeace.EF;
using Tripeace.Domain.Enums;

namespace Tripeace.EF.Migrations
{
    [DbContext(typeof(ServerContext))]
    [Migration("20170615223257_Register")]
    partial class Register
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("char(40)");

                    b.Property<int>("Premdays")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("premdays")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Secret")
                        .HasColumnName("secret")
                        .HasColumnType("char(16)");

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("type")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("1");

                    b.Property<int>("_creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("creation")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("_lastday")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lastday")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("name");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountBan", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnName("account_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("BannedById")
                        .HasColumnName("banned_by")
                        .HasColumnType("int(11)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnName("reason")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("_bannedAt")
                        .HasColumnName("banned_at")
                        .HasColumnType("bigint(20)");

                    b.Property<long>("_expiresAt")
                        .HasColumnName("expires_at")
                        .HasColumnType("bigint(20)");

                    b.HasKey("AccountId")
                        .HasName("PK_account_bans");

                    b.HasIndex("BannedById")
                        .HasName("banned_by");

                    b.ToTable("account_bans");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountBanHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("AccountId")
                        .HasColumnName("account_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("BannedById")
                        .HasColumnName("banned_by")
                        .HasColumnType("int(11)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnName("reason")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("_bannedAt")
                        .HasColumnName("banned_at")
                        .HasColumnType("bigint(20)");

                    b.Property<long>("_expiredAt")
                        .HasColumnName("expired_at")
                        .HasColumnType("bigint(20)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .HasName("account_id");

                    b.HasIndex("BannedById")
                        .HasName("banned_by");

                    b.ToTable("account_ban_history");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountIdentity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AccountIdentityId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<bool>("News");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AccountIdentityId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountViplist", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnName("account_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(128)");

                    b.Property<byte>("Icon")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("icon")
                        .HasColumnType("tinyint(2) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Notify")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("notify")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.HasKey("AccountId", "PlayerId")
                        .HasName("account_player_index");

                    b.HasIndex("PlayerId")
                        .HasName("player_id");

                    b.ToTable("account_viplist");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.Guild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Motd")
                        .IsRequired()
                        .HasColumnName("motd")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Ownerid")
                        .HasColumnName("ownerid")
                        .HasColumnType("int(11)");

                    b.Property<int>("_creationdata")
                        .HasColumnName("creationdata")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("name");

                    b.HasIndex("Ownerid")
                        .IsUnique()
                        .HasName("ownerid");

                    b.ToTable("guilds");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildInvite", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("GuildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("guild_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("PlayerId", "GuildId")
                        .HasName("PK_guild_invites");

                    b.HasIndex("GuildId")
                        .HasName("guild_id");

                    b.ToTable("guild_invites");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildMembership", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("GuildId")
                        .HasColumnName("guild_id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnName("nick")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("RankId")
                        .HasColumnName("rank_id")
                        .HasColumnType("int(11)");

                    b.HasKey("PlayerId")
                        .HasName("PK_guild_membership");

                    b.HasIndex("GuildId")
                        .HasName("guild_id");

                    b.HasIndex("RankId")
                        .HasName("rank_id");

                    b.ToTable("guild_membership");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("GuildId")
                        .HasColumnName("guild_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Level")
                        .HasColumnName("level")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId")
                        .HasName("guild_id");

                    b.ToTable("guild_ranks");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildWar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Guild1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("guild1")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Guild2")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("guild2")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name1")
                        .IsRequired()
                        .HasColumnName("name1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name2")
                        .IsRequired()
                        .HasColumnName("name2")
                        .HasColumnType("varchar(255)");

                    b.Property<sbyte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status")
                        .HasColumnType("tinyint(2)")
                        .HasDefaultValueSql("0");

                    b.Property<long>("_ended")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ended")
                        .HasColumnType("bigint(15)")
                        .HasDefaultValueSql("0");

                    b.Property<long>("_started")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("started")
                        .HasColumnType("bigint(15)")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("Guild1")
                        .HasName("guild1");

                    b.HasIndex("Guild2")
                        .HasName("guild2");

                    b.ToTable("guild_wars");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildWarKill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Killer")
                        .IsRequired()
                        .HasColumnName("killer")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Killerguild")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("killerguild")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnName("target")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Targetguild")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("targetguild")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Warid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("warid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<long>("_time")
                        .HasColumnName("time")
                        .HasColumnType("bigint(15)");

                    b.HasKey("Id");

                    b.HasIndex("Warid")
                        .HasName("warid");

                    b.ToTable("guildwar_kills");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Beds")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("beds")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Bid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("bid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("BidEnd")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("bid_end")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("HighestBidder")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("highest_bidder")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("LastBid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("last_bid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Owner")
                        .HasColumnName("owner")
                        .HasColumnType("int(11)");

                    b.Property<int>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("paid")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Rent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rent")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Size")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("size")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("TownId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("town_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Warnings")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("warnings")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("Owner")
                        .HasName("owner");

                    b.HasIndex("TownId")
                        .HasName("town_id");

                    b.ToTable("houses");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.HouseList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("HouseId")
                        .HasColumnName("house_id")
                        .HasColumnType("int(11)");

                    b.Property<string>("List")
                        .IsRequired()
                        .HasColumnName("list")
                        .HasColumnType("text");

                    b.Property<int>("Listid")
                        .HasColumnName("listid")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId")
                        .HasName("house_id");

                    b.ToTable("house_lists");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.IpBan", b =>
                {
                    b.Property<int>("Ip")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ip")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("BannedBy")
                        .HasColumnName("banned_by")
                        .HasColumnType("int(11)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnName("reason")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("_bannedAt")
                        .HasColumnName("banned_at")
                        .HasColumnType("bigint(20)");

                    b.Property<long>("_expiresAt")
                        .HasColumnName("expires_at")
                        .HasColumnType("bigint(20)");

                    b.HasKey("Ip")
                        .HasName("PK_ip_bans");

                    b.HasIndex("BannedBy")
                        .HasName("banned_by");

                    b.ToTable("ip_bans");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.MarketHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<ushort>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("smallint(5) unsigned");

                    b.Property<int>("Itemtype")
                        .HasColumnName("itemtype")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Sale")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sale")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("State")
                        .HasColumnName("state")
                        .HasColumnType("tinyint(1) unsigned");

                    b.Property<ulong>("_expiresAt")
                        .HasColumnName("expires_at")
                        .HasColumnType("bigint(20) unsigned");

                    b.Property<ulong>("_insertedAt")
                        .HasColumnName("inserted")
                        .HasColumnType("bigint(20) unsigned");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId", "Sale")
                        .HasName("player_id");

                    b.ToTable("market_history");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.MarketOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<ushort>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("smallint(5) unsigned");

                    b.Property<bool>("Anonymous")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("anonymous")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Itemtype")
                        .HasColumnName("itemtype")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Sale")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sale")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<ulong>("_created")
                        .HasColumnName("created")
                        .HasColumnType("bigint(20) unsigned");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .HasName("player_id");

                    b.HasIndex("_created")
                        .HasName("created");

                    b.HasIndex("Sale", "Itemtype")
                        .HasName("sale");

                    b.ToTable("market_offers");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("account_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<ulong>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("balance")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<sbyte>("Blessings")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("blessings")
                        .HasColumnType("tinyint(2)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Cap")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cap")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<byte[]>("Conditions")
                        .IsRequired()
                        .HasColumnName("conditions")
                        .HasColumnType("blob");

                    b.Property<long>("Experience")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("experience")
                        .HasColumnType("bigint(20)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("group_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("1");

                    b.Property<int>("Health")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("health")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("150");

                    b.Property<int>("Healthmax")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("healthmax")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("150");

                    b.Property<int>("Lastip")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lastip")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Level")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("level")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("1");

                    b.Property<int>("Lookaddons")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lookaddons")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Lookbody")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lookbody")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Lookfeet")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lookfeet")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Lookhead")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lookhead")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Looklegs")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("looklegs")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Looktype")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("looktype")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("136");

                    b.Property<int>("Maglevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("maglevel")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Mana")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("mana")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Manamax")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("manamax")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Manaspent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("manaspent")
                        .HasColumnType("int(11) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OfflinetrainingSkill")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("offlinetraining_skill")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("-1");

                    b.Property<ushort>("OfflinetrainingTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("offlinetraining_time")
                        .HasColumnType("smallint(5) unsigned")
                        .HasDefaultValueSql("43200");

                    b.Property<int>("Onlinetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("onlinetime")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Posx")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("posx")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Posy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("posy")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Posz")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("posz")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Save")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("save")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("1");

                    b.Property<int>("Sex")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sex")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillAxe")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_axe")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillAxeTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_axe_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillClub")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_club")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillClubTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_club_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillDist")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_dist")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillDistTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_dist_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillFishing")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_fishing")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillFishingTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_fishing_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillFist")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_fist")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillFistTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_fist_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillShielding")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_shielding")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillShieldingTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_shielding_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SkillSword")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_sword")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("10");

                    b.Property<ulong>("SkillSwordTries")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_sword_tries")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Skull")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skull")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Skulltime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skulltime")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Soul")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("soul")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<ushort>("Stamina")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("stamina")
                        .HasColumnType("smallint(5) unsigned")
                        .HasDefaultValueSql("2520");

                    b.Property<int>("TownId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("town_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Vocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("vocation")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<long>("_deletion")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("deletion")
                        .HasColumnType("bigint(15)")
                        .HasDefaultValueSql("0");

                    b.Property<ulong>("_lastlogin")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lastlogin")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<ulong>("_lastlogout")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("lastlogout")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .HasName("account_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("name");

                    b.HasIndex("Vocation")
                        .HasName("vocation");

                    b.ToTable("players");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerDeath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<bool>("IsPlayer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_player")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("1");

                    b.Property<string>("KilledBy")
                        .IsRequired()
                        .HasColumnName("killed_by")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Level")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("level")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("1");

                    b.Property<string>("MostdamageBy")
                        .IsRequired()
                        .HasColumnName("mostdamage_by")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("MostdamageIsPlayer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("mostdamage_is_player")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("MostdamageUnjustified")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("mostdamage_unjustified")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<bool>("Unjustified")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("unjustified")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<ulong>("_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("time")
                        .HasColumnType("bigint(20) unsigned")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("KilledBy")
                        .HasName("killed_by");

                    b.HasIndex("MostdamageBy")
                        .HasName("mostdamage_by");

                    b.HasIndex("PlayerId")
                        .HasName("player_id");

                    b.ToTable("player_deaths");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerDepotItem", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Sid")
                        .HasColumnName("sid")
                        .HasColumnType("int(11)");

                    b.Property<byte[]>("Attributes")
                        .IsRequired()
                        .HasColumnName("attributes")
                        .HasColumnType("blob");

                    b.Property<short>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("count")
                        .HasColumnType("smallint(5)")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Itemtype")
                        .HasColumnName("itemtype")
                        .HasColumnType("smallint(6)");

                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("PlayerId", "Sid")
                        .HasName("player_id_2");

                    b.ToTable("player_depotitems");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerInboxItem", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("Sid")
                        .HasColumnName("sid")
                        .HasColumnType("int(11)");

                    b.Property<byte[]>("Attributes")
                        .IsRequired()
                        .HasColumnName("attributes")
                        .HasColumnType("blob");

                    b.Property<short>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("count")
                        .HasColumnType("smallint(5)")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Itemtype")
                        .HasColumnName("itemtype")
                        .HasColumnType("smallint(6)");

                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("PlayerId", "Sid")
                        .HasName("player_id_2");

                    b.ToTable("player_inboxitems");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<byte[]>("Attributes")
                        .IsRequired()
                        .HasColumnName("attributes")
                        .HasColumnType("blob");

                    b.Property<short>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("count")
                        .HasColumnType("smallint(5)")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Itemtype")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("itemtype")
                        .HasColumnType("smallint(6)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Sid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sid")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .HasName("player_id");

                    b.HasIndex("Sid")
                        .HasName("sid");

                    b.ToTable("player_items");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerNamelock", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("NamelockedBy")
                        .HasColumnName("namelocked_by")
                        .HasColumnType("int(11)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnName("reason")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("_namelockedAt")
                        .HasColumnName("namelocked_at")
                        .HasColumnType("bigint(20)");

                    b.HasKey("PlayerId")
                        .HasName("PK_player_namelocks");

                    b.HasIndex("NamelockedBy")
                        .HasName("namelocked_by");

                    b.ToTable("player_namelocks");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayersOnline", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.HasKey("PlayerId")
                        .HasName("PK_players_online");

                    b.ToTable("players_online");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerSpell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .HasName("player_id");

                    b.ToTable("player_spells");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerStorage", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("player_id")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("key")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("value")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("PlayerId", "Key")
                        .HasName("PK_player_storage");

                    b.ToTable("player_storage");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.ServerConfig", b =>
                {
                    b.Property<string>("Config")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("config")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Config")
                        .HasName("PK_server_config");

                    b.ToTable("server_config");
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.TileStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnName("data");

                    b.Property<int>("HouseId")
                        .HasColumnName("house_id")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId")
                        .HasName("house_id");

                    b.ToTable("tile_store");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.AccountIdentity")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.AccountIdentity")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.AccountIdentity")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountBan", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Account", "Account")
                        .WithOne("AccountBans")
                        .HasForeignKey("Tripeace.Domain.Entities.AccountBan", "AccountId")
                        .HasConstraintName("account_bans_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "BannedBy")
                        .WithMany("AccountBans")
                        .HasForeignKey("BannedById")
                        .HasConstraintName("account_bans_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountBanHistory", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Account", "Account")
                        .WithMany("AccountBanHistory")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("account_ban_history_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "BannedBy")
                        .WithMany("AccountBanHistory")
                        .HasForeignKey("BannedById")
                        .HasConstraintName("account_ban_history_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountIdentity", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Account", "Account")
                        .WithOne("AccountIdentity")
                        .HasForeignKey("Tripeace.Domain.Entities.AccountIdentity", "AccountIdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.AccountViplist", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Account", "Account")
                        .WithMany("AccountViplist")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("account_viplist_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("AccountViplist")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("account_viplist_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.Guild", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Owner")
                        .WithOne("Guilds")
                        .HasForeignKey("Tripeace.Domain.Entities.Guild", "Ownerid")
                        .HasConstraintName("guilds_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildInvite", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Guild", "Guild")
                        .WithMany("GuildInvites")
                        .HasForeignKey("GuildId")
                        .HasConstraintName("guild_invites_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("GuildInvites")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("guild_invites_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildMembership", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Guild", "Guild")
                        .WithMany("GuildMembership")
                        .HasForeignKey("GuildId")
                        .HasConstraintName("guild_membership_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithOne("GuildMembership")
                        .HasForeignKey("Tripeace.Domain.Entities.GuildMembership", "PlayerId")
                        .HasConstraintName("guild_membership_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.GuildRank", "Rank")
                        .WithMany("GuildMembership")
                        .HasForeignKey("RankId")
                        .HasConstraintName("guild_membership_ibfk_3")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildRank", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Guild", "Guild")
                        .WithMany("GuildRanks")
                        .HasForeignKey("GuildId")
                        .HasConstraintName("guild_ranks_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.GuildWarKill", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.GuildWar", "War")
                        .WithMany("GuildwarKills")
                        .HasForeignKey("Warid")
                        .HasConstraintName("guildwar_kills_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.HouseList", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.House", "House")
                        .WithMany("HouseLists")
                        .HasForeignKey("HouseId")
                        .HasConstraintName("house_lists_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.IpBan", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "BannedByNavigation")
                        .WithMany("IpBans")
                        .HasForeignKey("BannedBy")
                        .HasConstraintName("ip_bans_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.MarketHistory", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("MarketHistory")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("market_history_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.MarketOffer", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("MarketOffers")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("market_offers_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.Player", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Account", "Account")
                        .WithMany("Players")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("players_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerDeath", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerDeaths")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_deaths_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerDepotItem", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerDepotitems")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_depotitems_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerInboxItem", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerInboxitems")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_inboxitems_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerItem", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerItems")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_items_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerNamelock", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "NamelockedByNavigation")
                        .WithMany("PlayerNamelocksNamelockedByNavigation")
                        .HasForeignKey("NamelockedBy")
                        .HasConstraintName("player_namelocks_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithOne("PlayerNamelocksPlayer")
                        .HasForeignKey("Tripeace.Domain.Entities.PlayerNamelock", "PlayerId")
                        .HasConstraintName("player_namelocks_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerSpell", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerSpells")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_spells_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.PlayerStorage", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.Player", "Player")
                        .WithMany("PlayerStorage")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("player_storage_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tripeace.Domain.Entities.TileStore", b =>
                {
                    b.HasOne("Tripeace.Domain.Entities.House", "House")
                        .WithMany("TileStore")
                        .HasForeignKey("HouseId")
                        .HasConstraintName("tile_store_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
