using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
	public class AuteurModel : PageModel
	{

		public List<AuteurInfo> listAuteur = new List<AuteurInfo>();
		public void OnGet()
		{
			// connection vers la base de données
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Auteur";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader rd = cmd.ExecuteReader();

				while (rd.Read())
				{
					AuteurInfo auteurinf = new AuteurInfo();
					auteurinf.idAuteur = rd.GetInt32(0);
					auteurinf.nomAuteur = rd.GetString(1);
					auteurinf.emailAuteur = rd.GetString(2);
					auteurinf.telephoneAuteur = rd.GetString(3);
					auteurinf.adresseAuteur = rd.GetString(4);
					listAuteur.Add(auteurinf);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
	}
	public class AuteurInfo
	{
		public int idAuteur;
		public string nomAuteur;
		public string emailAuteur;
		public string telephoneAuteur;
		public string adresseAuteur;
	}



}


