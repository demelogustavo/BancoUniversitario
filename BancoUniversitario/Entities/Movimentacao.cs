using System;

namespace BancoUniversitario.Models
{
    public class Movimentacao
    {
        public int Id{ get; set; }
        public TipoMovimentacaoEnum TipoMovimentacao{ get; set; }

        public DateTime DataCadastro { get; set; }

        public decimal Valor { get; set; }

        public int ContaId{ get; set; }

        public Conta Conta { get; set; }

    }
}
