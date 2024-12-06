    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using CosmeticHub.Models;

    namespace CosmeticHub.Controllers
    {
        public class ReviewsController : Controller
        {
            private readonly MyDbContext _context;

            public ReviewsController(MyDbContext context)
            {
                _context = context;
            }

            // GET: Reviews
            public async Task<IActionResult> Index()
            {
                return View(await _context.Reviews.ToListAsync());
            }

            // GET: Reviews/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var review = await _context.Reviews
                    .FirstOrDefaultAsync(m => m.ReviewId == id);
                if (review == null)
                {
                    return NotFound();
                }

                return View(review);
            }

            // GET: Reviews/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Reviews/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ReviewId,UserId,ProductId,Rating,Comment,CreatedAt")] Review review)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(review);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(review);
            }

            // GET: Reviews/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var review = await _context.Reviews.FindAsync(id);
                if (review == null)
                {
                    return NotFound();
                }
                return View(review);
            }

            // POST: Reviews/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("ReviewId,UserId,ProductId,Rating,Comment,CreatedAt")] Review review)
            {
                if (id != review.ReviewId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(review);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReviewExists(review.ReviewId))
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
                return View(review);
            }

            // GET: Reviews/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var review = await _context.Reviews
                    .FirstOrDefaultAsync(m => m.ReviewId == id);
                if (review == null)
                {
                    return NotFound();
                }

                return View(review);
            }

            // POST: Reviews/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var review = await _context.Reviews.FindAsync(id);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ReviewExists(int id)
            {
                return _context.Reviews.Any(e => e.ReviewId == id);
            }


            // GET: Display all reviews for the logged-in user
            public IActionResult DisplayReviews()
            {
                // Retrieve UserId from session
                var userId = HttpContext.Session.GetInt32("UserID");

                if (userId == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }

                // Fetch reviews for the logged-in user
                var reviews = _context.Reviews
                                      .Where(r => r.UserId == userId)
                                      .OrderByDescending(r => r.CreatedAt)
                                      .ToList();

                return View(reviews);
            }


            public IActionResult AddReview(int productId)
            {
                // Retrieve UserId from session
                var userId = HttpContext.Session.GetInt32("UserID");

                // If UserId is not found, redirect to login page
                if (userId == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }

                // Pass ProductId and UserId to the view
                ViewBag.ProductId = productId;
                ViewBag.UserId = userId;

                return View();
            }

            // Action to handle the review submission
            [HttpPost]
            public IActionResult SubmitReview(int productId, int userId, int rating, string comment)
            {
                var review = new Review
                {
                    ProductId = productId,
                    UserId = userId,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.Now
                };

                _context.Reviews.Add(review);
                _context.SaveChanges();

                return RedirectToAction("DisplayReviews");
            }



            public IActionResult BulkDeletionReviews()
            {
                var reviews = _context.Reviews.ToList();
                return View(reviews);
            }

            // Handle bulk deletion of selected reviews
            [HttpPost]
            public IActionResult BulkDeleteReviews(List<int> reviewIds)
            {
                if (reviewIds == null || !reviewIds.Any())
                {
                    return BadRequest("No reviews selected for deletion.");
                }

                var reviewsToDelete = _context.Reviews
                    .Where(r => reviewIds.Contains(r.ReviewId))
                    .ToList();

                if (reviewsToDelete.Any())
                {
                    _context.Reviews.RemoveRange(reviewsToDelete);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index"); // Redirect after deletion
            }







        }
    }
