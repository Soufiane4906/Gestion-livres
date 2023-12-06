using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class Create_editModel : PageModel
    {
		public EditeurInfo editeurInfo = new EditeurInfo();
		public string errormessage = "";
		public string SuccessMessage = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{
			editeurInfo.nomEditeur = Request.Form["Nom"];
			editeurInfo.descriptionEditeur = Request.Form["Description"];
			editeurInfo.emailEditeur = Request.Form["Email"];
			editeurInfo.telephoneEditeur = Request.Form["Telephone"];
			editeurInfo.adresseEditeur = Request.Form["Adresse"];
			if (editeurInfo.nomEditeur.Length == 0 || editeurInfo.descriptionEditeur.Length == 0 || editeurInfo.emailEditeur.Length == 0 || editeurInfo.telephoneEditeur.Length == 0 || editeurInfo.adresseEditeur.Length==0)
			{
				errormessage = "Tous les champs sont obligatoires";
				return;
			}

			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "insert into Editeur(nomEditeur,descriptionEditeur,emailEditeur,telephoneEditeur,adresseEditeur) values(@nomEditeur,@descriptionEditeur, @emailEditeur, @telephoneEditeur, @adresseEditeur)";
				SqlCommand cmd = new SqlCommand(sql, con);
				// cmd.Parameters.AddWithValue("@id",clientInfo.id);
				cmd.Parameters.AddWithValue("@nomEditeur",editeurInfo.nomEditeur);
				cmd.Parameters.AddWithValue("@descriptionEditeur", editeurInfo.descriptionEditeur);
				cmd.Parameters.AddWithValue("@emailEditeur", editeurInfo.emailEditeur);
				cmd.Parameters.AddWithValue("@telephoneEditeur", editeurInfo.telephoneEditeur);
				cmd.Parameters.AddWithValue("@adresseEditeur", editeurInfo.adresseEditeur);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception" + ex.ToString());
			}




		}
	}
}

