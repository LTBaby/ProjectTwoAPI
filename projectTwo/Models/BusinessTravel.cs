﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class BusinessTravel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
