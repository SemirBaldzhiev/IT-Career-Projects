using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Data.Repositories;
using GamingCatalog.Models;

namespace GamingCatalog.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameDbContext _context;
        private readonly GameRepository _gameRepository;


        public GamesController(GameDbContext context)
        {
            _context = context;
            _gameRepository = new GameRepository(context);
        }

        // GET: Games
        public  IActionResult Index(string searchString, string gameManufacturer)
        {
            //var gameDbContext = _context.Games.Include(g => g.Manufacturer);
            var manufacturerList = new List<string>();
          
            var manufacturers = _context.Games.Select(g => g.Manufacturer.Name).ToList();

            manufacturerList.AddRange(manufacturers.Distinct());

            ViewBag.gameManufacturer = new SelectList(manufacturerList);

            var games = _gameRepository.GetAll().Include(g => g.Manufacturer).ToList();

           

            if (!String.IsNullOrEmpty(searchString))
            {
                games = _gameRepository.SearchByTittle(searchString).ToList();
            }
            if (!string.IsNullOrEmpty(gameManufacturer))
            {
                games = _gameRepository.SearchByManufacturer(gameManufacturer).ToList();
            }

            return View(games.ToList());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);

            game.Genres = _context.GameGenreConnection.Where(gc => gc.GameId == game.Id).Select(g => g.Genre).ToList();

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewBag.Genres = new MultiSelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Rating,ReleaseDate,Platform,Description,Image,ManufacturerId")] Game game,int[] Genres, IFormFile Image)
        {
            var modelState = ModelState.IsValid;

            ViewBag.ModelState = modelState;

            if (ModelState.IsValid)
            {
                if (Genres != null)
                {
                    foreach (var genreId in Genres)
                    {
                        var genre = _context.Genres.Find(genreId);

                        var gameGenre = new GameGenreConnection()
                        {
                            Game = game,
                            Genre = genre
                        };
                        _context.GameGenreConnection.Add(gameGenre);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    game.Image = stream.ToArray();
                }

                    _gameRepository.Add(game);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", game.ManufacturerId);
            return RedirectToAction("Create", "Games");
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            //game.Genres = _context.GameGenreConnection.Where(gc => gc.GameId != game.Id).Select(g => g.Genre).ToList();

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", game.ManufacturerId);
            ViewBag.Genres = new MultiSelectList(_context.Genres, "Id", "Name");
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Rating,ReleaseDate,Platform,Description,Image,ManufacturerId")] Game game, int[] Genres, IFormFile Image)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Genres != null)
                    {
                        var gameGenres = _context.GameGenreConnection.Where(g => g.GameId == id).ToList();

                        var genersId = gameGenres.Select(g => g.GenreId).ToList();
                        foreach (var genreId in Genres)
                        {
                            if (!genersId.Contains(genreId))
                            {
                                var genre = _context.Genres.Find(genreId);

                                var gameGenre = new GameGenreConnection()
                                {
                                    Game = game,
                                    Genre = genre
                                };

                                _context.GameGenreConnection.Update(gameGenre);
                            }
                            else
                            {
                                //this genre already exist
                            }
                        }
                    }
                    using (var stream = new MemoryStream())
                    {
                        await Image.CopyToAsync(stream);
                        game.Image = stream.ToArray();
                    }

                    _gameRepository.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", game.ManufacturerId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetAll()
                .Include(g => g.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);

            game.Genres = _context.GameGenreConnection.Where(gc => gc.GameId == game.Id).Select(g => g.Genre).ToList();
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var game =  _gameRepository.GetById(id);

            var gemeGenres = _context.GameGenreConnection.Where(g => g.GameId == game.Id).ToList();

            foreach (var item in gemeGenres)
            {
                _context.GameGenreConnection.Remove(item);
            }

            _gameRepository.Remove(game);
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
