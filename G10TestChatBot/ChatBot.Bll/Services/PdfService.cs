using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Dal;
using ChatBot.Dal.Entites;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ChatBot.Bll.Services
{
    public class PdfService : IFileService
    {
        private readonly MainContext _mainContext;

        public PdfService(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<byte[]> GenerateCVAsync(long botUserId)
        {
            var user = await _mainContext.Users.FindAsync(botUserId);
            if (user == null)
                throw new Exception("User not found");

            var userInfo = _mainContext.UserInfos.FirstOrDefault(u => u.BotUserId == botUserId);

            if(userInfo == null)
            {

                return null;
            }

            var experiences = _mainContext.Experiences.Where(e => e.UserInfoId == userInfo.UserInfoId).ToList();
            var educations = _mainContext.Educations.Where(e => e.UserInfoId == userInfo.UserInfoId).ToList();
            var skills = _mainContext.Skills.Where(s => s.UserInfoId == userInfo.UserInfoId).ToList();

            using (var memoryStream = new MemoryStream())
            {
                using (var pdfWriter = new PdfWriter(memoryStream))
                using (var pdfDocument = new PdfDocument(pdfWriter))
                using (var document = new Document(pdfDocument))
                {
                    document.Add(new Paragraph(userInfo.FirstName).SetFontSize(18));
                    document.Add(new Paragraph(userInfo?.Summary ?? "No summary available"));
                    document.Add(new Paragraph("\nExperience"));
                    foreach (var exp in experiences)
                    {
                        document.Add(new Paragraph($"{exp.Position} at {exp.Company} ({exp.StartDate:yyyy-MM} - {exp.EndDate:yyyy-MM})"));
                    }
                    document.Add(new Paragraph("\nEducation"));
                    foreach (var edu in educations)
                    {
                        document.Add(new Paragraph($"{edu.Degree} at {edu.Institution} ({edu.StartDate:yyyy-MM} - {edu.EndDate:yyyy-MM})"));
                    }
                    document.Add(new Paragraph("\nSkills"));
                    document.Add(new Paragraph(string.Join(", ", skills.Select(s => s.Name))));
                }
                return memoryStream.ToArray();
            }
        }
    }
}
