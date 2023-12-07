using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class EditerModel : PageModel
    {
		public AuteurInfo auteurinfo = new AuteurInfo();
		public void OnGet()
		{
			string id = Request.Query["id"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Auteur where idAuteur=@id";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					auteurinfo.idAuteur = rd.GetInt32(0);
					auteurinfo.nomAuteur = rd.GetString(1);
					auteurinfo.emailAuteur = rd.GetString(2);
					auteurinfo.telephoneAuteur = rd.GetString(3);
					auteurinfo.adresseAuteur = rd.GetString(4);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
		public void OnPost()
		{
			auteurinfo.idAuteur = Convert.ToInt32(Request.Form["id"]);
			auteurinfo.nomAuteur = Request.Form["nom"];
			auteurinfo.emailAuteur = Request.Form["email"];
			auteurinfo.telephoneAuteur = Request.Form["telephone"];
			auteurinfo.adresseAuteur = Request.Form["adress"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();

				string sql = "update auteur set  nomAuteur = @nomAuteur,emailAuteur = @emailAuteur,telephoneAuteur = @telephoneAuteur, adresseAuteur=@adresseAuteur where idAuteur = @idAuteur";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@idAuteur", auteurinfo.idAuteur);
				cmd.Parameters.AddWithValue("@nomAuteur", auteurinfo.nomAuteur);
				cmd.Parameters.AddWithValue("@emailAuteur", auteurinfo.emailAuteur);
				cmd.Parameters.AddWithValue("@telephoneAuteur", auteurinfo.telephoneAuteur);
				cmd.Parameters.AddWithValue("@adresseAuteur", auteurinfo.adresseAuteur);
					cmd.ExecuteNonQuery();con.Close();
				con.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
			Response.Redirect("/Auteur");
		}
	}

}
