using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.Model.Core.Domains
{
    public class Result
    {
        public int Grade { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public AppUser User { get; set; }
        public Exam Exam { get; set; }
    }
}
