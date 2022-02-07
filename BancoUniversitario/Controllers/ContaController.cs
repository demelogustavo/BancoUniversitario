using BancoUniversitario.Data;
using BancoUniversitario.Models;
using BancoUniversitario.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace BancoUniversitario.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        
        public ContaController(UserManager<User> userManager)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var conta = _context.Contas.FirstOrDefault(x => x.UserId == userId);
            if (conta == null)
                return RedirectToAction(nameof(List));
            var viewModel=new HomeViewModel { NumeroDaConta=conta.NumeroDaConta , Saldo = conta.Saldo};
            var tipos = Enum.GetValues(typeof(TipoMovimentacaoEnum));
            return View(viewModel);
        }
        public IActionResult List()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listaContas = _context.Contas.Where(x=>x.UserId==userId).ToList().Select(x=>new ContaViewModel {Id=x.Id,NumeroDaConta=x.NumeroDaConta}).ToList();
            return View(listaContas);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContaViewModel contaViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Contas.Add(new Conta { Ativo=true, DataCadastro= DateTime.Now, NumeroDaConta = contaViewModel.NumeroDaConta , UserId = userId  });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ProcessarMovimentacao(MovimentacaoViewModel movimentacaoViewModel)
        {
            if (movimentacaoViewModel.Valor <= 0) 
            {
                TempData["erro"] = "O valor deve ser positivo";
                return RedirectToAction(nameof(Index));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var conta = _context.Contas.Where(x=>x.UserId == userId).FirstOrDefault();
            if (conta == null) 
            {
                TempData["erro"] = "O usuário não possue conta cadastrada";
            return View();
            }
            _context.Movimetacoes.Add(new Movimentacao { ContaId = conta.Id, DataCadastro = DateTime.Now, TipoMovimentacao = movimentacaoViewModel.TipoMovimentacao, Valor = movimentacaoViewModel.Valor });
            if (movimentacaoViewModel.TipoMovimentacao == TipoMovimentacaoEnum.Deposito)
                conta.Saldo = conta.Saldo + movimentacaoViewModel.Valor;
            else
                conta.Saldo = conta.Saldo - movimentacaoViewModel.Valor;

            if (conta.Saldo < 0) 
            {
                TempData["erro"] = "Você não possue saldo suficiente para essa operação.";
                return RedirectToAction(nameof(Index));
            }

            _context.Contas.Update(conta);
            _context.SaveChanges();
            TempData["sucesso"] = "Operação realizada com sucesso! :)";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var conta = _context.Contas.FirstOrDefault(x=>x.Id==id);
            return View(new ContaViewModel { Id=conta.Id, NumeroDaConta = conta.NumeroDaConta});
        }
        
        [HttpPost]
        public IActionResult Delete(ContaViewModel contaViewModel)
        {
            var conta = _context.Contas.FirstOrDefault(x => x.Id == contaViewModel.Id);
            _context.Contas.Remove(conta);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
    }
}
