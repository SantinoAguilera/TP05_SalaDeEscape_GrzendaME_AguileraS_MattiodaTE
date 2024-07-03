static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    static int estadoJuego = 1;

    //Metodos
    private static void InicializarJuego()
    {
        estadoJuego = 1;
    }
    public static int GetEstadoJuego()
    {
        return estadoJuego;
    }
    public static bool ResolverSala(int sala, string incognita)
    {
        
    }
}