namespace APIProject.Services
{
    public class AuthService
    {
        public bool EstConnecte { get; private set; } = false;

        public string UtilisateurActuel { get; private set; } = "";

        public bool Connecter(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                EstConnecte = true;
                UtilisateurActuel = username;
                return true;
            }
            return false;
        }

        public void Deconnecter()
        {
            EstConnecte = false;
            UtilisateurActuel = "";
        }
    }
}