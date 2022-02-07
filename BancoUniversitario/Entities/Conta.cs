using System;
using System.Collections.Generic;

namespace BancoUniversitario.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string NumeroDaConta { get; set; }
        public decimal Saldo { get; set; }
        public string UserId { get; set; }
        public virtual User User{ get; set; }
        public DateTime DataCadastro{ get; set; }
        public bool Ativo{ get; set; }
        public ICollection<Movimentacao> Movimentacoes { get; set; }


    }
}
