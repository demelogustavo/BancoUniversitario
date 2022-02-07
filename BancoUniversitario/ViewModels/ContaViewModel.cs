using System;
using System.ComponentModel.DataAnnotations;

namespace BancoUniversitario.ViewModels
{
    public class ContaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Numero da Conta Obrigatório")]
        public string NumeroDaConta { get; set; }
       
        


    }
}
