﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dianzhu.Model;

namespace Dianzhu.ApplicationService
{
    public class ClientDTO
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        publicApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}
