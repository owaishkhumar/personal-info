﻿namespace PersonalInfo.Helper
{
    public static class UploadHandler
    {
        public static string Upload(IFormFile file)
        {
            //extension 
            List<string> validExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
            string extension = Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(extension))
            {
                return $"Extension is not supported ({string.Join(',', validExtensions)})";
            }

            //file size
            long size = file.Length;
            if (size > (5 * 1024 * 1024))
            {
                return "File size is too large";
            }

            //nameof changing
            string fileName = Path.GetFileName(file.FileName) + Guid.NewGuid().ToString() + extension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Images\\");
            Console.WriteLine(path);
            using FileStream stream = new FileStream(path + fileName, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }

        public static void DeleteImage(string imageUrl)
        {
            Console.WriteLine(imageUrl);
            if (File.Exists(Path.Combine($"{Directory.GetCurrentDirectory()}\\Images", imageUrl)))
            {
                File.Delete(Path.Combine($"{Directory.GetCurrentDirectory()}\\Images", imageUrl));
            }
        }
    }
}
