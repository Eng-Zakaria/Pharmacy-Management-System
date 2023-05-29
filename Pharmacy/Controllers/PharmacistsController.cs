using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class PharmacistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PharmacistsController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pharmacists
        public async Task<IActionResult> Index()
        {
           // Try Using Guid for FullName

            return View(await _context.Pharmacists.ToListAsync());
        }

        // GET: Pharmacists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pharmacists == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacists.FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // GET: Pharmacists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pharmacists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Salary,CreditCard,HiringDate,LastUpdatedAt,Attendance,PhoneNo,Email,ConfirmEmail,Password,ConfirmPassword,Age,ImageUrl")] Pharmacist pharmacist, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile == null)
                {
                    pharmacist.ImageUrl = "\\images\\No_Image.png";
                }
                else
                {
                    string imgExtension = Path.GetExtension(imageFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgUrl = "\\images\\" + imgName;

                    pharmacist.ImageUrl = imgUrl;
                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFile.CopyTo(imgStream);
                    imgStream.Dispose();

                }

                pharmacist.AddedAt = pharmacist.LastUpdatedAt = DateTime.Now;
                _context.Add(pharmacist);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pharmacist);
        }

        // GET: Pharmacists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pharmacists == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // POST: Pharmacists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Salary,CreditCard,HiringDate,LastUpdatedAt,Attendance,PhoneNo,Email,ConfirmEmail,Password,ConfirmPassword,Age,ImageUrl")] Pharmacist pharmacist, IFormFile? imageFile)
        {
            if (id != pharmacist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    if (pharmacist.ImageUrl != "\\images\\No_Image.png")
                    {
                        string oldPath = _webHostEnvironment.WebRootPath + pharmacist.ImageUrl;
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }

                        string imgExtension = Path.GetExtension(imageFile.FileName);
                        Guid imgGuid = Guid.NewGuid();
                        string imgName = imgGuid + imgExtension;
                        string imgUrl = "\\images" + imgName;

                        pharmacist.ImageUrl = imgUrl;
                        string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                        FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                        imageFile.CopyTo(imgStream);
                        imgStream.Dispose();

                    }
                }
                try
                {
                    pharmacist.LastUpdatedAt = DateTime.Now;
                    _context.Update(pharmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistExists(pharmacist.Id))
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


            return View(pharmacist);
        }

        // GET: Pharmacists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pharmacists == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // POST: Pharmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pharmacists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pharmacists'  is null.");
            }
            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist != null)
            {
                if (pharmacist.ImageUrl != "\\images\\No_Image.png")
                {
                    string imgPath = _webHostEnvironment.WebRootPath + pharmacist.ImageUrl;
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }

                }
                _context.Pharmacists.Remove(pharmacist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacistExists(int id)
        {
          return _context.Pharmacists.Any(e => e.Id == id);
        }
    }
}
