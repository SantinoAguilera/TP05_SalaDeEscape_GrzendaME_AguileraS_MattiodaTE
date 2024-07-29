using System.Security.Cryptography.X509Certificates;

static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;} = null!;
    public static int estadoJuego = 1;
    public static int contrasenaActual = 0;
    public static int[] estadoSalaID = new int[5];
    private static int sumStanca = 0;
    private static bool[] boton = new bool[3];

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        contrasenaActual = 0;
        for(int i = 0; i < 5; i++) estadoSalaID[i] = 1;
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

        //==
        if (estadoJuego == 2) estadoSalaID[0] = 2;
        else if (estadoJuego == 4)
        {
            estadoSalaID[2] = 2;
            estadoSalaID[1] = 2;
        }
        else if (estadoJuego == 5) estadoSalaID[1] = 3;
        else if (estadoJuego == 6) estadoSalaID[1] = 4;

        //>=
        if (estadoJuego >= 4) estadoSalaID[0] = 3;

        return estadoSalaID[sala - 1];
    }
    public static bool ResolverStanca(int num, int botonNum)
    {
        if (!boton[botonNum])
        {
        sumStanca += num;
        boton[botonNum] = true;
        }
        if (sumStanca >= 2) return true;
        else return false;
    }
}