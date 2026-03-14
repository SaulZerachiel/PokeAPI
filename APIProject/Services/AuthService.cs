namespace APIProject.Services
{
    public class AuthService
    {
        // Etat de session simple, conserve en memoire pendant la session.
        public bool EstConnecte { get; private set; } = false;

        public string UtilisateurActuel { get; private set; } = "";

        private readonly Dictionary<string, string> _comptes = new(StringComparer.OrdinalIgnoreCase);

        public bool CreerCompte(string username, string password, out string erreur)
        {
            erreur = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                erreur = "Le nom d'utilisateur et le mot de passe sont obligatoires.";
                return false;
            }

            if (_comptes.ContainsKey(username))
            {
                erreur = "Ce nom d'utilisateur existe deja.";
                return false;
            }

            _comptes[username] = password;
            return true;
        }

        public bool Connecter(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (_comptes.TryGetValue(username, out var motDePasse) && motDePasse == password)
            {
                EstConnecte = true;
                UtilisateurActuel = username;
                return true;
            }
            return false;
        }

        public void Deconnecter()
        {
            // On remet l'etat de connexion a zero.
            EstConnecte = false;
            UtilisateurActuel = "";
        }
    }
}