static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    static int estadoJuego = 1;

    //Metodos
    private static void InicializarJuego()
    {
        estadoJuego = 1;
        string[] incognitasSalas = {"29809371", "vivainfo", "462", "select contraseña from contraseñas where aula = 'ami'", "173", "true", "025358"}; //0 - 25 - 3 - 58
    }
    public static int GetEstadoJuego()
    {
        return estadoJuego;
    }
    public static bool ResolverSala(int sala, string incognita)
    {
        int estadoJuego = GetEstadoJuego();
        if (sala == estadoJuego)
        {
            if (incognita == incognitasSalas[estadoJuego])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}