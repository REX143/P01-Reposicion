using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class KardexViewModel
    {
        [Display(Name ="Fecha Movimiento")]
        public DateTime FECHA_MOV { get; set; }

        [Display(Name = "Código Artículo")]
        public string COD_ART_IMPORT { get; set; }

        [Display(Name = "Documento operación")]
        public string NUMERO_DOCUMENTO { get; set; }

        [Display(Name = "Cantidad Movimiento")]
        public int CANTIDADES { get; set; }

        [Display(Name = "Stock del día")]
        public int TOTAL_AL_DIA { get; set; }

        [Display(Name = "Descripción Movimiento")]
        public string DESCRIP_MOV { get; set; }

        [Display(Name = "Usuario")]
        public string PC_CLIENTE { get; set; }
    }
}