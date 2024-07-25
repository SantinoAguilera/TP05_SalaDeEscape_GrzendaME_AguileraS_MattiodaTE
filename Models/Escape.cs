static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    public static int estadoJuego = 1;
    public static int[] estadoSalaID = new int[5];
    public static int[] posSalaID = new int[5];

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        for(int i = 0; i < 5; i++)
        {
            estadoSalaID[i] = 1;
            posSalaID[i] = 1;
        }
        string[] incognitasSalas = {"vivainfo", "462", "select contraseña from aulas where aula = 'AMI';", "173", "025358"}; //0 - 25 - 3 - 58
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