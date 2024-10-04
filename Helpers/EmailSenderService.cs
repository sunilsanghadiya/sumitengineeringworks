// using System.Net.Mail;
// using MimeKit;


// namespace authmodule.Common
// {   
//     public class EmailSenderService
//     {
//         public EmailSenderService()
//         {

//         }

//         public static async Task SendEmailAsync(string emailTo, string subject, string body)
//         {
//             var message = new MimeMessage();
//             message.From.Add(new MailboxAddress("YourAppName", "youremail@example.com")); // Sender's email
//             message.To.Add(new MailboxAddress("Recipient Name", emailTo));
//             message.Subject = subject;

//             message.Body = new TextPart("plain")
//             {
//                 Text = body
//             };

//             using (var client = new SmtpClient())
//             {
//                 // try
//                 // {
//                 //     // Connect to the SMTP server (use your own server's details)
//                 //     await client.ConnectAsync("smtp.gmail.com", 587, false);  // SMTP server and port (for Gmail)

//                 //     // Authenticate with the SMTP server (replace with your email and password)
//                 //     await client.AuthenticateAsync("youremail@example.com", "yourpassword");

//                 //     // Send the email
//                 //     await client.SendAsync(message);
//                 // }
//                 // catch (Exception ex)
//                 // {
//                 //     Console.WriteLine($"An error occurred: {ex.Message}");
//                 // }
//                 // finally
//                 // {
//                 //     // Disconnect and dispose the SMTP client
//                 //     await client.DisconnectAsync(true);
//                 //     client.Dispose();
//                 // }
//             }
//         }
//     }
// }