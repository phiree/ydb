using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminAgent.Models
{
 

    public class AddNoticeModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "正文")]
        public string Body { get; set; }
    }
}
