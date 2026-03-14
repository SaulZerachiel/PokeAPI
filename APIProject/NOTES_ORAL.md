# Notes d'oral - APIProject

Vidéo Youtube : https://www.youtube.com/watch?v=HVbUyOK7wxU

## 1. Idee generale du projet
- C'est une appli Blazor Server interactive.
- Objectif: chercher un Pokemon via PokeAPI, puis l'ajouter en favoris.
- Le stockage des favoris est en memoire (pas de base de donnees).

Phrase simple a dire:
"Le projet separe bien UI, logique metier et modeles. Les pages Razor gerent l'affichage, les services font la logique, et les modeles transportent les donnees."

## 2. Architecture rapide
- `Program.cs`: configuration globale, DI, pipeline HTTP.
- `Services/`: logique applicative (`AuthService`, `PokemonService`, `FavoriService`).
- `Models/`: structures de donnees (`Pokemon`, `Favori`).
- `Components/Pages/`: ecrans (`Home`, `Favoris`, `Login`).
- `Components/Layout/`: structure globale (menu + zone de contenu).
- `wwwroot/app.css`: style global custom.

## 3. Explication fichier par fichier

### Program.cs
- Active Razor Components en mode interactif serveur.
- Enregistre `HttpClient` pour faire les appels API.
- Enregistre les services metier en singleton.
- Configure le pipeline (HTTPS, fichiers statiques, antiforgery, gestion d'erreurs).
- Mappe le composant racine `App`.

Ce qu'on peut te demander:
- Pourquoi singleton ?
Reponse: pour garder un etat partage simple pendant la vie de l'appli (connexion/favoris).

### Services/AuthService.cs
- Gere un etat de connexion minimal.
- `Connecter(...)` verifie les comptes crees en memoire pendant la session.
- `Deconnecter()` remet l'etat a zero.

Limite assumee:
- Pas de vrai systeme d'auth (hash, token, DB). C'est volontaire pour un projet d'apprentissage.

### Services/PokemonServices.cs
- Fait l'appel HTTP vers `https://pokeapi.co/api/v2/pokemon/{nom}`.
- Deserialize le JSON en classes internes (`PokemonApiReponse`, etc.).
- Transforme ces donnees vers le modele interne `Pokemon`.

Point important:
- Separation DTO API externe vs modele interne pour ne pas coupler l'UI au format de l'API.

### Services/FavoriService.cs
- Garde une liste de favoris en memoire.
- `AjouterFavori`: ajoute avec id auto-incremente.
- `ModifierNote`: modifie la note d'un favori.
- `SupprimerFavori`: retire un favori.

### Models/Pokemon.cs
- Modele affiche dans l'UI: nom, taille, poids, image, types.

### Models/Favoris.cs
- Modele d'un favori local: id, nom, note, image.

### Components/App.razor
- Squelette HTML global (head + body).
- Charge Bootstrap + CSS custom + css scope.
- Monte `Routes` et script Blazor.

### Components/Routes.razor
- Routeur principal de l'application.
- Associe URL -> page Razor.
- Defaut sur `MainLayout`.

### Components/Layout/MainLayout.razor
- Layout commun: barre laterale + contenu principal.

### Components/Layout/NavMenu.razor
- Menu de navigation vers `/`, `/favoris`, `/login`.

### Components/Pages/Home.razor
- Page de recherche Pokemon.
- Si non connecte: affiche un message et lien login.
- Si connecte:
  - saisie du nom
  - appel `Chercher()`
  - affichage resultat
  - bouton ajout favoris
- Gere etat local: `recherche`, `pokemon`, `chargement`, `erreur`, `afficherPopup`.

### Components/Pages/Favoris.razor
- Affiche la liste des favoris.
- Gere edition de note en ligne (`idEnEdition`, `noteEnEdition`).
- Actions: modifier note, supprimer favori.

### Components/Pages/Login.razor
- Formulaire login tres simple.
- Appel `Auth.Connecter(...)` puis redirection vers `/` si succes.

## 4. Choix CSS (ce que j'ai modifie)
- Oui, j'ai ajoute un CSS assez fourni mais volontairement simple a maintenir.
- Tout est centralise dans `wwwroot/app.css` avec variables (`:root`).
- J'ai remplace les styles inline par des classes (`card-box`, `search-row`, `fav-item`, etc.).
- Le layout a ete modernise via `MainLayout.razor.css` + `NavMenu.razor.css`.
- Responsive: adaptation mobile (grilles en 1 colonne, champs largeur 100%).

Phrase simple a dire:
"J'ai d'abord factorise le style en classes reutilisables, puis j'ai harmonise les pages autour d'une petite charte visuelle pour eviter le template brut de base."

## 5. Limites actuelles (important a assumer)
- Authentification hardcodee.
- Donnees en memoire (perdues au redemarrage).
- Pas de persistance base de donnees.
- Gestion d'erreur API encore basique.

## 6. Evolutions possibles
- Ajouter une vraie auth (Identity/JWT).
- Ajouter une base SQLite/SQL Server pour les favoris.
- Eviter les doublons de favoris.
- Ajouter validation de formulaire et etats de chargement plus fins.

## 7. Mini script oral (30-45 sec)
"Le projet est une appli Blazor Server. La navigation et le layout sont dans les composants Razor, la logique est dans des services injectes par DI. `PokemonService` appelle PokeAPI et mappe la reponse vers mon modele interne `Pokemon`. `FavoriService` gere une liste en memoire avec ajout, suppression et edition de note. `AuthService` simule une connexion simple pour proteger les ecrans. J'ai aussi refactorise le CSS: j'ai retire les styles inline et construit un style global propre, avec un layout responsive, pour sortir du template Blazor standard."
