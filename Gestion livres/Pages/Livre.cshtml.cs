using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
	public class LivreModel : PageModel
	{
		public List<LivreInfo> listLivre = new List<LivreInfo>();
		public void OnGet()
		{
			// connection vers la base de données
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select distinct * from Livre ";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					LivreInfo livreinf = new LivreInfo();
					livreinf.idLivre = rd.GetInt32(0);
					livreinf.titre = rd.GetString(1);
					livreinf.isbn = rd.GetString(2);
					livreinf.idEditeur = rd.GetInt32(3);
					livreinf.idAuteur = rd.GetInt32(4);
					livreinf.idCat = rd.GetInt32(5);
					livreinf.descripLivre = rd.GetString(6);
					livreinf.anneeEdition = rd.GetInt32(7);
					listLivre.Add(livreinf);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
	}
	public class LivreInfo
	{
		public int idLivre;
		public string titre;
		public string isbn;
		public int idCat;
		public int idAuteur;
		public int idEditeur;
		public string descripLivre;
		public int anneeEdition;
		


	}
}

