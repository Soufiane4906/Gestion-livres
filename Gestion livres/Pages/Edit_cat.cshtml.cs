using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
    public class Edit_catModel : PageModel
    {
		public CatInfo catinfo = new CatInfo();
		public void OnGet()
		{
			string id = Request.Query["id"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from Categorie where idCat=@id";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					catinfo.idCat = rd.GetInt32(0);
					catinfo.nomCat = rd.GetString(1);
					catinfo.descriptionCat = rd.GetString(2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
		}
		public void OnPost()
		{
			catinfo.idCat = Convert.ToInt32(Request.Form["id"]);
			catinfo.nomCat = Request.Form["nom"];
			catinfo.descriptionCat = Request.Form["description"];
			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();

				string sql = "update Categorie set  nomCat = @nomCat,descriptionCat = @descriptionCat where idCat = @idCat";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@idCat", catinfo.idCat);
				cmd.Parameters.AddWithValue("@nomCat", catinfo.nomCat);
				cmd.Parameters.AddWithValue("@descriptionCat", catinfo.descriptionCat);
					cmd.ExecuteNonQuery();con.Close();
				con.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
			Response.Redirect("/Categorie");
		}
	}
}
