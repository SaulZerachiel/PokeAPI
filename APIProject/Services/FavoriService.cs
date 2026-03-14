using APIProject.Models;

namespace APIProject.Services
{
    public class FavoriService
    {
        // Stockage en memoire: pas de base de donnees dans cette version.
        private List<Favori> _favoris = new List<Favori>();
        private int _prochainId = 1;

        public List<Favori> GetFavoris()
        {
            return _favoris;
        }

        public void AjouterFavori(Favori favori)
        {
            // Id auto-incremente pour identifier facilement les elements.
            favori.Id = _prochainId;
            _prochainId++;
            _favoris.Add(favori);
        }

        public void ModifierNote(int id, string nouvelleNote)
        {
            var favori = _favoris.FirstOrDefault(f => f.Id == id);
            if (favori != null)
            {
                // Edition locale d'une note associee au favori.
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