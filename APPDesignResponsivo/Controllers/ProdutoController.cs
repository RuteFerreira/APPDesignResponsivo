using APPDB.Models;
using APPDB.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APPDesignResponsivo.Controllers
{
    public class ProdutoController : Controller
    {
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

            

        
        }
        [HttpGet]
        public IActionResult Editar(string id)
        {
            Guid idAPesquisar = Guid.Empty;
            Boolean idIsOk = Guid.TryParse(id, out idAPesquisar);
            if (idIsOk)
            {
                ProdutoHelperCRUD ph = new ProdutoHelperCRUD(Program.ligacao);
                Produto produto = ph.read(id);
              
                if (produto.Id != Guid.Empty)
                {
                    List<ItemCombo> listaEstados = new List<ItemCombo>();
                    listaEstados.Add(new ItemCombo { Id = (int)Produto.TipoEstado.Inativo, Designacao = "Aguarda Validação" });
                    listaEstados.Add(new ItemCombo { Id = (int)Produto.TipoEstado.Ativo, Designacao = "Válido" });
                    listaEstados.Add(new ItemCombo { Id = (int)Produto.TipoEstado.Apagado, Designacao = "Eliminado" });
                    ViewBag.ListaEstados = listaEstados;
                    return View(produto);
                }
                else return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(Produto produtoEditado)
        {
            string informacao = "";
            if (ModelState.IsValid)
            {
                //informacao += $"ID: {produto.Id}; Designação: {produto.Designacao}; PU: {produto.PUnitario}; Stock: {produto.StkAtual}";
                ProdutoHelperCRUD ph = new ProdutoHelperCRUD(Program.ligacao);
                Guid idDevolvido = ph.update(produtoEditado);
            }
            else
            {
                informacao += "Erro de Informação nos Campos";
                return Content(informacao);
            }
            return RedirectToAction("Index");
        }


    }
}
