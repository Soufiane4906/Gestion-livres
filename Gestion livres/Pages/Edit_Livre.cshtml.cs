using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class Edit_LivreModel : PageModel
    {
		public LivreInfo livreinfo = new LivreInfo();
		public void OnGet()
		{
			string id = Request.Query["id"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Livre where idLivre=@id";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					livreinfo.idLivre = rd.GetInt32(0);
					livreinfo.titre = rd.GetString(1);
					livreinfo.idEditeur = rd.GetInt32(2);
					livreinfo.idAuteur = rd.GetInt32(3);
					livreinfo.idCat = rd.GetInt32(4);
					livreinfo.descripLivre = rd.GetString(5);
					livreinfo.anneeEdition = rd.GetInt32(6);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
		public void OnPost()
		{
			livreinfo.idLivre = Convert.ToInt32(Request.Form["id"]);
			livreinfo.titre = Request.Form["titre"];
			livreinfo.idEditeur = Convert.ToInt32(Request.Form["idediteur"]);
			livreinfo.idAuteur = Convert.ToInt32(Request.Form["idauteur"]);
			livreinfo.idCat = Convert.ToInt32(Request.Form["idcat"]);
			livreinfo.descripLivre = Request.Form["description"];//anneedition
			livreinfo.anneeEdition = Convert.ToInt32(Request.Form["anne"]);
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();

				string sql = "update Livre set  titre = @titre,isbn = @isbn,idEditeur=@idediteur,idAuteur=@idauteur,idCat=@idcat,descripLivre=@descriplivre,anneeEdition=@anneedition where idLivre = @idlivre";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@idlivre", livreinfo.idLivre);
				cmd.Parameters.AddWithValue("@titre", livreinfo.titre);
				cmd.Parameters.AddWithValue("@idediteur", livreinfo.idEditeur);
				cmd.Parameters.AddWithValue("@idauteur", livreinfo.idAuteur);
				cmd.Parameters.AddWithValue("@idcat", livreinfo.idCat);
				cmd.Parameters.AddWithValue("@descriplivre", livreinfo.descripLivre);
				cmd.Parameters.AddWithValue("@anneedition", livreinfo.anneeEdition);
				cmd.ExecuteNonQuery();
				con.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
			Response.Redirect("/Livre");
		}
	}
}

