static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    public static int estadoJuego = 1;
    public static int salaID = 1;
    public static int estadoSalaID = 1;
    public static int posSalaID = 1;

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        salaID = 1;
        estadoSalaID = 1;
        posSalaID = 1;
        string[] incognitasSalas = {"vivainfo", "462", "select contraseña from aulas where aula = 'AMI';", "173", "true", "025358"}; //0 - 25 - 3 - 58
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