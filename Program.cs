using System;


public abstract class Mensagem
{
    public string Conteudo { get; set; }
    public DateTime DataEnvio { get; private set; }

    public Mensagem(string conteudo)
    {
        Conteudo = conteudo;
        DataEnvio = DateTime.Now;
    }

    public abstract void EnviarMensagem();
}


public class MensagemTexto : Mensagem
{
    public MensagemTexto(string conteudo) : base(conteudo) { }

    public override void EnviarMensagem()
    {
        Console.WriteLine($"Mensagem de texto: {Conteudo}, Enviada em {DataEnvio}");
    }
}


public class MensagemVideo : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }
    public TimeSpan Duracao { get; set; }

    public MensagemVideo(string conteudo, string arquivo, string formato, TimeSpan duracao)
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
        Duracao = duracao;
    }

    public override void EnviarMensagem()
    {
        Console.WriteLine($"Mensagem de vídeo: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, Duração: {Duracao}, Enviada em {DataEnvio}");
    }
}


public class MensagemFoto : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public MensagemFoto(string conteudo, string arquivo, string formato)
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
    }

    public override void EnviarMensagem()
    {
        Console.WriteLine($"Mensagem de foto: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, foi enviada em {DataEnvio}");
    }
}


public class MensagemArquivo : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public MensagemArquivo(string conteudo, string arquivo, string formato)
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
    }

    public override void EnviarMensagem()
    {
        Console.WriteLine($"Mensagem de tipo arquivo, titulo: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, foi enviada em {DataEnvio}");
    }
}


public abstract class CanalComunicacao
{
    public string Destinatario { get; set; }
    public abstract void EnviarMensagem(Mensagem mensagem);
}


public class CanalWhatsApp : CanalComunicacao
{
   

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem via WhatsApp para o número {Destinatario}...");
        mensagem.EnviarMensagem();
    }
}

public class CanalTelegram : CanalComunicacao
{

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem via Telegram para o usuário {Destinatario}...");
        mensagem.EnviarMensagem();
    }
}

public class CanalFacebook : CanalComunicacao
{

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem via Facebook para o usuário {Destinatario}:");
        mensagem.EnviarMensagem();
    }
}

public class CanalInstagram : CanalComunicacao
{


    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem via Instagram para o usuário {Destinatario}:");
        mensagem.EnviarMensagem();
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("Escolha o canal de comunicação (WhatsApp, Telegram, Facebook, Instagram):");
        string canal = Console.ReadLine().ToLower();

        Console.WriteLine("Digite o conteúdo da mensagem:");
        string conteudo = Console.ReadLine();

        CanalComunicacao canalComunicacao;

switch (canal)
{
    case "whatsapp":
        canalComunicacao = new CanalWhatsApp();
        Console.WriteLine("Informe o telefone do usuário que deseja enviar a mensagem:");
        canalComunicacao.Destinatario = Console.ReadLine();
        break;
        case "telegram":
        canalComunicacao = new CanalTelegram();
        Console.WriteLine("Escolha o tipo de destinatário (Telefone, Usuário):");
        string tipoDestinatario = Console.ReadLine().ToLower();

        switch (tipoDestinatario)
        {
            case "telefone":
                Console.WriteLine("Digite o número de telefone:");
                canalComunicacao.Destinatario = Console.ReadLine();
                break;
            case "usuario":
                Console.WriteLine("Digite o usuário do Telegram:");
                canalComunicacao.Destinatario = Console.ReadLine();
                break;
            default:
                Console.WriteLine("Opção de destinatário inválida para Telegram.");
                return;
        }
        break;
    case "facebook":
        canalComunicacao = new CanalFacebook();
        Console.WriteLine("Digite o usuário do Facebook:");
        canalComunicacao.Destinatario = Console.ReadLine();
        break;
    case "instagram":
        canalComunicacao = new CanalInstagram();
        Console.WriteLine("Digite o usuário do Instagram:");
        canalComunicacao.Destinatario = Console.ReadLine();
        break;
    default:
        Console.WriteLine("Canal de comunicação inválido.");
        return;
}

Console.WriteLine("Escolha o tipo de mensagem (Texto, Video, Foto, Arquivo):");
string tipoMensagem = Console.ReadLine().ToLower();

Mensagem mensagem;

switch (tipoMensagem)
{
    case "texto":
        mensagem = new MensagemTexto(conteudo);
        break;
    case "video":
        Console.WriteLine("Digite o nome do video:");
        string arquivoVideo = Console.ReadLine();
        Console.WriteLine("Digite o formato do video:");
        string formatoVideo = Console.ReadLine();
        Console.WriteLine("Digite a duração do video em minutos:");
        double duracaoVideo = Convert.ToDouble(Console.ReadLine());
        mensagem = new MensagemVideo(conteudo, arquivoVideo, formatoVideo, TimeSpan.FromMinutes(duracaoVideo));
        break;
    case "foto":
        Console.WriteLine("Digite o nome do arquivo de foto:");
        string arquivoFoto = Console.ReadLine();
        Console.WriteLine("Digite o formato da foto:");
        string formatoFoto = Console.ReadLine();
        mensagem = new MensagemFoto(conteudo, arquivoFoto, formatoFoto);
        break;
    case "arquivo":
        Console.WriteLine("Digite o nome do arquivo:");
        string arquivo = Console.ReadLine();
        Console.WriteLine("Digite o formato do arquivo:");
        string formato = Console.ReadLine();
        mensagem = new MensagemArquivo(conteudo, arquivo, formato);
        break;
    default:
        Console.WriteLine("Tipo de mensagem inválido.");
        return;
}

        canalComunicacao.EnviarMensagem(mensagem);
    }
}
