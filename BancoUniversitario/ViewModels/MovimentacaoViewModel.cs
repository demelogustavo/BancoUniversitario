using BancoUniversitario.Models;

namespace BancoUniversitario.ViewModels
{
    public class MovimentacaoViewModel
    {
        public int Id { get; set; }
        public TipoMovimentacaoEnum TipoMovimentacao { get; set; }

        public decimal Valor { get; set; }

    }
}
