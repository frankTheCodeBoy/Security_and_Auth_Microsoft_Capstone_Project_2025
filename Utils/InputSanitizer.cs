using System.Text.RegularExpressions;

public static class InputSanitizer
{
    // Sanitizes username: allows only letters, numbers, underscores, 3-20 chars
    public static string SanitizeUsername(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        var sanitized = Regex.Replace(input, @"[^\w]", "");
        if (sanitized.Length < 3 || sanitized.Length > 20)
            return string.Empty;
        return sanitized;
    }

    // Sanitizes email using a simple regex and trims length to 100
    public static string SanitizeEmail(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length > 100)
            return string.Empty;
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(input, emailPattern) ? input : string.Empty;
    }

    // Escapes HTML to prevent XSS when displaying output
    public static string HtmlEncode(string input)
    {
        return System.Net.WebUtility.HtmlEncode(input);
    }
}