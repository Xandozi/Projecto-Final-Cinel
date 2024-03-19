using Projeto_Final.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Final
{
    public partial class criar_turma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["logged"] != "yes")
            //{
            //    Response.Redirect("login.aspx");
            //}
            //else if (!Validation.Check_IsStaff(Session["username"].ToString()))
            //{
            //    Response.Redirect("personal_zone.aspx");
            //}
            //else
            //{

            //}
            if (ddl_curso.SelectedIndex != -1)
            {
                List<Formadores> lst_formadores = Formadores.Ler_FormadoresAll();
                List<string> lst_formadores_string = new List<string>();

                List<Modulos> lst_modulos = Modulos.Ler_ModulosAll_Curso(Convert.ToInt32(ddl_curso.SelectedValue));
                List<string> lst_modulos_string = new List<string>();

                foreach (Formadores formador in lst_formadores)
                {
                    string nome = formador.nome_proprio + " " + formador.apelido;

                    lst_formadores_string.Add(nome);
                }

                foreach (Modulos modulo in lst_modulos)
                {
                    string nome = modulo.nome_modulo;

                    lst_modulos_string.Add(nome);
                }

                lstb_formadores.DataSource = lst_formadores_string;
                lstb_formadores.DataBind();

                lstb_modulos.DataSource = lst_modulos_string;
                lstb_modulos.DataBind();
            }
        }
    }
}