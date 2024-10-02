using MaintenancePlatform.Data;
using MaintenancePlatform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static MaintenancePlatform.Shared.MaintenanceSheetDto;

namespace MaintenancePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceSheetController : ControllerBase
    {
        private readonly MaintenanceDbContext _dbContext;

        public MaintenanceSheetController(MaintenanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/maintenancesheet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceSheet>>> GetMaintenanceSheets()
        {
            return await _dbContext.MaintenanceSheets.ToListAsync();
        }

        // GET: api/maintenancesheet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceSheet>> GetMaintenanceSheet(int id)
        {
            var sheet = await _dbContext.MaintenanceSheets.FindAsync(id);
            if (sheet == null) return NotFound();
            return sheet;
        }

        // POST: api/maintenancesheet
        [HttpPost]
        public async Task<IActionResult> PostMaintenanceSheet(MaintenanceSheetDto sheetDto)
        {

            Console.WriteLine("Received signature: " + sheetDto.Signature); // For testing
            // Convert DTO to model
            var sheet = new MaintenanceSheet
            {
                Technician = sheetDto.Technician,
                MaintenanceType = sheetDto.MaintenanceType,
                Equipment = sheetDto.Equipment,
                Notes = sheetDto.Notes,
                Signature = sheetDto.Signature,
                Latitude = sheetDto.Latitude,
                Longitude = sheetDto.Longitude,
            };

            // Get the location description based on latitude and longitude
            sheet.LocationDescription = await GetLocationDescription(sheet.Latitude, sheet.Longitude);

            // Add to database
            _dbContext.MaintenanceSheets.Add(sheet);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetMaintenanceSheet", new { id = sheet.Id }, sheet);
        }

        // POST: api/maintenancesheet/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(int maintenanceSheetId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine("UploadedFiles", file.FileName);

            // Ensure the "Uploads" directory exists
            if (!Directory.Exists("UploadedFiles"))
            {
                Directory.CreateDirectory("UploadedFiles");
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Optionally update the corresponding MaintenanceSheet with file info
            var sheet = await _dbContext.MaintenanceSheets.FindAsync(maintenanceSheetId);
            if (sheet != null)
            {
                sheet.UploadedFileName = file.FileName; // Update this property in your model
                await _dbContext.SaveChangesAsync();
            }

            return Ok(new { FileName = file.FileName, FilePath = filePath });

        }


        // DELETE: api/maintenancesheet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceSheet(int id)
        {
            var sheet = await _dbContext.MaintenanceSheets.FindAsync(id);
            if (sheet == null) return NotFound();

            _dbContext.MaintenanceSheets.Remove(sheet);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        public async Task<string> GetLocationDescription(double latitude, double longitude)
        {
            var apiKey = "myapikey";
            var client = new HttpClient();
            var response = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}");

            dynamic result = JsonConvert.DeserializeObject(response);

            if (result?.results?.Count > 0)
            {
                return result.results[0].formatted_address;
            }

            return "Unknown location"; // Adjust based on actual response structure
        }

    }
}
