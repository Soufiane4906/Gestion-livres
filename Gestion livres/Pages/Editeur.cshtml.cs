using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class EditeurModel : PageModel
    {
		public List<EditeurInfo> listEditeur = new List<EditeurInfo>();
		public void OnGet()
		{
			// connection vers la base de données
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Editeur";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader rd = cmd.ExecuteReader();

				while (rd.Read())
				{
					EditeurInfo editeurinf = new EditeurInfo();
					editeurinf.idEditeur = rd.GetInt32(0);
					editeurinf.nomEditeur = rd.GetString(1);
					editeurinf.descriptionEditeur = rd.GetString(2);
					editeurinf.emailEditeur = rd.GetString(3);
					editeurinf.telephoneEditeur = rd.GetString(4);
					editeurinf.adresseEditeur = rd.GetString(5);
					listEditeur.Add(editeurinf);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
	}
	public class EditeurInfo
	{
		public int idEditeur;
		public string nomEditeur;
		public string descriptionEditeur;
		public string emailEditeur;
		public string telephoneEditeur;
		public string adresseEditeur;
	}


}

