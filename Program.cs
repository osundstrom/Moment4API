using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dbcontext och sqlite som databas
builder.Services.AddDbContext<SongDb>(options =>
    options.UseSqlite("Data Source = SongDb.db")
);

var app = builder.Build();

//--------------------------------GET-----------------------------------------//
//Routes, standard vid bara /
app.MapGet("/", () => "Välkommen till musik API!");

//Get vid /songs
app.MapGet("/songs", async (SongDb songDb) =>
{
    var songs = await songDb.Songs.ToListAsync(); //sätter songs till alla låtar
    return Results.Ok(songs); //returnerar all låtar
});

//Get specifik låt med id vid songs/id
app.MapGet("/songs/{id}", async (int id, SongDb songDb) =>
{
    var thisSong = await songDb.Songs.FindAsync(id); //hittar baserat på id

    if (thisSong != null){
        return Results.Ok(thisSong); //om låten finns 
    }else{
        return Results.NotFound("ID finns ej"); //om id ej finns
    };

});

//--------------------------------POST-----------------------------------------//

//Post vid /songs
app.MapPost("/songs", async (Song song, SongDb songDb) =>
{
    songDb.Songs.Add(song); //läggert ill i db
    await songDb.SaveChangesAsync(); //sparar

    return Results.Created("Tillagd:", song); //returnerar
});

//--------------------------------DELETE-----------------------------------------//

//Delete via id
app.MapDelete("/songs/{id}", async (int id, SongDb songDb) =>
{
    var thisSong = await songDb.Songs.FindAsync(id); //hittar baserat på id

    if (thisSong != null){
        songDb.Songs.Remove(thisSong); //tar bort om låt finns
        await songDb.SaveChangesAsync(); //sparar
        return Results.Ok("Raderad"); //returnerar 
    }else{
        return Results.NotFound("ID finns ej"); //om ej hittas
    }
});

//--------------------------------PUT-----------------------------------------//

//Put
app.MapPut("/songs/{id}", async (int id, Song newSong, SongDb songDb) =>
{
    var thisSong = await songDb.Songs.FindAsync(id); //hittar baserat på id

    if (thisSong != null){ //om låten skild fårn null, så byter vi gamla mot nya inmatade
        thisSong.artist = newSong.artist;
        thisSong.title = newSong.title;
        thisSong.length = newSong.length;
        thisSong.category = newSong.category;

        await songDb.SaveChangesAsync();//sparar
        return Results.Ok(new { Message = "Uppdaterad:", Song = newSong }); //returnerar 
    }else{
        return Results.NotFound("ID finns ej"); //om ej hittas
    }

});

//-------------------------------------------------------------------------//


//starta
app.Run();


//--------------------------------Klass och dbcontext-----------------------------------------//


//klass för låt, med id, namn, artist, längd, genre.
public class Song
{
    public int id { get; set; }
    public string? title { get; set; }
    public string? artist { get; set; }
    public int length { get; set; }
    public string? category { get; set; }
}


//DbContext
public class SongDb : DbContext
{
    public SongDb(DbContextOptions<SongDb> options)
    : base(options) { }

    //dbSet
    public DbSet<Song> Songs => Set<Song>();
}