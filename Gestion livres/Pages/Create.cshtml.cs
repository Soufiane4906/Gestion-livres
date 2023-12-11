using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class CreateModel : PageModel
    {
		public AuteurInfo auteurInfo = new AuteurInfo();
		public string errormessage = "";
		public string SuccessMessage = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{
			auteurInfo.nomAuteur = Request.Form["Nom"];
			auteurInfo.emailAuteur = Request.Form["Email"];
			auteurInfo.telephoneAuteur = Request.Form["Telephone"];
			auteurInfo.adresseAuteur = Request.Form["Adresse"];
			if (auteurInfo.nomAuteur.Length == 0 || auteurInfo.emailAuteur.Length == 0 || auteurInfo.telephoneAuteur.Length == 0 || auteurInfo.adresseAuteur.Length == 0)
			{
				errormessage = "Tous les champs sont obligatoires";
				return;
			}

			try
			{
				string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "insert into auteur(nomAuteur,emailAuteur,telephoneAuteur,adresseAuteur) values(@nomAuteur, @emailAuteur, @telephoneAuteur, @adresseAuteur)";
				SqlCommand cmd = new SqlCommand(sql, con);
				// cmd.Parameters.AddWithValue("@id",clientInfo.id);
				cmd.Parameters.AddWithValue("@nomAuteur", auteurInfo.nomAuteur);
				cmd.Parameters.AddWithValue("@emailAuteur", auteurInfo.emailAuteur);
				cmd.Parameters.AddWithValue("@telephoneAuteur", auteurInfo.telephoneAuteur);
				cmd.Parameters.AddWithValue("@adresseAuteur", auteurInfo.adresseAuteur);
					cmd.ExecuteNonQuery();con.Close();
				SuccessMessage = "Auteur ajoutée avec succès";

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception" + ex.ToString());
			}




            Response.Redirect("/Auteur");
        }
	}
	
}
