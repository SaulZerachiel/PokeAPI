using APIProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Active Razor Components + mode interactif cote serveur (SignalR).
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient est injecte dans PokemonService pour appeler PokeAPI.
builder.Services.AddHttpClient();

// Services metier en Scoped: etat isole par session utilisateur.
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FavoriService>();
builder.Services.AddSingleton<PokemonService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // En prod, on masque les details d'erreur et on active HSTS.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Point d'entree Razor + mode de rendu interactif serveur.
app.MapRazorComponents<APIProject.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();