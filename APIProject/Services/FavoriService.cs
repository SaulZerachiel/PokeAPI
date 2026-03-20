using APIProject.Models;

namespace APIProject.Services
{
    public class FavoriService
    {
        // Stockage en memoire par utilisateur pour la session courante.
        private readonly Dictionary<string, FavorisUtilisateur> _favorisParUtilisateur =
            new(StringComparer.OrdinalIgnoreCase);

        public List<Favori> GetFavoris(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<Favori>();
            }

            return GetOuCreerEtatUtilisateur(username).Favoris;
        }

        public void AjouterFavori(string username, Favori favori)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            var etat = GetOuCreerEtatUtilisateur(username);

            // Id auto-incremente pour identifier facilement les elements.
            favori.Id = etat.ProchainId;
            etat.ProchainId++;
            etat.Favoris.Add(favori);
        }

        public void ModifierNote(string username, int id, string nouvelleNote)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            var etat = GetOuCreerEtatUtilisateur(username);
            var favori = etat.Favoris.FirstOrDefault(f => f.Id == id);
            if (favori != null)
            {
                // Edition locale d'une note associee au favori.
                favori.Note = nouvelleNote;
            }
        }

        public void SupprimerFavori(string username, int id)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            var etat = GetOuCreerEtatUtilisateur(username);
            var favori = etat.Favoris.FirstOrDefault(f => f.Id == id);
            if (favori != null)
            {
                etat.Favoris.Remove(favori);
            }
        }

        private FavorisUtilisateur GetOuCreerEtatUtilisateur(string username)
        {
            var cle = username.Trim();

            if (!_favorisParUtilisateur.TryGetValue(cle, out var etat))
            {
                etat = new FavorisUtilisateur();
                _favorisParUtilisateur[cle] = etat;
            }

            return etat;
        }

        private class FavorisUtilisateur
        {
            public List<Favori> Favoris { get; set; } = new();
            public int ProchainId { get; set; } = 1;
        }
    }
}