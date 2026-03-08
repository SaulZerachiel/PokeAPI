namespace APIProject.Models
{
    public class Pokemon
    {
        public string Name { get; set; } = "";
        public int Height { get; set; }
        public int Weight { get; set; }
        public string ImageUrl { get; set; } = "";
        public List<string> Types { get; set; } = new List<string>();
    }
}