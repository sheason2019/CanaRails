namespace CanaRails.Integration;

public class Program
{
  public static async Task Main(string[] args)
  {
    var ingressApp = Ingress.Program.CreateApplication();
    var manageApp = Manage.Program.CreateApplication();

    await Task.WhenAll(
      ingressApp.RunAsync("http://localhost:80"),
      manageApp.RunAsync("http://localhost:8080")
    );
  }
}