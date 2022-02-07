using BancoUniversitario.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BancoUniversitario.ViewModels
{
    public class HomeViewModel
    {
        public string NumeroDaConta{ get; set; }
        public decimal Saldo { get; set; }

        public TipoMovimentacaoEnum TipoMovimentacao { get; set; }

    }
}
