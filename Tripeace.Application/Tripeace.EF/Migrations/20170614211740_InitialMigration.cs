using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tripeace.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    password = table.Column<string>(type: "char(40)", nullable: false),
                    premdays = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    secret = table.Column<string>(type: "char(16)", nullable: true),
                    type = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    creation = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lastday = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "guild_wars",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    guild1 = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    guild2 = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    name2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    status = table.Column<sbyte>(type: "tinyint(2)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ended = table.Column<long>(type: "bigint(15)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    started = table.Column<long>(type: "bigint(15)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_wars", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    beds = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    bid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    bid_end = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    highest_bidder = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    last_bid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    owner = table.Column<int>(type: "int(11)", nullable: false),
                    paid = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    rent = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    size = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    town_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    warnings = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players_online",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players_online", x => x.player_id);
                });

            migrationBuilder.CreateTable(
                name: "server_config",
                columns: table => new
                {
                    config = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    value = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server_config", x => x.config);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccountIdentityId = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_accounts_AccountIdentityId",
                        column: x => x.AccountIdentityId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    account_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    balance = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    blessings = table.Column<sbyte>(type: "tinyint(2)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    cap = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    conditions = table.Column<byte[]>(type: "blob", nullable: false),
                    experience = table.Column<long>(type: "bigint(20)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    group_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    health = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "150")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    healthmax = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "150")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lastip = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    level = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lookaddons = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lookbody = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lookfeet = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lookhead = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    looklegs = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    looktype = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "136")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    maglevel = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    mana = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    manamax = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    manaspent = table.Column<int>(type: "int(11) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    offlinetraining_skill = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "-1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    offlinetraining_time = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false, defaultValueSql: "43200")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    onlinetime = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    posx = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    posy = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    posz = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    save = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    sex = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_axe = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_axe_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_club = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_club_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_dist = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_dist_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_fishing = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_fishing_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_fist = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_fist_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_shielding = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_shielding_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_sword = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "10")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skill_sword_tries = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skull = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    skulltime = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    soul = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    stamina = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false, defaultValueSql: "2520")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    town_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    vocation = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    deletion = table.Column<long>(type: "bigint(15)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lastlogin = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    lastlogout = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                    table.ForeignKey(
                        name: "players_ibfk_1",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guildwar_kills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    killer = table.Column<string>(type: "varchar(50)", nullable: false),
                    killerguild = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    target = table.Column<string>(type: "varchar(50)", nullable: false),
                    targetguild = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    warid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    time = table.Column<long>(type: "bigint(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guildwar_kills", x => x.id);
                    table.ForeignKey(
                        name: "guildwar_kills_ibfk_1",
                        column: x => x.warid,
                        principalTable: "guild_wars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "house_lists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    house_id = table.Column<int>(type: "int(11)", nullable: false),
                    list = table.Column<string>(type: "text", nullable: false),
                    listid = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_house_lists", x => x.id);
                    table.ForeignKey(
                        name: "house_lists_ibfk_1",
                        column: x => x.house_id,
                        principalTable: "houses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tile_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    data = table.Column<byte[]>(nullable: false),
                    house_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tile_store", x => x.id);
                    table.ForeignKey(
                        name: "tile_store_ibfk_1",
                        column: x => x.house_id,
                        principalTable: "houses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_bans",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int(11)", nullable: false),
                    banned_by = table.Column<int>(type: "int(11)", nullable: false),
                    reason = table.Column<string>(type: "varchar(255)", nullable: false),
                    banned_at = table.Column<long>(type: "bigint(20)", nullable: false),
                    expires_at = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_bans", x => x.account_id);
                    table.ForeignKey(
                        name: "account_bans_ibfk_1",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "account_bans_ibfk_2",
                        column: x => x.banned_by,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_ban_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    account_id = table.Column<int>(type: "int(11)", nullable: false),
                    banned_by = table.Column<int>(type: "int(11)", nullable: false),
                    reason = table.Column<string>(type: "varchar(255)", nullable: false),
                    banned_at = table.Column<long>(type: "bigint(20)", nullable: false),
                    expired_at = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_ban_history", x => x.id);
                    table.ForeignKey(
                        name: "account_ban_history_ibfk_1",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "account_ban_history_ibfk_2",
                        column: x => x.banned_by,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_viplist",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int(11)", nullable: false),
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    description = table.Column<string>(type: "varchar(128)", nullable: false),
                    icon = table.Column<byte>(type: "tinyint(2) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    notify = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_player_index", x => new { x.account_id, x.player_id });
                    table.ForeignKey(
                        name: "account_viplist_ibfk_1",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "account_viplist_ibfk_2",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guilds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    motd = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    ownerid = table.Column<int>(type: "int(11)", nullable: false),
                    creationdata = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guilds", x => x.id);
                    table.ForeignKey(
                        name: "guilds_ibfk_1",
                        column: x => x.ownerid,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ip_bans",
                columns: table => new
                {
                    ip = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    banned_by = table.Column<int>(type: "int(11)", nullable: false),
                    reason = table.Column<string>(type: "varchar(255)", nullable: false),
                    banned_at = table.Column<long>(type: "bigint(20)", nullable: false),
                    expires_at = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ip_bans", x => x.ip);
                    table.ForeignKey(
                        name: "ip_bans_ibfk_1",
                        column: x => x.banned_by,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "market_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    amount = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    itemtype = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    price = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    sale = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    state = table.Column<bool>(type: "tinyint(1) unsigned", nullable: false),
                    expires_at = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false),
                    inserted = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_history", x => x.id);
                    table.ForeignKey(
                        name: "market_history_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "market_offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    amount = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    anonymous = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    itemtype = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    price = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    sale = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    created = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_offers", x => x.id);
                    table.ForeignKey(
                        name: "market_offers_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_deaths",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    is_player = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    killed_by = table.Column<string>(type: "varchar(255)", nullable: false),
                    level = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "1")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    mostdamage_by = table.Column<string>(type: "varchar(100)", nullable: false),
                    mostdamage_is_player = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    mostdamage_unjustified = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    unjustified = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    time = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_deaths", x => x.id);
                    table.ForeignKey(
                        name: "player_deaths_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_depotitems",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    sid = table.Column<int>(type: "int(11)", nullable: false),
                    attributes = table.Column<byte[]>(type: "blob", nullable: false),
                    count = table.Column<short>(type: "smallint(5)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    itemtype = table.Column<short>(type: "smallint(6)", nullable: false),
                    pid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_id_2", x => new { x.player_id, x.sid });
                    table.ForeignKey(
                        name: "player_depotitems_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_inboxitems",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    sid = table.Column<int>(type: "int(11)", nullable: false),
                    attributes = table.Column<byte[]>(type: "blob", nullable: false),
                    count = table.Column<short>(type: "smallint(5)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    itemtype = table.Column<short>(type: "smallint(6)", nullable: false),
                    pid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_id_2", x => new { x.player_id, x.sid });
                    table.ForeignKey(
                        name: "player_inboxitems_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    attributes = table.Column<byte[]>(type: "blob", nullable: false),
                    count = table.Column<short>(type: "smallint(5)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    itemtype = table.Column<short>(type: "smallint(6)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    pid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    player_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    sid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_items", x => x.id);
                    table.ForeignKey(
                        name: "player_items_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_namelocks",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    namelocked_by = table.Column<int>(type: "int(11)", nullable: false),
                    reason = table.Column<string>(type: "varchar(255)", nullable: false),
                    namelocked_at = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_namelocks", x => x.player_id);
                    table.ForeignKey(
                        name: "player_namelocks_ibfk_2",
                        column: x => x.namelocked_by,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "player_namelocks_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_spells",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    player_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_spells", x => x.id);
                    table.ForeignKey(
                        name: "player_spells_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_storage",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    key = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    value = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_storage", x => new { x.player_id, x.key });
                    table.ForeignKey(
                        name: "player_storage_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guild_invites",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    guild_id = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_invites", x => new { x.player_id, x.guild_id });
                    table.ForeignKey(
                        name: "guild_invites_ibfk_2",
                        column: x => x.guild_id,
                        principalTable: "guilds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "guild_invites_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guild_ranks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    guild_id = table.Column<int>(type: "int(11)", nullable: false),
                    level = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_ranks", x => x.id);
                    table.ForeignKey(
                        name: "guild_ranks_ibfk_1",
                        column: x => x.guild_id,
                        principalTable: "guilds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guild_membership",
                columns: table => new
                {
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    guild_id = table.Column<int>(type: "int(11)", nullable: false),
                    nick = table.Column<string>(type: "varchar(15)", nullable: false),
                    rank_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_membership", x => x.player_id);
                    table.ForeignKey(
                        name: "guild_membership_ibfk_2",
                        column: x => x.guild_id,
                        principalTable: "guilds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "guild_membership_ibfk_1",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "guild_membership_ibfk_3",
                        column: x => x.rank_id,
                        principalTable: "guild_ranks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "accounts",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "banned_by",
                table: "account_bans",
                column: "banned_by");

            migrationBuilder.CreateIndex(
                name: "account_id",
                table: "account_ban_history",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "banned_by",
                table: "account_ban_history",
                column: "banned_by");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccountIdentityId",
                table: "AspNetUsers",
                column: "AccountIdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "account_viplist",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "guilds",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ownerid",
                table: "guilds",
                column: "ownerid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "guild_id",
                table: "guild_invites",
                column: "guild_id");

            migrationBuilder.CreateIndex(
                name: "guild_id",
                table: "guild_membership",
                column: "guild_id");

            migrationBuilder.CreateIndex(
                name: "rank_id",
                table: "guild_membership",
                column: "rank_id");

            migrationBuilder.CreateIndex(
                name: "guild_id",
                table: "guild_ranks",
                column: "guild_id");

            migrationBuilder.CreateIndex(
                name: "guild1",
                table: "guild_wars",
                column: "guild1");

            migrationBuilder.CreateIndex(
                name: "guild2",
                table: "guild_wars",
                column: "guild2");

            migrationBuilder.CreateIndex(
                name: "warid",
                table: "guildwar_kills",
                column: "warid");

            migrationBuilder.CreateIndex(
                name: "owner",
                table: "houses",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "town_id",
                table: "houses",
                column: "town_id");

            migrationBuilder.CreateIndex(
                name: "house_id",
                table: "house_lists",
                column: "house_id");

            migrationBuilder.CreateIndex(
                name: "banned_by",
                table: "ip_bans",
                column: "banned_by");

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "market_history",
                columns: new[] { "player_id", "sale" });

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "market_offers",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "created",
                table: "market_offers",
                column: "created");

            migrationBuilder.CreateIndex(
                name: "sale",
                table: "market_offers",
                columns: new[] { "sale", "itemtype" });

            migrationBuilder.CreateIndex(
                name: "account_id",
                table: "players",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "players",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "vocation",
                table: "players",
                column: "vocation");

            migrationBuilder.CreateIndex(
                name: "killed_by",
                table: "player_deaths",
                column: "killed_by");

            migrationBuilder.CreateIndex(
                name: "mostdamage_by",
                table: "player_deaths",
                column: "mostdamage_by");

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "player_deaths",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "player_items",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "sid",
                table: "player_items",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "namelocked_by",
                table: "player_namelocks",
                column: "namelocked_by");

            migrationBuilder.CreateIndex(
                name: "player_id",
                table: "player_spells",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "house_id",
                table: "tile_store",
                column: "house_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "account_bans");

            migrationBuilder.DropTable(
                name: "account_ban_history");

            migrationBuilder.DropTable(
                name: "account_viplist");

            migrationBuilder.DropTable(
                name: "guild_invites");

            migrationBuilder.DropTable(
                name: "guild_membership");

            migrationBuilder.DropTable(
                name: "guildwar_kills");

            migrationBuilder.DropTable(
                name: "house_lists");

            migrationBuilder.DropTable(
                name: "ip_bans");

            migrationBuilder.DropTable(
                name: "market_history");

            migrationBuilder.DropTable(
                name: "market_offers");

            migrationBuilder.DropTable(
                name: "player_deaths");

            migrationBuilder.DropTable(
                name: "player_depotitems");

            migrationBuilder.DropTable(
                name: "player_inboxitems");

            migrationBuilder.DropTable(
                name: "player_items");

            migrationBuilder.DropTable(
                name: "player_namelocks");

            migrationBuilder.DropTable(
                name: "players_online");

            migrationBuilder.DropTable(
                name: "player_spells");

            migrationBuilder.DropTable(
                name: "player_storage");

            migrationBuilder.DropTable(
                name: "server_config");

            migrationBuilder.DropTable(
                name: "tile_store");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "guild_ranks");

            migrationBuilder.DropTable(
                name: "guild_wars");

            migrationBuilder.DropTable(
                name: "houses");

            migrationBuilder.DropTable(
                name: "guilds");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
