using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

static class Escape
{
    //Atributos
    public static string[] incognitasSalas {get; private set;}
    public static int estadoJuego = 1;
    public static int contrasenaActual = 0;
    public static int[] estadoSalaID = new int[5];
    private static bool corregido = false;
    private static bool enviado = false;
    private static bool stancaHecho = false;
    private static string codigo = String.Empty;
    public static bool mostrarPistas = false;
    private static int pistaActual = 0;
    public static Dictionary<int, string[]> pistas = new Dictionary<int, string[]>
    {
        {1, 
            [
                "Necesitás encontrar una contraseña para la puerta. ¿Quizás está guardada en esa computadora?",
                "¡A veces las personas dejan sus contraseñas escritas en los lugares más obvios!"
            ]
        },
        {3,
            [
                "Buscá a alguien que te pueda abrir la puerta."
            ]
        },
        {4,
            [
                "Revisá la computadora prendida en el CIDI.",
                "Buscá la contraseña del AMI entre las contraseñas.",
                "Pista “Necesito ayuda con SQL”: Para buscar todas las contraseñas hay que escribir: select contraseña from aulas."
            ]
        },
        {5,
            [
                "¿De dónde exactamente querés la contraseña?",
                "Pista “No sé SQL”: Para buscar la contraseña del AMI hay que escribir: select contraseña from aulas where aula = 'AMI';"
            ]
        },
        {6,
            [
                "Stancatrón te pidió que corrijas y envíes las pruebas, podés hacer eso en la computadora en el fondo del AMI. Después de hacerlo, volvé a hablar con él."
            ]
        },
        {7,
            [
                "Stancatrón te pidió que corrijas y envíes las pruebas, podés hacer eso en la computadora en el fondo del AMI. Después de hacerlo, volvé a hablar con él."
            ]
        },
        {8,
            [
                "Volvé a hablar con Stancatrón."
            ]
        },
        {9,
            [
                "¿Qué significará ese marcador arriba de la caja fuerte?",
                "¿Quizás hayan más marcadores?",
                "Hay un marcador por habitación, excepto en la que está Stancatrón.",
                "Revisá abajo de la mesa del HMP.",
                "Revisá la computadora del CIDI.",
                "Revisá el proyector del AMI.",
                "Los marcadores son de distintos colores y tienen distintos números, ¿qué significará?",
                "Las computadoras del AMI tienen una pista.",
                "Los números de las computadoras son el orden.",
                "Primero va el marcador del mismo color que el 1. Después lo mismo, con los números que le siguen."
            ]
        }
    };

    //Metodos
    public static void InicializarJuego()
    {
        estadoJuego = 1;
        contrasenaActual = 0;
        for(int i = 0; i < 5; i++) estadoSalaID[i] = 1;
        incognitasSalas = [
            "^vivainfo$",
            "^462$",
            @"^\s*select\s+(?:contraseña|\*)\s+from\s+aulas(?<where>\s+where\s+aula\s*=\s*'ami')?;?\s*$",
            "^173$",
            "025358" //0 - 25 - 3 - 58
        ];
        corregido = false;
        enviado = false;
        stancaHecho = false;
        codigo = String.Empty;
        mostrarPistas = false;
        pistaActual = 0;
    }
    public static int GetEstadoJuego()
    {
        return estadoJuego;
    }
    public static void AvanzarEstado()
    {
        estadoJuego++;
        pistaActual = 0;
    }
    public static bool ResolverSala(string incognita, int contrasenaAceptada)
    {
        estadoJuego = GetEstadoJuego();
        if (contrasenaAceptada == contrasenaActual || contrasenaAceptada == -1)
        {
            Regex regex = new(incognitasSalas[contrasenaActual]);
            if (contrasenaActual != 0) incognita = incognita.ToLower();
            var match = regex.Match(incognita);
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
        else if (estadoJuego == 10) estadoSalaID[4] = 2;
        else if (estadoJuego == 11) estadoSalaID[4] = 3;

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
    public static string ResolverCaja(string num)
    {
        if (codigo.Length < 6)
        {
            codigo += num;
        }
        return codigo;
    }
    public static bool CheckearCaja()
    {
        if (codigo == incognitasSalas[contrasenaActual])
        {  
            return true;
        }
        else
        {
            codigo = String.Empty;
            return false;
        }
    }
    public static string BackspaceCaja()
    {
        if (codigo.Length != 0)
        {
            codigo = codigo.Substring(0, codigo.Length - 1);
        }
        return codigo;
    }
    public static string SeleccionarPista(int boton)
    {
        if (boton == 0) mostrarPistas = !mostrarPistas;

        if (boton != 2) pistaActual += boton;
        
        if (pistas.ContainsKey(estadoJuego))
        {
            if (pistaActual < 0) pistaActual++;
            else if (pistaActual > pistas[estadoJuego].Length - 1) pistaActual--;
            return pistas[estadoJuego][pistaActual];
        }
        else return null;
    }
}