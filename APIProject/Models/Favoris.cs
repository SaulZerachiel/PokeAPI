namespace APIProject.Models
{
    // Entite locale pour stocker les favoris de l'utilisateur.
    public class Favori
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Note { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}