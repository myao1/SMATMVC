using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMATMVC.Models;

namespace SMATMVC.Controllers
{
    public class SiteInfoController : Controller
    {
        private readonly SiteInfoContext _context;
        

        public SiteInfoController(SiteInfoContext context)
        {
            _context = context;
        }

        // GET: SiteInfo
        [GenerateAntiforgeryTokenCookieForAjax]
        public async Task<IActionResult> Index()
        {
            return View(await _context.SiteInfo.ToListAsync());
        }

        // GET: SiteInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteInfo = await _context.SiteInfo
                .SingleOrDefaultAsync(m => m.ID == id);
            if (siteInfo == null)
            {
                return NotFound();
            }

            return View(siteInfo);
        }

        // GET: SiteInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WebUrl,SiteUrl,Creator,SiteCollectionAdmins,Owners,LastItemModifiedDate,ProposedDisposition")] SiteInfo siteInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteInfo);
        }

        // GET: SiteInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Disposition = new List<SMATEnums.Disposition>
            {
                SMATEnums.Disposition.Delete,
                SMATEnums.Disposition.Keep
            };

            var siteInfo = await _context.SiteInfo.SingleOrDefaultAsync(m => m.ID == id);
            if (siteInfo == null)
            {
                return NotFound();
            }
            return View(siteInfo);
        }

        // POST: SiteInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WebUrl,SiteUrl,Creator,SiteCollectionAdmins,Owners,LastItemModifiedDate,ProposedDisposition")] SiteInfo siteInfo)
        {
            if (id != siteInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteInfoExists(siteInfo.ID))
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
            return View(siteInfo);
        }

        // GET: SiteInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteInfo = await _context.SiteInfo
                .SingleOrDefaultAsync(m => m.ID == id);
            if (siteInfo == null)
            {
                return NotFound();
            }

            return View(siteInfo);
        }

        // POST: SiteInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteInfo = await _context.SiteInfo.SingleOrDefaultAsync(m => m.ID == id);
            _context.SiteInfo.Remove(siteInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload()
        {}

        private bool SiteInfoExists(int id)
        {
            return _context.SiteInfo.Any(e => e.ID == id);
        }
    }

    public class GenerateAntiforgeryTokenCookieForAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context){
            var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();
            var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
            context.HttpContext.Response.Cookies.Append(
                "XSRF-TOKEN",
                tokens.RequestToken,
                new CookieOptions() {HttpOnly = false}
            );
        }
    }
}
