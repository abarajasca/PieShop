using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository,ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //public ViewResult List()
        //{
        //    PieListViewModel pieListViewModel = new PieListViewModel();

        //    pieListViewModel.Pies = _pieRepository.AllPies;
        //    pieListViewModel.CurrentCategory = "Chees cakes";

        //    return View(pieListViewModel);
        //}

        public ViewResult List(string category)
        {
            PieListViewModel pieListViewModel = new PieListViewModel();

            if (category != null)
            {
                pieListViewModel.Pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category);
                pieListViewModel.CurrentCategory = category;
            } else
            {
                pieListViewModel.Pies = _pieRepository.AllPies;
                pieListViewModel.CurrentCategory = "All Pies";
            }

            return View(pieListViewModel);
        }

        public IActionResult Details(int id)
        {
            Pie pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
    }
}
