static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;} = null!;
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
        incognitasSalas = ["vivainfo", "462", "select contraseña from aulas where aula = 'ami';", "173", "025358"]; //0 - 25 - 3 - 58
    }
    public static int GetEstadoJuego()
    {
        return estadoJuego;
    }
    public static void AvanzarEstado()
    {
        estadoJuego++;
    }
    public static bool ResolverSala(string incognita, int contrasenaAceptada)
    {
        estadoJuego = GetEstadoJuego();
        if (contrasenaAceptada == contrasenaActual || contrasenaAceptada == -1)
        {
            if (incognita == incognitasSalas[contrasenaActual])
            {
                contrasenaActual++;
                return true;
            }
            else if (contrasenaActual == 2) //Tuve que hardcodearlo :(
            {
                if (incognita.ToLower() == incognitasSalas[contrasenaActual])
                {
                    estadoJuego = 5;
                    contrasenaActual++;
                    return true;
                }
                else if (incognita.ToLower() == "select contraseña from aulas;" && estadoJuego != 5) return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }
    public static int RevisarEstadoSala(int sala)
    {
        sala /= 10;

        //Acá es donde tengo que poner if (estadoJuego == sth) estadoSalaID[sth]++
        if (estadoJuego == 2) estadoSalaID[0] = 2;
        return estadoSalaID[sala - 1];
    }
}