using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Plug.Data.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" 
                INSERT INTO Courses (Id, CreateBy,CreateDate,UpdateBy,UpdateDate,Subject,Description) 
                VALUES (CONVERT(uniqueidentifier, 'f351d441-4797-451c-8d3c-c11bdb895218'),'System',GETDATE(),'System',GETDATE(),'Building Applications with React and Flux','Get started with React, React Router, and Flux by building a real-world style data-driven application that manages Pluralsight author data.')


                INSERT INTO Courses (Id, CreateBy,CreateDate,UpdateBy,UpdateDate,Subject,Description) 
                VALUES (CONVERT(uniqueidentifier, '2ca58911-e279-40d2-a274-8bfa72e94f29'),'System',GETDATE(),'System',GETDATE(),'JavaScript Design Patterns','JavaScript should be treated like any programming language when it comes to designing your code structure. Throughout this course we''ll look at a number of different design patterns which are useful when writing JavaScript centric applications.')


                INSERT INTO Courses (Id, CreateBy,CreateDate,UpdateBy,UpdateDate,Subject,Description) 
                VALUES (CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'C# Interfaces','Do you want code that''s maintainable, extensible, and easily testable? If so, then C# interfaces are here to help. In this course, we''ll take a look at how we can use interfaces effectively in our code.')

                --Data for 
                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '7c0bb308-f782-4e13-92c0-a9390542a58a'),CONVERT(uniqueidentifier, 'f351d441-4797-451c-8d3c-c11bdb895218'),'System',GETDATE(),'System',GETDATE(),'Text','TEXT',1,'What You get',NULL,'Lot of learning stuff',NULL,NULL)

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '165154e9-664c-4c93-bcca-8acc85b8fb23'),CONVERT(uniqueidentifier, 'f351d441-4797-451c-8d3c-c11bdb895218'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 01 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, 'acce6dcb-55fe-4e11-9868-35b46ab95bfc'),CONVERT(uniqueidentifier, 'f351d441-4797-451c-8d3c-c11bdb895218'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 02 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, 'ded96034-a913-442f-ba73-8a84f5ca7dc2'),CONVERT(uniqueidentifier, 'f351d441-4797-451c-8d3c-c11bdb895218'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 03 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')


                --Data for 
                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, 'c6fb5c31-915f-4118-aaea-d2ff6e40cb17'),CONVERT(uniqueidentifier, '2ca58911-e279-40d2-a274-8bfa72e94f29'),'System',GETDATE(),'System',GETDATE(),'Text','TEXT',1,'What You get',NULL,'Lot of learning stuff',NULL,NULL)

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '33ca81fa-187c-4a7a-b408-813ce2ae67a2'),CONVERT(uniqueidentifier, '2ca58911-e279-40d2-a274-8bfa72e94f29'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 01 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '5aa97ce8-22d3-4ffb-857b-9563723013c8'),CONVERT(uniqueidentifier, '2ca58911-e279-40d2-a274-8bfa72e94f29'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 02 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '5f31b1bd-c9e6-4b35-9b74-3b512164d1bd'),CONVERT(uniqueidentifier, '2ca58911-e279-40d2-a274-8bfa72e94f29'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 03 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')


                --Data for 
                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, 'eaa81b21-3e23-4499-997d-079c653de733'),CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'Text','TEXT',1,'What You get',NULL,'Lot of learning stuff',NULL,NULL)

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '522d7170-01e6-48e8-9747-2d97d5e50d37'),CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 01 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, 'cd9f7d68-27f4-40a5-b2a5-f329974c13bb'),CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 02 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '1ddc3af9-b2e0-4f46-a225-ce587971c829'),CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'Video','VIDEO',0,'Lesson: 03 - Intro',NULL,NULL,'http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_10mb.mp4','01:00:00.0000')

                INSERT INTO Modules(Id, CourseId, CreateBy,CreateDate,UpdateBy,UpdateDate,Discriminator,Icon,CanSkip,Title,Text,Description,Uri,Duration)
                VALUES (CONVERT(uniqueidentifier, '6d080743-b892-4d39-936d-0c244a30bfd1'),CONVERT(uniqueidentifier, 'eea1109a-ce29-48ce-a031-d33dd482c42f'),'System',GETDATE(),'System',GETDATE(),'Question','QUESTION',0,'What is it','What is it ?',NULL,NULL,NULL)

                INSERT INTO Choices(IsAnswer,[Option],QuestionId)
                VALUES (0,'Option 1',CONVERT(uniqueidentifier, '6d080743-b892-4d39-936d-0c244a30bfd1'))

                INSERT INTO Choices(IsAnswer,[Option],QuestionId)
                VALUES (0,'Option 2',CONVERT(uniqueidentifier, '6d080743-b892-4d39-936d-0c244a30bfd1'))

                INSERT INTO Choices(IsAnswer,[Option],QuestionId)
                VALUES (1,'Option 3',CONVERT(uniqueidentifier, '6d080743-b892-4d39-936d-0c244a30bfd1'))

                INSERT INTO Choices(IsAnswer,[Option],QuestionId)
                VALUES (0,'Option 4',CONVERT(uniqueidentifier, '6d080743-b892-4d39-936d-0c244a30bfd1'))
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
