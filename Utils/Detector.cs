using Tesseract;

namespace UmatoMusume.Utils
{
    public class Detector
    {
        private const int AVG = 3;
        private const int BRIGHTNESS_THRESHOLD = 200;
        private const float IMAGE_SCALE = 2.0f;

        public static Rectangle? CaptureArea(IntPtr _processhWnd)
        {
            if (_processhWnd == IntPtr.Zero)
            {
                MessageBox.Show("Process window not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var rect = Hook.GetWindowRectangle(_processhWnd);
            var height = rect.Bottom - rect.Top;
            var width = rect.Right - rect.Left;

            Rectangle processRect = new Rectangle(rect.Left, rect.Top, width, height);
            using (var overlay = new FrmScreenSelectionOverlay(processRect))
            {
                if (overlay.ShowDialog() == DialogResult.OK)
                {
                    Rectangle selectedRect = overlay.SelectedRectangle;
                    if (selectedRect.Width > 0 && selectedRect.Height > 0)
                    {
                        return selectedRect;
                    }
                }

                return null;
            }
        }

        public static Bitmap CaptureScreen(Rectangle region)
        {
            Bitmap bitmap = new Bitmap(region.Width, region.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(region.X, region.Y, 0, 0, region.Size);
            }
            return bitmap;
        }

        public static Bitmap ScaleImage(Bitmap _image)
        {
            int newWidth = (int)(_image.Width * IMAGE_SCALE);
            int newHeight = (int)(_image.Height * IMAGE_SCALE);

            Bitmap scaledImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(_image, 0, 0, newWidth, newHeight);
            }
            return scaledImage;
        }

        public static Bitmap PreprocessImage(Bitmap _image)
        {
            Bitmap processed = new Bitmap(_image.Width, _image.Height);

            for (int y = 0; y < _image.Height; y++)
            {
                for (int x = 0; x < _image.Width; x++)
                {
                    Color pixel = _image.GetPixel(x, y);

                    int brightness = (pixel.R + pixel.G + pixel.B) / AVG;

                    if (brightness > BRIGHTNESS_THRESHOLD)
                    {
                        processed.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        processed.SetPixel(x, y, Color.White);
                    }
                }
            }

            return processed;
        }

        public static string DetectText(Rectangle _captureArea)
        {
            using (Bitmap screenshot = CaptureScreen(_captureArea))
            {
                Bitmap scaledImage = ScaleImage(screenshot);
                Bitmap preprocessedImage = PreprocessImage(scaledImage);
                string result = string.Empty;
                try
                {
                    using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                    {
                        engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ");
                        engine.SetVariable("tessedit_pageseg_mode", "7");
                        engine.SetVariable("user_defined_dpi", "300");

                        using (var ms = new MemoryStream())
                        {
                            preprocessedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ms.Position = 0;
                            using (var img = Pix.LoadFromMemory(ms.ToArray()))
                            {
                                using (var page = engine.Process(img))
                                {
                                    result = page.GetText().Trim();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during text detection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return result;
            }
        }
    }
}
