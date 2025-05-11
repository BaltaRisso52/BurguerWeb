using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BurguerWeb.Models;

namespace BurguerWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoriaRepository categoriaRepository;

    public HomeController(ILogger<HomeController> logger, ICategoriaRepository categoriaRepository)
    {
        _logger = logger;
        this.categoriaRepository = categoriaRepository;
    }

    public IActionResult Index()
    {
        List<Categoria> categorias = categoriaRepository.ListarCategorias();
        
        return View(categorias);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
