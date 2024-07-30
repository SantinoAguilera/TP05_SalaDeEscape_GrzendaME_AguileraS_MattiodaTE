using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;} = null!;
    public static int estadoJuego = 1;
    public static int contrasenaActual = 0;
    public static int[] estadoSalaID = new int[5];
    private static bool corregido = false;
    private static bool enviado = false;
    private static bool stancaHecho = false;
    private static string codigo = String.Empty;

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        contrasenaActual = 0;
        for(int i = 0; i < 5; i++) estadoSalaID[i] = 1;
        incognitasSalas = [
            "^vivainfo$",
            "^462$",
            @"\s*select\s+(?:contraseña|\*)\s+from\s+aulas(?<where>\s+where\s+aula\s*=\s*'ami')?;?",
            "^173$",
            "^025358$"
        ]; //0 - 25 - 3 - 58
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
            Regex regex = new(incognitasSalas[contrasenaActual]);
            var match = regex.Match(incognita.ToLower());
            if (match.Success)
            {
                if (contrasenaActual == 2)
                {
                    // caso especial para SQL
                    if (match.Groups["where"].Success)
                    {
                        // WHERE detectado, avanzamos contraseña
                        contrasenaActual++;
                        // y seteamos estado 5 para que pase a 6 al retornar
                        estadoJuego = 5;
                    }
                    else
                    {
                        // WHERE no detectado,
                        // seteamos estado 4 para que pase a 5 al retornar
                        estadoJuego = 4;
                    }
                }
                else
                {
                    contrasenaActual++;
                }
                return true;
            }
            /*
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
            */
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
        else if (estadoJuego == 8) estadoSalaID[2] = 3;
        else if (estadoJuego == 9) estadoSalaID[2] = 4;

        //>=
        if (estadoJuego >= 4) estadoSalaID[0] = 3;

        return estadoSalaID[sala - 1];
    }
    public static bool ResolverStanca(int botonNum)
    {
        switch(botonNum)
        {
            case 1:
                if (corregido == false)
                {
                    corregido = true;
                }
                break;
            case 2:
                if (corregido == false)
                {
                    corregido = true;
                }
                if (enviado == false)
                {
                    enviado = true;
                }
                break;
            case 3:
                if (corregido == true && enviado == false)
                {
                    enviado = true;
                }
                break;
        }
        if (corregido == true && enviado == true && stancaHecho == false)
        {
            stancaHecho = true;
            return true;
        }
        else
        {
            return false;
        }
        /*
        if ((!boton[botonNum - 1] && botonNum != 3) || (botonNum == 3 && boton[0]))
        {
            stancaFinalizado = true;
            return true;
        }
        else return false;
        */
    }
    public static int CheckearStanca()
    {
        if (corregido == true && enviado == false)
        {
            return 1;
        }
        else if (corregido == true && enviado == true)
        {
            return 2;
        }
        else if (corregido == false && enviado == true)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }
    public static void ResolverCaja(string num)
    {
        if (codigo.Length < 6)
        {
            codigo += num;
        }
    }
    public static bool CheckearCaja()
    {
        if (codigo == incognitasSalas[contrasenaActual])
        {   
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void BackspaceCaja()
    {
        if (codigo.Length != 0)
        {
            codigo = codigo.Substring(0, codigo.Length - 1);
        }
    }
}