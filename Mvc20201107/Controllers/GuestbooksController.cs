using Mvc20201107.Models;
using Mvc20201107.Services;
using Mvc20201107.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc20201107.Controllers
{
    public class GuestbooksController : Controller
    {
        private readonly GuestbooksDBService GuestbooksService = new GuestbooksDBService();



        // GET: Guestbooks
        public ActionResult Index(string Search,int Page=1)
        {
            GuestbooksViewModel Data = new GuestbooksViewModel();
            Data.Search = Search;
            Data.Paging = new ForPaging(Page);
            Data.DataList = GuestbooksService.GetDataList(Data.Paging, Data.Search);
            return View(Data);
        }






        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="Name,Content")] Guesbooks Data)
        {
            GuestbooksService.InSertGuestbooks(Data);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            Guesbooks Data = GuestbooksService.GetDataById(Id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult Edit(int Id, [Bind(Include ="Name,Content")] Guesbooks UpdateDate)
        {
            if (GuestbooksService.CheckUpdate(Id))
            {
                UpdateDate.Id = Id;
                GuestbooksService.UpdateGuestboooks(UpdateDate);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Reply(int Id)
        {
            Guesbooks Data = GuestbooksService.GetDataById(Id);
            return View(Data);
        }

        [HttpPost]
        public ActionResult Reply(int Id, [Bind(Include ="Reply,ReplyTime")] Guesbooks ReplyData)
        {
            if (GuestbooksService.CheckUpdate(Id))
            {
                ReplyData.Id = Id;
                GuestbooksService.ReplyGuestbooks(ReplyData);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(int Id)
        {
            GuestbooksService.DeleteGuestbooks(Id);
            return RedirectToAction("Index");
        }
       
   










    }
}