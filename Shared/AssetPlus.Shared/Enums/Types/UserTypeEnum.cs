using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetPlus.Shared.Enums.Types
{
    public enum UserTypeEnum
    {
        [Display(Name = "Administrador")]
        Administrador = 1,
        [Display(Name = "Usuário Comum")]
        UsuarioComum = 2,
        [Display(Name = "Convidado")]
        Convidado = 3
    }
}
