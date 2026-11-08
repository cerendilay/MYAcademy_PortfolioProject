namespace MYAcademy_PortfolyoProject.Services
{
    // Görsel yükleme işlemlerini tek yerde toplayan servis.
    // Hakkımda fotoğrafı ve proje görselleri bu servisi kullanır.
    public class ImageUploadService
    {
        private const long MaxFileSize = 2 * 1024 * 1024; // 2 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private readonly IWebHostEnvironment _environment;

        public ImageUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Dosyayı doğrular ve wwwroot/uploads/{folder} altına kaydeder.
        /// Başarılıysa veritabanına yazılacak yolu (ör. /uploads/projects/x.jpg) döner,
        /// değilse ErrorMessage dolu gelir.
        /// </summary>
        public async Task<(string? Path, string? ErrorMessage)> SaveAsync(IFormFile file, string folder)
        {
            if (file.Length == 0)
                return (null, "Seçilen dosya boş.");

            if (file.Length > MaxFileSize)
                return (null, "Görsel en fazla 2 MB olabilir.");

            var extension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                return (null, "Sadece .jpg, .jpeg, .png veya .webp uzantılı görseller yüklenebilir.");

            // Uzantı değiştirilmiş zararlı dosyalara karşı dosyanın ilk baytları (imza) kontrol edilir.
            if (!await HasImageSignatureAsync(file))
                return (null, "Dosya geçerli bir görsel değil.");

            // Kullanıcının verdiği dosya adı kullanılmaz; çakışma ve güvenlik için benzersiz ad üretilir.
            var fileName = $"{Guid.NewGuid():N}{extension}";
            var uploadDir = System.IO.Path.Combine(_environment.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadDir);

            await using (var stream = new FileStream(System.IO.Path.Combine(uploadDir, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return ($"/uploads/{folder}/{fileName}", null);
        }

        /// <summary>Daha önce yüklenmiş bir görseli siler (yalnızca /uploads altındakiler).</summary>
        public void Delete(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("/uploads/"))
                return;

            var fullPath = System.IO.Path.GetFullPath(
                System.IO.Path.Combine(_environment.WebRootPath, path.TrimStart('/')));
            var uploadsRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(_environment.WebRootPath, "uploads"));

            if (fullPath.StartsWith(uploadsRoot) && File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private static async Task<bool> HasImageSignatureAsync(IFormFile file)
        {
            var header = new byte[12];
            await using var stream = file.OpenReadStream();
            var read = await stream.ReadAsync(header.AsMemory(0, header.Length));
            if (read < 4) return false;

            bool isJpeg = header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
            bool isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
            bool isWebp = read >= 12
                          && header[0] == 'R' && header[1] == 'I' && header[2] == 'F' && header[3] == 'F'
                          && header[8] == 'W' && header[9] == 'E' && header[10] == 'B' && header[11] == 'P';
            return isJpeg || isPng || isWebp;
        }
    }
}
