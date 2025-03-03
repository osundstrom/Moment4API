using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SongDb>(options => 
    options.UseSqlite("Data Source = SongDb.db")
);

var app = builder.Build();

//Routes, standard vid bara /
app.MapGet("/" , ()  => "Välkommen till musik API!");

//Get
app.MapGet("/songs" , async(SongDb songDb) => {
    var songs = await songDb.Songs.ToListAsync();
    return Results.Ok(songs);
});

//Get specifik låt med id
app.MapGet("/songs/{id}", async(int id, SongDb songDb) => {
   var thisSong = await songDb.Songs.FindAsync(id)
    is Song song ? Results.Ok(song)
    :
    Results.NotFound("ID finns ej");
    return Results.Ok(thisSong);
});


//Post 
app.MapPost("/songs", async(Song song, SongDb songDb) => {

    songDb.Songs.Add(song);
    await songDb.SaveChangesAsync();

    return Results.Created("Tillagd:", song);
});

//Delete
app.MapDelete("/songs/{id}", async(int id, SongDb songDb) => {
    var thisSong = await songDb.Songs.FindAsync(id);
    
    if (thisSong != null) {
        songDb.Songs.Remove(thisSong);
        await songDb.SaveChangesAsync();

    }else {
        Results.NotFound("ID finns ej");
    }

    return Results.Ok("Raderad");

});

//Put
app.MapPut("/songs/{id}", async(int id, SongDb songDb) => {

});







//starta
app.Run();


//klass för låt, med id, namn, artist, längd, genre.
public class Song {
    public int id  {get; set; }
    public string? title  {get; set; }
    public string? artist  {get; set; }
    public int length  {get; set; }
    public string? category  {get; set; }
}


//DbContext
public class SongDb : DbContext {
    public SongDb(DbContextOptions<SongDb> options)
    : base(options) { }

    public DbSet<Song> Songs => Set<Song>();
}