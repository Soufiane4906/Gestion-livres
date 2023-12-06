using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gestion_livres.Pages
{
	public class Create_LivreModel : PageModel
	{

		public LivreInfo livreInfo = new LivreInfo();
		public string errormessage = "";
		public string SuccessMessage = "";

        public List<AuteurInfo> listAuteur = new List<AuteurInfo>();
        public void OnGet()
		{
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

        private List<AuteurModel> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public void OnPost()
		{
			livreInfo.titre = Request.Form["titre"];
			livreInfo.isbn = Request.Form["isbn"];
			livreInfo.idEditeur = Convert.ToInt32(Request.Form["idediteur"]);
			livreInfo.idAuteur = Convert.ToInt32(Request.Form["idauteur"]);
			livreInfo.idCat = Convert.ToInt32(Request.Form["idcat"]);
			livreInfo.descripLivre = Request.Form["description"];
			livreInfo.anneeEdition = Convert.ToInt32(Request.Form["anne"]);

			if (string.IsNullOrEmpty(livreInfo.titre) || string.IsNullOrEmpty(livreInfo.isbn))
			{
				errormessage = "Le titre et l'ISBN sont obligatoires";
				return;
			}

			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";

				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					string sql = "insert into Livre(titre, isbn, idEditeur,idAuteur,idCat,descripLivre ,anneeEdition) values(@titre,@isbn, @idEditeur, @idAuteur,@idCat,@descripLivre,@anneeEdition)";
					using (SqlCommand cmd = new SqlCommand(sql, con))
					{
						cmd.Parameters.AddWithValue("@titre", livreInfo.titre);
						cmd.Parameters.AddWithValue("@isbn", livreInfo.isbn);
						cmd.Parameters.AddWithValue("@idEditeur", livreInfo.idEditeur);
						cmd.Parameters.AddWithValue("@idAuteur", livreInfo.idAuteur);
						cmd.Parameters.AddWithValue("@idCat", livreInfo.idCat);
						cmd.Parameters.AddWithValue("@descripLivre", livreInfo.descripLivre);
						cmd.Parameters.AddWithValue("@anneeEdition", livreInfo.anneeEdition);

						cmd.ExecuteNonQuery();
					}
				}

				SuccessMessage = "Livre ajout� avec succ�s";
			}
			catch (Exception ex)
			{
				// Utilisez un syst�me de journalisation appropri� ici
				errormessage = "Une erreur s'est produite lors de l'ajout du livre. Veuillez r�essayer.";
			}

		}
	}
}

