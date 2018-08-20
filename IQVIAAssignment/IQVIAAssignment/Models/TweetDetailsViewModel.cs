﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQVIAAssignment.Models
{
    public class TweetDetailsViewModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }

        public  TimeSpan Time { get; set; }
        public string Description { get; set; }
    }
}