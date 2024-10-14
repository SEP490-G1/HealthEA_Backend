﻿using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HealthProfileOutput
    {
        public Guid? PantientId { get; set; }
        public Guid? UserId { get; set; }
        public string? ProfileCode { get; set; }
        public string? FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Residence { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public int? SharedStatus { get; set; }

    }
}
