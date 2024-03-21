using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_Final.Classes
{
    public class Formadores_Modulos
    {
        public int cod_formador { get; set; }
        public int cod_modulo { get; set; }
        public int cod_ufcd { get; set; }
        public int cod_turma { get; set; }
        public string nome_completo { get; set; }
        public string nome_modulo { get; set; }
        public string formador_modulo { get { return $"{nome_completo} | {cod_ufcd} | {nome_modulo}"; } }
        public string cod_formador_modulo { get { return $"{cod_formador}|{cod_modulo}"; } }
    }
}