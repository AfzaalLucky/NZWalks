using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            
            // Upload image to local path
            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // AppURL https://localhost:port/images/image.jpg
            var urlFilePath =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Add Image to Image Table
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}
