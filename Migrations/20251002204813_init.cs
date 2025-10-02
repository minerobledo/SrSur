using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SrSur.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parentid = table.Column<long>(type: "bigint", nullable: true),
                    categoryname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    categorydescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lasteditdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                    table.ForeignKey(
                        name: "categories_parentid_fkey",
                        column: x => x.parentid,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "guest",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    useragent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ipaddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    referrer = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    datecreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("guest_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Resourses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    CloudflareId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Variants = table.Column<string>(type: "jsonb", nullable: false),
                    Uploaded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sponsors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    lastpayment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sponsor_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tagname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tagdescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lasteditdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tags_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dateregister = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SponsorsImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SponsorID = table.Column<long>(type: "bigint", nullable: false),
                    CloudflareId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Variants = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponsorsImages_sponsors_SponsorID",
                        column: x => x.SponsorID,
                        principalTable: "sponsors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    authorid = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    body = table.Column<string>(type: "text", nullable: true),
                    commentstatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    publishdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lasteditdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    poststatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    excerpt = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CloudflareId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Variants = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("posts_pkey", x => x.id);
                    table.ForeignKey(
                        name: "posts_authorid_fkey",
                        column: x => x.authorid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refreshtokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<long>(type: "bigint", nullable: false),
                    token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    expirydate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    replacedbytoken = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refreshtokens_pkey", x => x.id);
                    table.ForeignKey(
                        name: "refreshtokens_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    postid = table.Column<long>(type: "bigint", nullable: false),
                    userid = table.Column<long>(type: "bigint", nullable: true),
                    parentid = table.Column<long>(type: "bigint", nullable: true),
                    author = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    emailauthor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    device = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    datecreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    authorurl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comments_pkey", x => x.id);
                    table.ForeignKey(
                        name: "comments_parentid_fkey",
                        column: x => x.parentid,
                        principalTable: "comments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "comments_postid_fkey",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "comments_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "postincategory",
                columns: table => new
                {
                    postid = table.Column<long>(type: "bigint", nullable: false),
                    categoryid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("postincategory_pkey", x => new { x.postid, x.categoryid });
                    table.ForeignKey(
                        name: "postincategory_categoryid_fkey",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "postincategory_postid_fkey",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "postintag",
                columns: table => new
                {
                    postid = table.Column<long>(type: "bigint", nullable: false),
                    tagid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("postintag_pkey", x => new { x.postid, x.tagid });
                    table.ForeignKey(
                        name: "postintag_postid_fkey",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "postintag_tagid_fkey",
                        column: x => x.tagid,
                        principalTable: "tags",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "postsvisitedbyguest",
                columns: table => new
                {
                    postid = table.Column<long>(type: "bigint", nullable: false),
                    guestid = table.Column<long>(type: "bigint", nullable: false),
                    visitetime = table.Column<int>(type: "integer", nullable: false),
                    datevisited = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("postsvisitedbyguest_pkey", x => new { x.postid, x.guestid });
                    table.ForeignKey(
                        name: "postsvisitedbyguest_guestid_fkey",
                        column: x => x.guestid,
                        principalTable: "guest",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "postsvisitedbyguest_postid_fkey",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_parentid",
                table: "categories",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "IX_comments_parentid",
                table: "comments",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "IX_comments_postid",
                table: "comments",
                column: "postid");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userid",
                table: "comments",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_postincategory_categoryid",
                table: "postincategory",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_postintag_tagid",
                table: "postintag",
                column: "tagid");

            migrationBuilder.CreateIndex(
                name: "IX_posts_authorid",
                table: "posts",
                column: "authorid");

            migrationBuilder.CreateIndex(
                name: "IX_postsvisitedbyguest_guestid",
                table: "postsvisitedbyguest",
                column: "guestid");

            migrationBuilder.CreateIndex(
                name: "IX_refreshtokens_userid",
                table: "refreshtokens",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorsImages_SponsorID",
                table: "SponsorsImages",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "postincategory");

            migrationBuilder.DropTable(
                name: "postintag");

            migrationBuilder.DropTable(
                name: "postsvisitedbyguest");

            migrationBuilder.DropTable(
                name: "refreshtokens");

            migrationBuilder.DropTable(
                name: "Resourses");

            migrationBuilder.DropTable(
                name: "SponsorsImages");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "guest");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "sponsors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
