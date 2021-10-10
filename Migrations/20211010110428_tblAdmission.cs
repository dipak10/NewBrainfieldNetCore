using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBrainfieldNetCore.Migrations
{
    public partial class tblAdmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demo");

            migrationBuilder.CreateTable(
                name: "tblAdmissions",
                columns: table => new
                {
                    AdmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionTXNID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExamName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastBoard1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastYear1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPercentage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExamName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastBoard2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastYear2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPercentage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExamName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastBoard3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastYear3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPercentage3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualFamilyIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasMadePayment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmissions", x => x.AdmissionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAdmissions");

        }
    }
}
