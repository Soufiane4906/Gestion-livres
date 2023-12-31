using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace Gestion_livres.Pages
{
	public class Create_LivreModel : PageModel
	{
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Create_LivreModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }



        public LivreInfo livreInfo = new LivreInfo();
		public string errormessage = "";
		public string SuccessMessage = "";
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public List<AuteurInfo> listAuteur = new List<AuteurInfo>();
        public List<CatInfo> listCategorie = new List<CatInfo>();
        		public List<EditeurInfo> listEditeur = new List<EditeurInfo>();


        public void OnGet()
		{
            try
            {
                string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";
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
            try
            {
                string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";
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
            try
            {
                string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";
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

     

        public async  void OnPost()
		{
			livreInfo.titre = Request.Form["titre"];
			livreInfo.isbn = Request.Form["isbn"];
			livreInfo.idEditeur = Convert.ToInt32(Request.Form["idediteur"]);
			livreInfo.idAuteur = Convert.ToInt32(Request.Form["idauteur"]);
			livreInfo.idCat = Convert.ToInt32(Request.Form["idcat"]);
			livreInfo.descripLivre = Request.Form["description"];
			livreInfo.anneeEdition = Convert.ToInt32(Request.Form["annee"]);
			livreInfo.anneeEdition = Convert.ToInt32(Request.Form["annee"]);

            if (string.IsNullOrEmpty(livreInfo.titre) || string.IsNullOrEmpty(livreInfo.isbn) || (livreInfo.idEditeur) == null || (livreInfo.idCat) == null || (livreInfo.idAuteur) == null || (livreInfo.anneeEdition) == null)
			{
				errormessage = "Le titre et l'ISBN sont obligatoires";
				return;
			}

			try
			{
				string connectionString = @"Data Source=MERI\SQLEXPRESS;Initial Catalog = gestion_livre; Integrated Security = True";

				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
                    string imagePath = SaveImage(ImageFile);


                    string sql = "INSERT INTO Livre(titre, isbn, idEditeur, idAuteur, idCat, descripLivre, anneeEdition, imagepath) VALUES(@titre, @isbn, @idEditeur, @idAuteur, @idCat, @descripLivre, @anneeEdition, @imagepath)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
					{
						cmd.Parameters.AddWithValue("@titre", livreInfo.titre);
						cmd.Parameters.AddWithValue("@isbn", livreInfo.isbn);
						cmd.Parameters.AddWithValue("@idEditeur", livreInfo.idEditeur);
						cmd.Parameters.AddWithValue("@idAuteur", livreInfo.idAuteur);
						cmd.Parameters.AddWithValue("@idCat", livreInfo.idCat);
						cmd.Parameters.AddWithValue("@descripLivre", livreInfo.descripLivre);
						cmd.Parameters.AddWithValue("@anneeEdition", livreInfo.anneeEdition);
                        cmd.Parameters.AddWithValue("@imagepath", imagePath);

                        await cmd.ExecuteNonQueryAsync();
                        con.Close();
					}
				}

				SuccessMessage = "Livre ajout� avec succ�s";
			}
			catch (Exception ex)
			{
				// Utilisez un syst�me de journalisation appropri� ici
				errormessage = "Une erreur s'est produite lors de l'ajout du livre. Veuillez r�essayer.";
			}
            Response.Redirect("/Livre");




        }

        private string SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; // Handle the case where no image is provided
            }

            string wwwRootPath = _hostingEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(wwwRootPath, "images", fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "images/" + fileName;
        }
    }

}


