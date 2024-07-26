static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    public static int estadoJuego = 1;
    public static int contrasenaActual = 0;
    public static int[] estadoSalaID = new int[5];
    public static int[] posSalaID = new int[5];

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        contrasenaActual = 0;
        for(int i = 0; i < 5; i++)
        {
            estadoSalaID[i] = 1;
            posSalaID[i] = 1;
        }
        string[] incognitasSalas = {"vivainfo", "462", "select contraseña from aulas where aula = 'ami';", "173", "025358"}; //0 - 25 - 3 - 58
    }
    public static int GetEstadoJuego()
    {
        return estadoJuego;
    }
    public static void AvanzarEstado()
    {
        estadoJuego++;
    }
    public static bool ResolverSala(string incognita)
    {
        int estadoJuego = GetEstadoJuego();
        if (incognita == incognitasSalas[contrasenaActual])
        {
            contrasenaActual++;
            return true;
        }
        else return false;
    }
    public static int RevisarEstadoSala(int sala)
    {
        sala /= 10;

        //Acá es donde tengo que poner if (estadoJuego == sth) estadoSalaID[sth]++
        return estadoSalaID[sala - 1];
    }
}