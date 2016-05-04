using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Garcom.Dominio.Entidades;
using Garcom.Apresentacao.ViewModels;
using Garcom.Aplicacao.Interfaces;

namespace Garcom.Apresentacao.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IAppServiceUsuario _serviceUsuario;

        public UsuarioController(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        //
        // GET: /Usuario/
        public ActionResult Index()
        {
            var usuarioViewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_serviceUsuario.RetornaTodos());
            return View(usuarioViewModel);
        }

        //
        // GET: /Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioDominio = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                _serviceUsuario.Adiciona(usuarioDominio);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
