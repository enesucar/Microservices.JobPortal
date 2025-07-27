using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerWay.JobSeekerService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "JobSeeker");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekers",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AboutMe = table.Column<string>(type: "character varying(1048)", maxLength: 1048, nullable: true),
                    Interests = table.Column<string>(type: "character varying(1048)", maxLength: 1048, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    WebSite = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Instragram = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Twitter = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Linkedin = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Github = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ProfilePhoto = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DriverLicenseType = table.Column<int>(type: "integer", nullable: true),
                    GenderType = table.Column<int>(type: "integer", nullable: true),
                    MilitaryStatus = table.Column<int>(type: "integer", nullable: true),
                    IsSmoking = table.Column<bool>(type: "boolean", nullable: true),
                    ResumeVideo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekers_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "JobSeeker",
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerCertificates",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ObtainedYear = table.Column<short>(type: "smallint", nullable: false),
                    ObtainedMonth = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerCertificates_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerLanguages",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageType = table.Column<int>(type: "integer", nullable: false),
                    ReadingLevelType = table.Column<int>(type: "integer", nullable: false),
                    WritingLevelType = table.Column<int>(type: "integer", nullable: false),
                    SpeakingLevelType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerLanguages_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerProjects",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectYear = table.Column<short>(type: "smallint", nullable: false),
                    ProjectMonth = table.Column<short>(type: "smallint", nullable: false),
                    Link = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerProjects_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerReferences",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerReferences_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSeekerReferences_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "JobSeeker",
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerSchools",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EducationLevelType = table.Column<int>(type: "integer", nullable: false),
                    StartYear = table.Column<short>(type: "smallint", nullable: false),
                    EndYear = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerSchools_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerSkills",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "JobSeeker",
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerWorkExperiences",
                schema: "JobSeeker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobSeekerId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    JobTitle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CompanyTitle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    StartMonth = table.Column<short>(type: "smallint", nullable: false),
                    StartYear = table.Column<short>(type: "smallint", nullable: false),
                    EndMonth = table.Column<short>(type: "smallint", nullable: false),
                    EndYear = table.Column<short>(type: "smallint", nullable: false),
                    StillWorking = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerWorkExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerWorkExperiences_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "JobSeeker",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSeekerWorkExperiences_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "JobSeeker",
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSeekerWorkExperiences_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "JobSeeker",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerCertificates_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerCertificates",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerLanguages_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerLanguages",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProjects_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerProjects",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerReferences_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerReferences",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerReferences_PositionId",
                schema: "JobSeeker",
                table: "JobSeekerReferences",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CityId",
                schema: "JobSeeker",
                table: "JobSeekers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSchools_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerSchools",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerSkills",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_SkillId",
                schema: "JobSeeker",
                table: "JobSeekerSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerWorkExperiences_CityId",
                schema: "JobSeeker",
                table: "JobSeekerWorkExperiences",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerWorkExperiences_JobSeekerId",
                schema: "JobSeeker",
                table: "JobSeekerWorkExperiences",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerWorkExperiences_PositionId",
                schema: "JobSeeker",
                table: "JobSeekerWorkExperiences",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSeekerCertificates",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerLanguages",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerProjects",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerReferences",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerSchools",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerSkills",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekerWorkExperiences",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "JobSeekers",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "JobSeeker");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "JobSeeker");
        }
    }
}
