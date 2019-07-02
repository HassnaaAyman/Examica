using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class ExamAppUser
    {
        public string AppUserId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public AppUser AppUser { get; set; }
    }
}
