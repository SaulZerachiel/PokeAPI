using APIProject.Models;

namespace APIProject.Services
{
    public class FavoriService
    {
        private List<Favori> _favoris = new List<Favori>();
        private int _prochainId = 1;

        public List<Favori> GetFavoris()
        {
            return _favoris;
        }

        public void AjouterFavori(Favori favori)
        {
            favori.Id = _prochainId;
            _prochainId++;
            _favoris.Add(favori);
        }

        public void ModifierNote(int id, string nouvelleNote)
        {
            var favori = _favoris.FirstOrDefault(f => f.Id == id);
            if (favori != null)
            {
                favori.Note = nouvelleNote;
            }
        }

        public void SupprimerFavori(int id)
        {
            var favori = _favoris.FirstOrDefault(f => f.Id == id);
            if (favori != null)
            {
                _favoris.Remove(favori);
            }
        }
    }
}