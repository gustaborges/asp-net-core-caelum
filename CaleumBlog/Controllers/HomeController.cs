using CaelumBlog.Infra.DAO;
using CaelumBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaelumBlog.Controllers
{
    public class HomeController : Controller
    {
        private PostDAO postDAO;

        public HomeController(PostDAO postDAO)
        {
            this.postDAO = postDAO;
        }
         
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            SetViewBagCategorias(); // O ViewBag será setado na execução de todas as actions
        }

        public IActionResult Index()
        {
            IList<Post> publicados = postDAO.GetPosts(somentePublicados: true);
            return View(publicados);
        }

        public IActionResult Busca(string termo)
        {
            if (termoInvalido(termo)) return RedirectToAction("Index");

            SetViewBagTermoBuscado(termo);
            IList<Post> resultadosBusca = postDAO.BuscaPeloTermo(termo);
            return View("Index", resultadosBusca);
        }


        public IActionResult Categoria([Bind(Prefix="id")]string categoria)
        {
            IList<Post> listaPostsCategoria = postDAO.FiltraPorCategoria(categoria);
            return View("Index", listaPostsCategoria);
        }


        #region Auxiliar Methods

        private void SetViewBagCategorias()
        {
            ViewBag.Categorias = postDAO.ListaCategorias();
        }

        private void SetViewBagTermoBuscado(string termo)
        {
            ViewBag.TermoBuscado = termo;
        }

        private bool termoInvalido(string termo)
        {
            return String.IsNullOrEmpty(termo);
        }
        #endregion
    }
}
