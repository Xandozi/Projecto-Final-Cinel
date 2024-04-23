using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class horarios_detalhe_lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nome_turma = Request.QueryString["nome_turma"];

            lbl_nome_turma.Text = nome_turma;
        }
    }
}