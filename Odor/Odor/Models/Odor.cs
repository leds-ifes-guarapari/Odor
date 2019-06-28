﻿using System;

namespace Odor.Models
{
    /*
     *
     * Default odor class data.
     * 
     */
    /// <summary>
    /// Default odor class data.
    /// </summary>
    public class Odor
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Intensity { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Begin { get; set; }
        public TimeSpan End { get; set; }
    }
}