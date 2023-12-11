using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Gestion_livres.Pages
{
    public class Edit_LivreModel : PageModel
    {
		public LivreInfo livreinfo = new LivreInfo();
        public List<CatInfo> listCategorie = new List<CatInfo>();
        [BindProperty]
        public IFormFile ImageFile { get; set; }


        public List<EditeurInfo> listEditeur = new List<EditeurInfo>();
        public List<AuteurInfo> listAuteur = new List<AuteurInfo>();

        private readonly IWebHostEnvironment _hostingEnvironment;

        public Edit_LivreModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
                    livreinfo.isbn = rd.GetString(2);
                    livreinfo.idEditeur = rd.GetInt32(3);
                    livreinfo.idAuteur = rd.GetInt32(4);
                    livreinfo.idCat = rd.GetInt32(5);
                    livreinfo.descripLivre = rd.GetString(6);
                    livreinfo.anneeEdition = rd.GetInt32(7);
                    livreinfo.imagepath = rd.GetString(8);

                }
            }
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
			}
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
		public void OnPost()
		{
			livreinfo.idLivre = Convert.ToInt32(Request.Form["id"]);
			livreinfo.titre = Request.Form["titre"];
			livreinfo.isbn = Request.Form["isbn"];
			livreinfo.idEditeur = Convert.ToInt32(Request.Form["idediteur"]);
			livreinfo.idAuteur = Convert.ToInt32(Request.Form["idauteur"]);
			livreinfo.idCat = Convert.ToInt32(Request.Form["idcat"]);
			livreinfo.descripLivre = Request.Form["description"];//anneedition
			livreinfo.anneeEdition = Convert.ToInt32(Request.Form["annee"]);
			livreinfo.imagepath = (Request.Form["ImageFile"]);

			try
			{
				string connectionString = @"Data Source=DESKTOP-V8TA7E5;Initial Catalog = gestion_livre; Integrated Security = True";
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
                string imagePath = SaveImage(ImageFile);


                string sql = "update Livre set  titre = @titre,isbn = @isbn,idEditeur=@idediteur,idAuteur=@idauteur,idCat=@idcat,descripLivre=@descriplivre,anneeEdition=@anneedition , imagepath=@imagepath where idLivre = @idlivre";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@idlivre", livreinfo.idLivre);
				cmd.Parameters.AddWithValue("@titre", livreinfo.titre);
				cmd.Parameters.AddWithValue("@isbn", livreinfo.isbn);
				cmd.Parameters.AddWithValue("@idediteur", livreinfo.idEditeur);
				cmd.Parameters.AddWithValue("@idauteur", livreinfo.idAuteur);
				cmd.Parameters.AddWithValue("@idcat", livreinfo.idCat);
				cmd.Parameters.AddWithValue("@descriplivre", livreinfo.descripLivre);
				cmd.Parameters.AddWithValue("@anneedition", livreinfo.anneeEdition);
                cmd.Parameters.AddWithValue("@imagepath", imagePath);

                cmd.ExecuteNonQuery();con.Close();
				con.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception " + ex.ToString());
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

