using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc20201107.Models
{
    public class Guesbooks
    {


        [DisplayName("編號:")]

        public int Id { get; set; }

        [DisplayName("名字:")]
        [Required(ErrorMessage ="請輸入名字")]
        [StringLength(20, ErrorMessage ="名子不可以超過20字元")]
        public string Name { get; set; }



        [DisplayName("留言內容:")]
        [Required(ErrorMessage = "請輸入內容")]
        [StringLength(100, ErrorMessage = "留言內容不可以超過100字元")]
        public string Content { get; set; }

        [DisplayName("新增時間:")]
        public DateTime CreateTime { get; set; }
        [DisplayName("回覆內容:")]
        [StringLength(100, ErrorMessage = "留言內容不可以超過100字元")]
        public string Reply { get; set; }

        [DisplayName("回覆時間:")]
        public DateTime? ReplyTime { get; set; }




    }
}