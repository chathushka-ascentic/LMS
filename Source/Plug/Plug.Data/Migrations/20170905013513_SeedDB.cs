using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Plug.Data.Migrations
{
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" 
            MERGE Students AS target  
            USING
            (
                VALUES 
                (CONVERT(uniqueidentifier, 'd00094ac-b71e-427c-b1f8-c5098afc12c5'),'SYSTEM',GETDATE(),'pasan@gmail.com','Donkey@1','SYSTEM',GETDATE()),
                (CONVERT(uniqueidentifier, '17d18869-599e-4855-99e2-77f89463fc61'),'SYSTEM',GETDATE(),'saman@gmail.com','Donkey@1','SYSTEM',GETDATE()),
                (CONVERT(uniqueidentifier, '4cbab65b-cbe1-4660-9ed6-030b54568f26'),'SYSTEM',GETDATE(),'wimal@gmail.com','Donkey@1','SYSTEM',GETDATE()),
                (CONVERT(uniqueidentifier, '66ea3a48-49e7-41d4-b36a-bbd86922fac5'),'SYSTEM',GETDATE(),'test1@gmail.com','Donkey@1','SYSTEM',GETDATE()),
                (CONVERT(uniqueidentifier, '20915b13-509c-465d-a9a2-8643171f0395'),'SYSTEM',GETDATE(),'test2@gmail.com','Donkey@1','SYSTEM',GETDATE()),
                (CONVERT(uniqueidentifier, 'd4387045-5ce3-4af3-8523-d130ace14876'),'SYSTEM',GETDATE(),'test3@gmail.com','Donkey@1','SYSTEM',GETDATE())
            ) 
            AS source(Id,CreateBy,CreateDate,Email,Password,UpdateBy,UpdateDate)
            ON (target.Id = source.Id)
            WHEN NOT MATCHED THEN
                INSERT(Id,CreateBy,CreateDate,Email,Password,UpdateBy,UpdateDate)
                VALUES(source.Id,source.CreateBy,source.CreateDate,source.Email,source.Password,source.UpdateBy,source.UpdateDate);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Students");
        }
    }
}
