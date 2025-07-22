using System;
using System.Windows.Forms;
using System.Text;

namespace sELedit
{
    public static class ErrorHandler
    {
        public static void ShowErrorWithClipboard(string title, Exception ex)
        {
            StringBuilder errorDetails = new StringBuilder();
            errorDetails.AppendLine($"Error: {title}");
            errorDetails.AppendLine($"Message: {ex.Message}");
            errorDetails.AppendLine($"Type: {ex.GetType().FullName}");
            errorDetails.AppendLine($"Stack Trace:");
            errorDetails.AppendLine(ex.StackTrace);
            
            if (ex.InnerException != null)
            {
                errorDetails.AppendLine();
                errorDetails.AppendLine("Inner Exception:");
                errorDetails.AppendLine($"Message: {ex.InnerException.Message}");
                errorDetails.AppendLine($"Type: {ex.InnerException.GetType().FullName}");
                errorDetails.AppendLine($"Stack Trace:");
                errorDetails.AppendLine(ex.InnerException.StackTrace);
            }
            
            string errorText = errorDetails.ToString();
            
            // Copy to clipboard
            try
            {
                Clipboard.SetText(errorText);
            }
            catch
            {
                // If clipboard fails, continue
            }
            
            // Show message box with option to copy
            MessageBox.Show(
                $"{ex.Message}\n\nError details have been copied to clipboard.\n\nClick OK to continue.",
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            
            // Also log to debug output
            System.Diagnostics.Debug.WriteLine(errorText);
        }
        
        public static void ShowErrorWithClipboard(string title, string message)
        {
            StringBuilder errorDetails = new StringBuilder();
            errorDetails.AppendLine($"Error: {title}");
            errorDetails.AppendLine($"Message: {message}");
            
            string errorText = errorDetails.ToString();
            
            // Copy to clipboard
            try
            {
                Clipboard.SetText(errorText);
            }
            catch
            {
                // If clipboard fails, continue
            }
            
            // Show message box
            MessageBox.Show(
                $"{message}\n\nError details have been copied to clipboard.",
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            
            // Also log to debug output
            System.Diagnostics.Debug.WriteLine(errorText);
        }
    }
}