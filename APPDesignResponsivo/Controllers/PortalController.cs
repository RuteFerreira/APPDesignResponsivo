using APPDB.Models;
using APPDB.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APPDesignResponsivo.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            ProdutoHelperCRUD ph = new ProdutoHelperCRUD(Program.ligacao);
            List<Produto> produtoParaAView = ph.list(Produto.TipoEstado.Ativo);
            return View(produtoParaAView);
        }
        public IActionResult Acerca()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Consultar(string id)
        {
            Guid idAPesquisar = Guid.Empty;
            Boolean idIsOk = Guid.TryParse(id, out idAPesquisar);
            if (idIsOk)
            {
                ProdutoHelperCRUD ph = new ProdutoHelperCRUD(Program.ligacao);
                Produto produto = ph.read(id);
                
                if (produto.Id != Guid.Empty)
                {
                   
                    return View(produto);
                }
                else return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View();


    }
            
        }
}
