using CaelumBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CaelumBlog.Infra.DAO
{
    public class PostDAO
    {
        private BlogContext contexto;

        public PostDAO(BlogContext context) // A conexão agora é injetada por quem precisar usar
        {
            this.contexto = context;
        }

        public List<Post> GetPosts()
        {
            return GetPosts(somentePublicados: false);
        }

        public List<Post> GetPosts(bool somentePublicados)
        {
            var listaDePosts = somentePublicados ?
                contexto.Posts.Where(p => p.Publicado).ToList() :
                contexto.Posts.ToList();
            return listaDePosts;

        }

        internal IList<Post> BuscaPeloTermo(string termo)
        {
            var listaDePosts = contexto.Posts
                .Where(p => p.Titulo.Contains(termo) || p.Resumo.Contains(termo))
                .ToList();

            return listaDePosts;

        }

        public void Adiciona(Post post)
        {
            contexto.Posts.Add(post);
            contexto.SaveChanges();
        }

        public IList<Post> FiltraPorCategoria(string categoria)
        {
            IList<Post> listaFiltrada = contexto.Posts.Where(postagem => postagem.Categoria.Contains(categoria)).ToList();
            return listaFiltrada;

        }

        public void SalvaAlteracoes(Post post)
        {
            contexto.Posts.Update(post);
            contexto.SaveChanges();

        }

        public IList<string> ListaCategorias()
        {
            List<string> listaCategorias = contexto.Posts
                                                        .Select(p => p.Categoria)
                                                        .Distinct()
                                                        .ToList();
            return listaCategorias;
        }

        public IList<string> ListaCategoriasQueContemTermo(string termoDigitado)
        {
            return contexto.Posts
                .Where(p => p.Categoria.Contains(termoDigitado))
                .Select(p => p.Categoria)
                .Distinct()
                .ToList();
        }

        public void Publica(int id)
        {
            Post post = contexto.Posts.Find(id);
            post.Publicado = true;
            post.DataPublicacao = DateTime.Now;
            contexto.Posts.Update(post);
            contexto.SaveChanges();
        }

        public void Deleta(int id)
        {
            Post post = contexto.Posts.Find(id);
            contexto.Posts.Remove(post);
            contexto.SaveChanges();
        }

        public Post BuscaPorId(int id)
        {
            return contexto.Posts.Find(id);
        }
    }
}
