using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class Edit_EditeurModel : PageModel
    {
		public EditeurInfo editeurinfo = new EditeurInfo();
		public void OnGet()
		{
			string id = Request.Query["id"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Editeur where idediteur=@id";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					editeurinfo.idEditeur = rd.GetInt32(0);
					editeurinfo.nomEditeur = rd.GetString(1);
					editeurinfo.descriptionEditeur = rd.GetString(2);
					editeurinfo.emailEditeur = rd.GetString(3);
					editeurinfo.telephoneEditeur = rd.GetString(4);
					editeurinfo.adresseEditeur = rd.GetString(5);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
		public void OnPost()
		{
			editeurinfo.idEditeur = Convert.ToInt32(Request.Form["id"]);
			editeurinfo.nomEditeur = Request.Form["nom"];
			editeurinfo.descriptionEditeur = Request.Form["description"];
			editeurinfo.emailEditeur = Request.Form["email"];
			editeurinfo.telephoneEditeur = Request.Form["telephone"];
			editeurinfo.adresseEditeur = Request.Form["adresse"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();

				string sql = "update Editeur set  nomEditeur = @nomediteur,descriptionEditeur = @descriptionEditeur,emailEditeur=@emailEditeur,telephoneEditeur=@telephoneEditeur,adresseEditeur=@adresseEditeur where idEditeur = @idEditeur";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@idediteur", editeurinfo.idEditeur);
				cmd.Parameters.AddWithValue("@nomediteur", editeurinfo.nomEditeur);
				cmd.Parameters.AddWithValue("@descriptionEditeur", editeurinfo.descriptionEditeur);
				cmd.Parameters.AddWithValue("@emailEditeur", editeurinfo.emailEditeur);
				cmd.Parameters.AddWithValue("@telephoneEditeur", editeurinfo.telephoneEditeur);
				cmd.Parameters.AddWithValue("@adresseEditeur", editeurinfo.adresseEditeur);
					cmd.ExecuteNonQuery();con.Close();
				con.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
			Response.Redirect("/Editeur");
		}
	}
}
