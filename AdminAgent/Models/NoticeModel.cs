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
        [Display(Name = "电子邮件")]
        public string Body { get; set; }
    }
}
