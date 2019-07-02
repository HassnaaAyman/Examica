using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.Model.Core.Domains
{
    public class Answer
    {
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string AppUserId { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public AppUser AppUser { get; set; }
    }
}
