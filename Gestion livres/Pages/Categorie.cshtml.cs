using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class CategorieModel : PageModel
    {
		public List<CatInfo> listCategorie = new List<CatInfo>();
		public void OnGet()
		{
			// connection vers la base de données
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Categorie";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader rd = cmd.ExecuteReader();

				while (rd.Read())
				{
					CatInfo catinf = new CatInfo();
					catinf.idCat = rd.GetInt32(0);
					catinf.nomCat = rd.GetString(1);
					catinf.descriptionCat = rd.GetString(2);
					listCategorie.Add(catinf);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
	}
	public class CatInfo
	{
		public int idCat;
		public string nomCat;
		public string descriptionCat;
		
	}


}

