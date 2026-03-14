namespace APIProject.Models
{
    // Modele simplifie utilise par l'UI apres mapping depuis l'API externe.
    public class Pokemon
    {
        public string Name { get; set; } = "";
        public int Height { get; set; }
        public int Weight { get; set; }
        public string ImageUrl { get; set; } = "";
        public List<string> Types { get; set; } = new List<string>();
    }
}