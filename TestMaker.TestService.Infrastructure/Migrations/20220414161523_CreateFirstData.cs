using Microsoft.EntityFrameworkCore.Migrations;

namespace TestMaker.TestService.Infrastructure.Migrations
{
    public partial class CreateFirstData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT [dbo].[Tests] ([TestId], [Name], [Description]) VALUES (N'181007d1-4a73-4109-c3ac-08d9fcbbc438', N'Bài kiểm tra (test)', N'Bài kiểm tra')");

            migrationBuilder.Sql("INSERT [dbo].[Sections] ([SectionId], [Name], [TestId]) VALUES (N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4', N'Phần kiểm tra (test)', N'181007d1-4a73-4109-c3ac-08d9fcbbc438')");

            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'59f7a6e9-a3ee-46f6-565a-08d9fcbc6b45', N'Câu hỏi chọn một đáp án (test)', NULL, 1, N'{\"question\":\"Câu hỏi chọn một đáp án\",\"answers\":[{\"answer\":\"Cấu trả lời 1\",\"isCorrect\":false},{\"answer\":\"Cấu trả lời 2\",\"isCorrect\":false},{\"answer\":\"Cấu trả lời 3\",\"isCorrect\":false},{\"answer\":\"Cấu trả lời 4\",\"isCorrect\":true}]}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'5bda5889-7ef8-4bc1-565b-08d9fcbc6b45', N'Câu hỏi chọn nhiều đáp án (test)', NULL, 1, N'{\"question\":\"Câu hỏi chọn nhiều đáp án\",\"answers\":[{\"answer\":\"Cấu trả lời 1\",\"isCorrect\":false},{\"answer\":\"Cấu trả lời 2\",\"isCorrect\":false},{\"answer\":\"Cấu trả lời 3\",\"isCorrect\":true},{\"answer\":\"Cấu trả lời 4\",\"isCorrect\":true}]}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'4c98b72a-3e83-4df5-565c-08d9fcbc6b45', N'Cấu hỏi điền chỗ trống từ nhóm từ (test)', NULL, 2, N'{\"question\":\"blank_1(Đáp án 1) blank_2(Đáp án 2) blank_3(Đáp án 3) blank_4(Đáp án 4)\",\"isFromAPrivateCollection\":false}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'ebbb737a-0702-4e8b-565d-08d9fcbc6b45', N'Cấu hỏi điền chỗ trống (test)', NULL, 2, N'{\"question\":\"blank_1(Đáp án 1) blank_2(Đáp án 2) blank_3(Đáp án 3) blank_4(Đáp án 4)\",\"isFromAPrivateCollection\":true,\"blanks\":[{\"position\":\"blank_1\",\"answer\":\"Đáp án 1,Đáp án 1.1,Đáp án 1.2,Đáp án 1.3\"},{\"position\":\"blank_2\",\"answer\":\"Đáp án 2,Đáp án 2.1,Đáp án 2.2,Đáp án 2.3\"},{\"position\":\"blank_3\",\"answer\":\"Đáp án 3,Đáp án 3.1,Đáp án 3.2,Đáp án 3.3\"},{\"position\":\"blank_4\",\"answer\":\"Đáp án 4,Đáp án 4.1,Đáp án 4.2,Đáp án 4.3\"}]}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'503f5b0f-7c87-47bf-565e-08d9fcbc6b45', N'Cấu hỏi sắp xếp (test)', NULL, 3, N'{\"question\":\"Cấu hỏi sắp xếp\",\"answers\":[{\"answer\":\"Đáp án 1\",\"position\":\"1\"},{\"answer\":\"Đáp án 2\",\"position\":\"2\"},{\"answer\":\"Đáp án 3\",\"position\":\"3\"},{\"answer\":\"Đáp án 4\",\"position\":\"4\"},{\"answer\":\"Đáp án 5\",\"position\":\"5\"}]}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
            migrationBuilder.Sql("INSERT [dbo].[Questions] ([QuestionId], [Name], [Media], [Type], [ContentAsJson], [SectionId]) VALUES (N'330e8f1b-f693-41ab-565f-08d9fcbc6b45', N'Câu hỏi nối (test)', NULL, 4, N'{\"question\":\"Câu hỏi nối\",\"answers\":[{\"from\":\"Phần bắt đầu 1\",\"target\":\"Phần kết thúc 1\"},{\"from\":\"Phần bắt đầu 2\",\"target\":\"Phần kết thúc 2\"},{\"from\":\"Phần bắt đầu 3\",\"target\":\"Phần kết thúc 3\"},{\"from\":\"Phần bắt đầu 4\",\"target\":\"Phần kết thúc 4\"}]}', N'0ec8ba6d-e7ad-4796-4e53-08d9fcbc1fa4')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
