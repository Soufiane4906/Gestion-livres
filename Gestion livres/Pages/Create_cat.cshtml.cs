using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class Create_catModel : PageModel
    {
		public CatInfo catInfo = new CatInfo();
		public string errormessage = "";
		public string SuccessMessage = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{
			catInfo.nomCat = Request.Form["Nom"];
			catInfo.descriptionCat = Request.Form["Desc"];
			if (catInfo.nomCat.Length == 0 || catInfo.descriptionCat.Length == 0 )
			{
				errormessage = "Tous les champs sont obligatoires";
				return;
			}

			try
			{
				string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "insert into Categorie (nomCat,descriptionCat) values(@nomCat, @descriptionCat)";
				SqlCommand cmd = new SqlCommand(sql, con);
				// cmd.Parameters.AddWithValue("@id",clientInfo.id);
				cmd.Parameters.AddWithValue("@nomCat", catInfo.nomCat);
				cmd.Parameters.AddWithValue("@descriptionCat", catInfo.descriptionCat);
					cmd.ExecuteNonQuery();con.Close();
				SuccessMessage = "Catégorie ajoutée avec succès";
			
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception" + ex.ToString());
			}

		}
	}
}