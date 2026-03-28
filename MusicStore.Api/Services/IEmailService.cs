namespace MusicStore.Api.Services;

public interface IEmailService
{
    Task EnviarEmailAsync(string destinatario, string asunto, string mensajeHtml);
}
