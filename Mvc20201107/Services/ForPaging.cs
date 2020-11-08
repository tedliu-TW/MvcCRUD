﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc20201107.Services
{
    public class ForPaging
    {
        public int NowPage { get; set; }

        public int MaxPage { get; set; }

        public int ItemNum
        {
            get
            {
                return 5;
            }
        }

        public ForPaging()
        {
            this.NowPage = 1;
        }
        public ForPaging(int Page)
        {
            this.NowPage = Page;
        }
        public void SetReightPage()
        {
            if (this.NowPage <1)
            {
                this.NowPage = 1;
            }
            else if(this.NowPage > this.MaxPage)
            {
                this.NowPage = this.MaxPage;
            }

            if (this.MaxPage.Equals(0))
            {
                this.NowPage = 1;
            }

        }

        


    }
}