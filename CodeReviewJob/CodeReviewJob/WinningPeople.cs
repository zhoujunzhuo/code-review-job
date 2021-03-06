﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReviewJob
{
    public class WinningPeople
    {
        public Department Department { get; set; }
        public DateTime ReviewDate { get; set; }
        public string CodeReviewId { get; set; }
        public string UpdateCodeId { get; set; }
        public string CodeReviewName { get; set; }
        public string UpdateCodeName { get; set; }
    }

    public enum Department
    {
        Internal,
        Client,
        Affiliate
    }
}

