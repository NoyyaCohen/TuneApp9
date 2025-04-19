// C# Backend Implementation for Blazor

using DBL;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/songs/{id:int}")]
public class SongController : ControllerBase
{
    private readonly SongDB songDb;

    public SongController(SongDB songDb)
    {
        this.songDb = songDb;
    }

    // GET: api/songs/{id}
    [HttpGet("")]
    public async Task<IActionResult> GetSong(int id)
    {
        try
        {
            var song = await songDb.GetSongByID(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                id = song.SongID,
                title = song.SongName,
                thumbnailUrl = $"/api/songs/{id}/thumbnail",
                audioUrl = $"/api/songs/{id}/audio"
            }); ;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/songs/{id}/audio
    [HttpGet("audio")]
    public async Task<IActionResult> GetSongAudio(int id)
    {
        return StatusCode(500, "adsadsad");
    }
    //    try
    //    {
    //        var song = await songDb.GetSongByID(id);

    //        if (song == null || song.SongFile == null)
    //        {
    //            return NotFound();
    //        }

    //        return File(song.SongFile, "audio/mpeg");
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex.Message}");
    //   }

    // GET: api/songs/{id}/thumbnail
    [HttpGet("thumbnail")]
    public async Task<IActionResult> GetSongThumbnail(int id)
    {
        try
        {
            var song = await songDb.GetSongByID(id);

            if (song == null)
            {
                return NotFound();
            }

            var artistDb = new ArtistDB();
            var artist = await artistDb.GetArtistByID(song.ArtistID);
            if (artist == null)
            {
                return NotFound();
            }

            return File(artist.ProfileImage, "image/jpeg");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}