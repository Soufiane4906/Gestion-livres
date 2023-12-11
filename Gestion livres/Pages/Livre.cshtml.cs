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
		public List<LivreViewModel> listLivre = new List<LivreViewModel>();


        public void OnGet()
		{
            // connection vers la base de données
            try
            {
                string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "SELECT Livre.*, Auteur.nomAuteur, Editeur.nomEditeur, Categorie.nomCat " +
                             "FROM Livre " +
                             "INNER JOIN Auteur ON Livre.idAuteur = Auteur.idAuteur " +
                             "INNER JOIN Editeur ON Livre.idEditeur = Editeur.idEditeur " +
                             "INNER JOIN Categorie ON Livre.idCat = Categorie.idCat";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    LivreViewModel livreViewModel = new LivreViewModel
                    {
                        IdLivre = rd.GetInt32(0),
                        Titre = rd.GetString(1),
                        Isbn = rd.GetString(2),
                        NomEditeur = rd.GetString(10),  // Indice de la colonne NomEditeur dans la requête SQL
                        NomAuteur = rd.GetString(9),   // Indice de la colonne NomAuteur dans la requête SQL
                        NomCategorie = rd.GetString(11), // Indice de la colonne NomCategorie dans la requête SQL
                        DescripLivre = rd.GetString(6),
                        AnneeEdition = rd.GetInt32(7),
                        imagepath = rd.GetString(8)
                    };

                    listLivre.Add(livreViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        
        }
	}

    public class LivreViewModel
    {
        public int IdLivre { get; set; }
        public string Titre { get; set; }
        public string Isbn { get; set; }
        public string NomEditeur { get; set; }
        public string NomAuteur { get; set; }
        public string NomCategorie { get; set; }
        public string DescripLivre { get; set; }
        public int AnneeEdition { get; set; }
        public string imagepath { get; set; }
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
        public string imagepath;




    }
}

