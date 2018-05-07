using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace APM.WebAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be atleast 2 chars long")]
        [MaxLength(20, ErrorMessage = "Name must be atmost 20 chars long")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Description must be atleast 2 chars long")]
        [MaxLength(20, ErrorMessage = "Description must be atmost 20 chars long")]
        public string Description { get; set; }

        public Nullable<int> StatusId { get; set; }

        public Nullable<bool> isDeleted { get; set; }

    }
}