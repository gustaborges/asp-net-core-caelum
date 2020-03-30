using CaelumBlog.Infra.DAO;
using CaelumBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CaelumBlog.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    //[AllowAnonymous]
    public class PostController : Controller
    {

        private PostDAO postDAO;

        #region Construtor

        public PostController(PostDAO postDAO)
        {
            this.postDAO = postDAO;
        }

        #endregion

        #region Controller Index

        public IActionResult Index()
        {
            //ViewBag.Posts = postDAO.GetPosts();
            IList<Post> listaFilmes= postDAO.GetPosts();
            return View(listaFilmes);
        }

        #endregion

        #region Controller AdicionaPostagem
        // Controller das requisições da view AdicionaFilme
        [HttpGet]
        public IActionResult AdicionaPostagem()
        {
            var model = new Post();
            return View(model);
        }

        [HttpPost] 
        public IActionResult AdicionaPostagem(Post post)
        {
            if (ModelState.IsValid)
            {
                postDAO.Adiciona(post);
                return RedirectToAction("Index");
            }
            else
                return View("AdicionaPostagem", post);
        }
        #endregion

        #region Controller VisualizaPost

        public IActionResult VisualizaPost(int id)
        {
            Post post = postDAO.BuscaPorId(id);
            return View(post);
        }

        #endregion

        #region Action Categoria

        [HttpGet]
        public IActionResult Categoria( [Bind(Prefix = "id")] string categoria)
        {
            var listaFiltrada = postDAO.FiltraPorCategoria(categoria);
            return View("Index", model: listaFiltrada);
        }

        #endregion

        #region Action PublicaPost

        public IActionResult PublicaPost(int id)
        {
            postDAO.Publica(id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Action DeletarPost

        public IActionResult DeletarPost(int id)
        {
            postDAO.Deleta(id);
            return RedirectToAction("Index");
        }

        #endregion
               
        #region Action EditaPost

        public IActionResult EditaPost(Post post)
        {
            if (ModelState.IsValid)
            {
                postDAO.SalvaAlteracoes(post);
                return RedirectToAction("Index");
            }
            else
                return View("VisualizaPost", post);
        }

        #endregion

        #region Action CategoriaAutocomplete

        [HttpPost]
        public IActionResult CategoriaAutocomplete(string termoDigitado)
        {
            var model = postDAO.ListaCategoriasQueContemTermo(termoDigitado);
            return Json(model);
        }

        #endregion

    }
}
