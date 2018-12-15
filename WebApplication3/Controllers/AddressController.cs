using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.AddressViewModels;

namespace WebApplication3.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AddressController : Controller
    {

        private IConfiguration configuration;
        private DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_configuration"></param>
        public AddressController(IConfiguration _configuration)
        {
            configuration = _configuration;
            optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// 住所一覧検索画面を初期表示する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddressList()
        {
            var model = new AddressListViewModel();
            var userID = User.Identity.Name;
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                ViewData["DataCount"] = model.Search(userID, context);
            }
  
            return View(model);
        }

        /// <summary>
        /// 検索して一覧表示を更新する
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddressList(AddressListViewModel model)
        {
            var userID = User.Identity.Name;
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                ViewData["DataCount"] = model.Search(userID, context);
            }
            return View(model);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="model"></param>
        [HttpGet]
        public IActionResult Delete(AddressListViewModel model, int id)
        {
            var userID = User.Identity.Name;
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                model.Delete(id, context);
                model.Search(userID, context);
            }
            
            return View("AddressList", model);
        }

        /// <summary>
        /// 登録更新画面の表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Address(int? id)
        {
            var model = new AddressViewModel();
            if (id.HasValue)
            {
                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    model.SetData(id.Value, context);
                }               
            }
            return View(model);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public IActionResult Insert(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var userID = User.Identity.Name;
                var now = DateTime.Now;
                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    model.Insert(userID, now, context);
                }      
            }
            return View("Address", model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public IActionResult Update(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var now = DateTime.Now;
                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    model.Update(now, context);
                }
            }
            return View("Address", model);
        }

    }
}
