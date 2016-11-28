using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
namespace DAL
{
    public class DataInitializer
    {
        public static void Seed(AppDbContext context, User user)
        {
            

            var category1 = new QuestionCategory()
            {
                Name = "Fruits and vegetables",
                OwnerId = user.Id
            };

            var category2 = new QuestionCategory()
            {
                Name = "Vegetables",
                Parent = category1,
                OwnerId = user.Id
            };

            var category3 = new QuestionCategory()
            {
                Name = "Fruits",
                Parent = category1,
                OwnerId = user.Id
            };

            var question1 = new Question()
            {
                Text = "Select only fruits.",
                OwnerId = user.Id,
                Multiple = true,
                QuestionCategory = category3
            };
            var answer1 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Banana",
                Score = 1,
                Note = "",
                Question = question1
            };
            var answer2 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Onion",
                Score = -0.5,
                Note = "",
                Question = question1
            };
            var answer3 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Carrot",
                Score = -0.5,
                Note = "",
                Question = question1
            };
            var answer4 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Apple",
                Score = 1,
                Note = "",
                Question = question1
            };

            var question2 = new Question()
            {
                Text = "Select only vegetable.",
                QuestionCategory = category2,
                OwnerId = user.Id,
                Multiple = true
            };

            var answer5 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Zucchini",
                Score = 2,
                Note = "",
                Question = question2
            };

            var answer6 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Eggplant",
                Score = 2,
                Note = "",
                Question = question2
            };

            var answer7 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Avocado",
                Score = -0.5,
                Note = "",
                Question = question2
            };

            var answer8 = new Answer()
            {
                OwnerId = user.Id,
                Text = "lychee",
                Score = -0.5,
                Note = "",
                Question = question2
            };

            var question3 = new Question()
            {
                Text = "Does Pitaya refer to genus of name Neoporteria?",
                OwnerId = user.Id,
                Multiple = false,
                QuestionCategory = category3
            };

            var answer9 = new Answer()
            {
                OwnerId = user.Id,
                Text = "yes",
                Score = -0.5,
                Note = "",
                Question = question3
            };

            var answer10 = new Answer()
            {
                OwnerId = user.Id,
                Text = "no",
                Score = 1,
                Note = "Pitaya refer to Stenocereus genus",
                Question = question3
            };

            var question4 = new Question()
            {
                Text = "Select lychee countries of origin",
                OwnerId = user.Id,
                Multiple = true,
                QuestionCategory = category3
            };

            var answer11 = new Answer()
            {
                OwnerId = user.Id,
                Text = "China",
                Score = 1,
                Note = "",
                Question = question4
            };

            var answer12 = new Answer()
            {
                OwnerId = user.Id,
                Text = "India",
                Score = -0.5,
                Note = "",
                Question = question4
            };

            var answer13 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Indochina",
                Score = 1,
                Note = "",
                Question = question4
            };

            var answer14 = new Answer()
            {
                OwnerId = user.Id,
                Text = "Thailand",
                Score = -0.5,
                Note = "",
                Question = question4
            };

            var question5 = new Question()
            {
                Text = "Pumpkin is vegetable.",
                QuestionCategory = category2,
                OwnerId = user.Id,
                Multiple = false
            };

            var answer15 = new Answer()
            {
                OwnerId = user.Id,
                Text = "yes",
                Score = 0.5,
                Note = "",
                Question = question5
            };

            var answer16 = new Answer()
            {
                OwnerId = user.Id,
                Text = "no",
                Score = -0.5,
                Note = "",
                Question = question5
            };

            var question6 = new Question()
            {
                Text = "Parsley is vegetable.",
                QuestionCategory = category2,
                OwnerId = user.Id,
                Multiple = false
            };

            var answer17 = new Answer()
            {
                OwnerId = user.Id,
                Text = "yes",
                Score = 0.5,
                Note = "",
                Question = question6
            };

            var answer18 = new Answer()
            {
                OwnerId = user.Id,
                Text = "no",
                Score = -0.5,
                Note = "",
                Question = question6
            };

            var question7 = new Question()
            {
                Text = "Apricot is vegetable.",
                QuestionCategory = category2,
                OwnerId = user.Id,
                Multiple = false
            };

            var answer19 = new Answer()
            {
                OwnerId = user.Id,
                Text = "yes",
                Score = -0.5,
                Note = "",
                Question = question7
            };

            var answer20 = new Answer()
            {
                OwnerId = user.Id,
                Text = "no",
                Score = 0.5,
                Note = "",
                Question = question7
            };



            var group1 = new Group()
            {
                OwnerId = user.Id,
                Name = "Fruits and vegetables",
                AuthCode = "d23tg3t"
            };

            var template1 = new TestTemplate()
            {
                OwnerId = user.Id,
                Name = "Vegetables exercise",
                Time = 5,
                NumQuestions = 4,
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date.AddDays(7),
                Group = group1,
                Attempts = 0,
                QuestionCategories = new List<QuestionCategory>( new QuestionCategory[1]{ category2 })
            };

            var template2 = new TestTemplate()
            {
                OwnerId = user.Id,
                Name = "Fruits exercise",
                Time = 5,
                NumQuestions = 3,
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date.AddDays(7),
                Group = group1,
                Attempts = 0,
                QuestionCategories = new List<QuestionCategory>(new QuestionCategory[1] { category3 })
            };

            var template3 = new TestTemplate()
            {
                OwnerId = user.Id,
                Name = "Fruits and vegetables final test",
                Time = 5,
                NumQuestions = 7,
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date.AddDays(7),
                Group = group1,
                Attempts = 1,
                QuestionCategories = new List<QuestionCategory>(new QuestionCategory[1] { category1 })
            };

            var template4 = new TestTemplate()
            {
                OwnerId = user.Id,
                Name = "Fruits and vegetables final test (resit)",
                Time = 5,
                NumQuestions = 7,
                DateFrom = DateTime.Now.Date.AddDays(3),
                DateTo = DateTime.Now.Date.AddDays(7),
                Group = group1,
                Attempts = 1,
                QuestionCategories = new List<QuestionCategory>(new QuestionCategory[1] { category1 })
            };

            context.QuestionCategories.Add(category1);
            context.QuestionCategories.Add(category2);
            context.QuestionCategories.Add(category3);
            context.Questions.Add(question1);
            context.Questions.Add(question2);
            context.Questions.Add(question3);
            context.Questions.Add(question4);
            context.Questions.Add(question5);
            context.Questions.Add(question6);
            context.Questions.Add(question7);
            context.Answers.Add(answer1);
            context.Answers.Add(answer2);
            context.Answers.Add(answer3);
            context.Answers.Add(answer4);
            context.Answers.Add(answer5);
            context.Answers.Add(answer6);
            context.Answers.Add(answer7);
            context.Answers.Add(answer8);
            context.Answers.Add(answer9);
            context.Answers.Add(answer10);
            context.Answers.Add(answer11);
            context.Answers.Add(answer12);
            context.Answers.Add(answer13);
            context.Answers.Add(answer14);
            context.Answers.Add(answer15);
            context.Answers.Add(answer16);
            context.Answers.Add(answer17);
            context.Answers.Add(answer18);
            context.Answers.Add(answer19);
            context.Answers.Add(answer20);
            context.Groups.Add(group1);
            user.Groups.Add(group1);
            context.TestTemplates.Add(template1);
            context.TestTemplates.Add(template2);
            context.TestTemplates.Add(template3);
            context.TestTemplates.Add(template4);
            context.SaveChanges();
        }
    }
}
