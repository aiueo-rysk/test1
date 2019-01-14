using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressManagement.Models;
using AddressManagement.Data;
using Microsoft.AspNetCore.Authorization;

namespace AddressManagement.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AddressesController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 住所一覧画面表示
        /// </summary>
        /// <returns>住所情報一覧画面</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userID = User.Identity.Name;
            return View(await _context.Address.Where(x => x.UserID.Equals(userID)).ToListAsync());
        }

        /// <summary>
        /// 検索処理を行い、一覧画面へ遷移
        /// </summary>
        /// <returns>住所情報一覧画面</returns>
        [HttpPost]
        public async Task<IActionResult> Search(string searchTitle,
                                                string searchPostalCode,
                                                string searchPrefectures,
                                                string searchCtiy,
                                                string searchBlock,
                                                string searchBuilding,
                                                string searchRemarks)
        {
            var userID = User.Identity.Name;
            var query = _context.Address.Where(x => x.UserID.Equals(userID));

            // 検索条件を反映
            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                ViewData["searchTitle"] = searchTitle;
                query = query.Where(x => x.Title.Contains(searchTitle));
            }
            if (!string.IsNullOrWhiteSpace(searchPostalCode))
            {
                ViewData["searchPostalCode"] = searchPostalCode;
                query = query.Where(x => x.PostalCode.ToString().Contains(searchPostalCode));
            }
            if (!string.IsNullOrWhiteSpace(searchPrefectures))
            {
                ViewData["searchPrefectures"] = searchPrefectures;
                query = query.Where(x => x.Prefectures.Contains(searchPrefectures));
            }
            if (!string.IsNullOrWhiteSpace(searchCtiy))
            {
                ViewData["searchCtiy"] = searchCtiy;
                query = query.Where(x => x.Ctiy.Contains(searchCtiy));
            }
            if (!string.IsNullOrWhiteSpace(searchBlock))
            {
                ViewData["searchBlock"] = searchBlock;
                query = query.Where(x => x.Block.Contains(searchBlock));
            }
            if (!string.IsNullOrWhiteSpace(searchBuilding))
            {
                ViewData["searchBuilding"] = searchBuilding;
                query = query.Where(x => x.Building.Contains(searchBuilding));
            }
            if (!string.IsNullOrWhiteSpace(searchRemarks))
            {
                ViewData["searchRemarks"] = searchRemarks;
                query = query.Where(x => x.Remarks.Contains(searchRemarks));
            }

            // 並び替え
            query = query.OrderByDescending(x => x.UpdateTime);

            return View("Index", await query.ToListAsync());
        }


        /// <summary>
        /// 詳細画面表示
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns>住所情報詳細画面</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .SingleOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        /// <summary>
        /// 登録画面表示
        /// </summary>
        /// <returns>住所情報登録画面</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 住所情報を登録して一覧画面へ遷移
        /// </summary>
        /// <param name="address">住所情報オブジェクト</param>
        /// <returns>住所一覧画面</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,PostalCode,Prefectures,Ctiy,Block,Building,Remarks")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.UserID = User.Identity.Name;
                address.RegistrationTime = address.UpdateTime = DateTime.Now;

                // 登録
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        /// <summary>
        /// 更新画面表示
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns>住所情報更新画面</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        /// <summary>
        /// 住所情報を更新して一覧画面へ遷移
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <param name="address">住所情報オブジェクト</param>
        /// <returns>住所一覧画面</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AddressID,Title,PostalCode,Prefectures,Ctiy,Block,Building,Remarks,UserID,RegistrationTime,UpdateTime")] Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    address.UpdateTime = DateTime.Now;

                    // 更新
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        /// <summary>
        /// 削除確認画面を表示
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns>削除確認画面</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .SingleOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        /// <summary>
        /// 住所情報を削除して一覧画面へ遷移
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns>住所一覧画面</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("AddressID")] int addressID)
        {
            var address = await _context.Address.SingleOrDefaultAsync(m => m.AddressID == addressID);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressID == id);
        }
    }
}
