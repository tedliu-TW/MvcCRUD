using Mvc20201107.Models;
using Mvc20201107.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc20201107.ViewModels
{
    public class GuestbooksViewModel
    {
        [DisplayName("收尋")]
        public string Search { get; set; }
        public List<Guesbooks> DataList { get; set; }

        public ForPaging Paging { get; set; }
        


        //public List<Guesbooks> DataList { get; set; }


    }
}