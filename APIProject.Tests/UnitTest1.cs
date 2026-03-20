using APIProject.Models;
using APIProject.Services;

namespace APIProject.Tests;

public class AuthServiceTests
{
    [Fact]
    public void CreerCompte_Valide_RetourneTrue()
    {
        var auth = new AuthService();

        var ok = auth.CreerCompte("alice", "secret", out var erreur);

        Assert.True(ok);
        Assert.Equal(string.Empty, erreur);
    }

    [Fact]
    public void Connecter_BonsIdentifiants_DefinitUtilisateurConnecte()
    {
        var auth = new AuthService();
        auth.CreerCompte("alice", "secret", out _);

        var ok = auth.Connecter("alice", "secret");

        Assert.True(ok);
        Assert.True(auth.EstConnecte);
        Assert.Equal("alice", auth.UtilisateurActuel);
    }

    [Fact]
    public void Deconnecter_ReinitialiseEtat()
    {
        var auth = new AuthService();
        auth.CreerCompte("alice", "secret", out _);
        auth.Connecter("alice", "secret");

        auth.Deconnecter();

        Assert.False(auth.EstConnecte);
        Assert.Equal(string.Empty, auth.UtilisateurActuel);
    }
}

public class FavoriServiceTests
{
    [Fact]
    public void AjouterFavori_SeparationParUtilisateur()
    {
        var service = new FavoriService();

        service.AjouterFavori("alice", new Favori { Nom = "pikachu" });
        service.AjouterFavori("bob", new Favori { Nom = "bulbasaur" });

        var favorisAlice = service.GetFavoris("alice");
        var favorisBob = service.GetFavoris("bob");

        Assert.Single(favorisAlice);
        Assert.Single(favorisBob);
        Assert.Equal("pikachu", favorisAlice[0].Nom);
        Assert.Equal("bulbasaur", favorisBob[0].Nom);
    }

    [Fact]
    public void ModifierNote_ModifieSeulementLeCompteCible()
    {
        var service = new FavoriService();
        service.AjouterFavori("alice", new Favori { Nom = "pikachu", Note = "" });
        service.AjouterFavori("bob", new Favori { Nom = "pikachu", Note = "" });

        var idAlice = service.GetFavoris("alice")[0].Id;
        service.ModifierNote("alice", idAlice, "Mon pref");

        Assert.Equal("Mon pref", service.GetFavoris("alice")[0].Note);
        Assert.Equal(string.Empty, service.GetFavoris("bob")[0].Note);
    }
}
